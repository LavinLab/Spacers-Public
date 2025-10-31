using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.Notifications.Android;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class ControlOfGame : MonoBehaviour
{
    [SerializeField] private GameObject Wave;
    public GameObject pause;
    public List<GameObject> gameObjects = new List<GameObject>();
    public GameObject[] UI;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject Score;
    [SerializeField] private GameObject MaxScore;
    [SerializeField] private Text MaxScoreText;
    [SerializeField] GameObject restart;
    [SerializeField] GameObject AddGun;
    public bool start;
    private GameObject[] hearts;
    public GameObject shop;
    private GameObject Magnum;
    private bool isSpeedUp = false;
    private bool isShield = false;
    private bool isMistles = false;
    private float SpeedUpCount;
    private float ShieldCount;
    private float MistlesCount;
    private float MagnumLives;
    public GameObject player;
    public Sprite classic;
    public Sprite immortal;
    public Sprite fulminant;
    public Sprite goldenWarrior;
    public Sprite firethrower;
    public Sprite firethrowerWithGun;
    public GameObject damageX2;
    public int damageTime;
    public GameObject leaderBoard;
    public GameObject settings;
    public GameObject achievments;
    public GameObject play;
    // Flags to track active effects when pausing
    private bool speedUpActiveOnPause = false;
    private bool shieldActiveOnPause = false;
    private bool mistlesActiveOnPause = false;
    const string immortalAchievment = "CgkIl9XD1bEWEAIQAg";
    [SerializeField] Light2D global;
    GameObject Goat;
    GameObject[] SmallGoats;
    [SerializeField] Material ClassicMaterial, ImmortalMaterial, FulminantMaterial, FirethrowerMaterial;
    // Start is called before the first frame update
    void Start()
    {
        global.intensity = 1;
        Social.ReportProgress(immortalAchievment, 0f, (bool success) => { });
        damageX2.SetActive(false);

        ToggleGameObjects(false); // Deactivate all game objects
        ToggleUI(true); // Activate UI elements

        if (PlayerPrefs.HasKey("MaxScore"))
        {
            MaxScore.SetActive(true);
            if(PlayerPrefs.GetInt("Language", 0) == 0)
            {
                MaxScoreText.text = "Max score: " + PlayerPrefs.GetInt("MaxScore");
            }
            else if(PlayerPrefs.GetInt("Language", 0) == 1)
            {
                MaxScoreText.text = "Рекорд: " + PlayerPrefs.GetInt("MaxScore");
            }
            
        }
        else
        {
            MaxScore.SetActive(false);
        }

        restart.SetActive(false);
        Score.SetActive(false);
        gameOver.SetActive(false);
        pause.SetActive(false);
        Wave.SetActive(false);
        ChangeWave.count = 1;
        start = true;

        hearts = GameObject.FindGameObjectsWithTag("Heart"); // Find hearts initially
        leaderBoard.SetActive(true);
        settings.SetActive(true);
        achievments.SetActive(true);
    }

    public void PlayClicked()
    {
        global.intensity = 0.06f;
        achievments.SetActive(false);
        settings.SetActive(false);
        leaderBoard.SetActive(false);
        if(PlayerDie.startLives > 10)
        {
            PlayerDie.instance.livesObject.SetActive(true);
        }
        else
        {
            PlayerDie.instance.livesObject.SetActive(false);
        }
        if (DamageX2Effect.instance.activeCoroutine)
        {
            StartCoroutine(DamageX2Effect.instance.DamageX2(damageTime));
        }
        else
        {
            StartCoroutine(DamageX2Effect.instance.CountdownCooldown(damageTime));
        }
        if (PlayerPrefs.HasKey("ChosenShip"))
        {
            if(PlayerPrefs.GetInt("ChosenShip") == 0)
            {
                player.GetComponent<SpriteRenderer>().sprite = classic;
                player.GetComponent<SpriteRenderer>().material = ClassicMaterial;
                if(PlayerPrefs.GetInt("ClassicStars") == 4)
                {
                    damageX2.SetActive(true);
                }
            }
            else if(PlayerPrefs.GetInt("ChosenShip") == 1)
            {
                player.GetComponent<SpriteRenderer>().sprite = immortal;
                player.GetComponent<SpriteRenderer>().material = ImmortalMaterial;
            }
            else if (PlayerPrefs.GetInt("ChosenShip") == 2)
            {
                player.GetComponent<SpriteRenderer>().sprite = fulminant;
                player.GetComponent<SpriteRenderer>().material = FulminantMaterial;
            }
            else if (PlayerPrefs.GetInt("ChosenShip") == 3)
            {
                player.GetComponent<SpriteRenderer>().sprite = goldenWarrior;
            }
            else if (PlayerPrefs.GetInt("ChosenShip") == 4)
            {
                player.GetComponent<SpriteRenderer>().sprite = firethrower;
                if(PlayerPrefs.GetInt("FirethrowerStars") == 4)
                {
                    player.GetComponent<SpriteRenderer>().sprite = firethrowerWithGun;
                }
                if (PlayerPrefs.GetInt("FirethrowerStars") == 4 && start)
                {
                    Instantiate(AddGun, player.transform.position, Quaternion.identity, player.transform);
                }
                player.GetComponent<SpriteRenderer>().material = FirethrowerMaterial;
            }
        }
        else
        {
            player.GetComponent<SpriteRenderer>().sprite = classic;
            player.GetComponent<SpriteRenderer>().material = ClassicMaterial;
        }
        if (start)
        {
            // Reset game state on fresh start
            DestroyEnemies();
            ChangeWave.count = 0;
            PlayerDie.lives = Core.GetLives(); // Default lives

        
            PlayerDie.start = true;
            hearts = GameObject.FindGameObjectsWithTag("Heart");
            StartCoroutine(PushUp());
        }
        else
        {
            ToggleHearts(true);
            // Reactivate enemies if resuming from pause
            ActivateEnemies();
        }

        // Activate hearts based on current lives
        UpdateHearts();
        EnemyBulletRelease.shoot = true;
        foreach (var go in EnemyBulletRelease.allEnemies)
        {
            if(go != null && go.gameObject.activeSelf)
                go.StartCoroutine(go.ShootBullet());
        }
        Enemy3BulletRelease.shoot = true;
        foreach (var go in Enemy3BulletRelease.allEnemies)
        {
            if (go != null && go.gameObject.activeSelf)
                go.StartCoroutine(go.ShootBullet());
        }
        KonfuseBulletRelease.shoot = true;
        foreach (var go in KonfuseBulletRelease.allEnemies)
        {
            if (go != null && go.gameObject.activeSelf)
                go.StartCoroutine(go.ShootBullet());
        }
        Enemy5BulletRelease.shoot = true;
        foreach (var go in Enemy5BulletRelease.allEnemies)
        {
            if (go != null && go.gameObject.activeSelf)
                go.StartCoroutine(go.ShootBullet());
        }

        ToggleGameObjects(true); // Activate game objects
        ToggleUI(false); // Deactivate UI elements
        Score.SetActive(true);
        MaxScore.SetActive(false);
        Wave.SetActive(true);
        pause.SetActive(true);
        start = false;

        // Reactivate effects if they were active on pause
        SpeedUpEffect.instance.objectToMove.SetActive(speedUpActiveOnPause);
        ShieldEffect.instance.objectToMove.SetActive(shieldActiveOnPause);
        MistlesEffect.instance.objectToMove.SetActive(mistlesActiveOnPause);

        // Activate special guns if equipped
        ActivateSpecialGuns();
    }

    IEnumerator PushUp()
    {
        Vector3 originalPos = new Vector3(0f, -6f, 0f);
        Vector3 posNeeded = new Vector3(0f, -4f, 0f);
        float posElapsedTime = 0f;
        float posDuration = 0.5f;
        while (posElapsedTime < posDuration)
        {
            posElapsedTime += Time.deltaTime;
            float t = posElapsedTime / posDuration;
            player.transform.position = Vector3.Lerp(originalPos, posNeeded, t);
            yield return null;
        }
        player.transform.position = posNeeded;
    }
    public void PauseClicked()
    {
        global.intensity = 1;
        achievments.SetActive(true);
        settings.SetActive(true);
        PlayerDie.instance.livesObject.SetActive(false);
        damageTime = DamageX2Effect.instance.remainingTime;
        damageX2.SetActive(false);
        if(AimEffect.aimObject != null)
        {
            Destroy(AimEffect.aimObject);
        }
        
        DeactivateEffects();
        leaderBoard.SetActive(true);
        UpdateHearts(); // Update hearts based on current lives before hiding them

        
        Enemy3BulletRelease.shoot = false;
        KonfuseBulletRelease.shoot = false;
        Enemy5BulletRelease.shoot = false;
        foreach (var go in EnemyBulletRelease.allEnemies)
        {
            
            if (go != null && go.gameObject.activeSelf)
                go.StopCoroutine(go.ShootBullet());
        }
        foreach (var go in Enemy3BulletRelease.allEnemies)
        {
            if (go != null && go.gameObject.activeSelf)
                go.StopCoroutine(go.ShootBullet());
        }
        foreach (var go in KonfuseBulletRelease.allEnemies)
        {
            if (go != null && go.gameObject.activeSelf)
                go.StopCoroutine(go.ShootBullet());
        }
        foreach (var go in Enemy5BulletRelease.allEnemies)
        {
            if (go != null && go.gameObject.activeSelf)
                go.StopCoroutine(go.ShootBullet());
        }
        DeactivateEnemies();

        ToggleGameObjects(false); // Deactivate game objects
        ToggleUI(true); // Activate UI elements
        shop.SetActive(false);
        Wave.SetActive(false);

        if (PlayerPrefs.HasKey("MaxScore"))
        {
            MaxScore.SetActive(true);
            if (PlayerPrefs.GetInt("Language", 0) == 0)
            {
                MaxScoreText.text = "Max score: " + PlayerPrefs.GetInt("MaxScore");
            }
            else if (PlayerPrefs.GetInt("Language", 0) == 1)
            {
                MaxScoreText.text = "Рекорд: " + PlayerPrefs.GetInt("MaxScore");
            }
        }
        else
        {
            MaxScore.SetActive(false);
        }

        pause.SetActive(false);

        // Deactivate special guns
        DeactivateSpecialGuns();

        // Store active effect states before deactivating
        speedUpActiveOnPause = SpeedUpEffect.instance.objectToMove.activeSelf;
        shieldActiveOnPause = ShieldEffect.instance.objectToMove.activeSelf;
        mistlesActiveOnPause = MistlesEffect.instance.objectToMove.activeSelf;

        // Deactivate effects
        SpeedUpEffect.instance.objectToMove.SetActive(false);
        ShieldEffect.instance.objectToMove.SetActive(false);
        MistlesEffect.instance.objectToMove.SetActive(false);

        ToggleHearts(false); // Hide hearts during pause
    }

    public void RestartClicked()
    {
        Move.isOk = true;
        global.intensity = 1;
        achievments.SetActive(true);
        settings.SetActive(true);
        leaderBoard.SetActive(true);
        start = true;

        ToggleGameObjects(false); // Deactivate game objects
        ToggleUI(true); // Activate UI elements

        if (PlayerPrefs.HasKey("MaxScore"))
        {
            MaxScore.SetActive(true);
            if (PlayerPrefs.GetInt("Language", 0) == 0)
            {
                MaxScoreText.text = "Max score: " + PlayerPrefs.GetInt("MaxScore");
            }
            else if (PlayerPrefs.GetInt("Language", 0) == 1)
            {
                MaxScoreText.text = "Рекорд: " + PlayerPrefs.GetInt("MaxScore");
            }
        }
        else
        {
            MaxScore.SetActive(false);
        }

        restart.SetActive(false);
        Score.SetActive(false);
        gameOver.SetActive(false);
        pause.SetActive(false);
        Wave.SetActive(false);
        ChangeWave.count = 1;
        ScoreManager.instance.count = 0;
        ShootCartridge.instance.Start();
        // Destroy special guns if they exist
        DestroySpecialGuns();
    }
    public void AchievmentOpen()
    {
        Social.ShowAchievementsUI();
    }
    public void ShopClicked()
    {
        SceneManager.LoadScene("Shop");
    }
    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            DeactivateEnemies();
        }
    }

    // Helper functions to improve code readability and maintainability:

    private void ToggleGameObjects(bool active)
    {
        foreach (GameObject go in gameObjects)
        {
            go.SetActive(active);
        }
    }

    private void ToggleUI(bool active)
    {
        foreach (GameObject go in UI)
        {
            go.SetActive(active);
        }
    }

    private void DestroyEnemies()
    {
        foreach (GameObject go in EnemyBulletRelease.enemy)
        {
            Destroy(go);
        }

        foreach (GameObject go in Enemy2Behaviour.enemy)
        {
            Destroy(go);
        }
        foreach (GameObject go in Enemy3BulletRelease.enemy)
        {
            Destroy(go);
        }
    }

    private void ActivateEnemies()
    {
        foreach (GameObject go in EnemyBulletRelease.enemy)
        {
            go.SetActive(true);
        }

        foreach (GameObject go in Enemy2Behaviour.enemy)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in Enemy3BulletRelease.enemy)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in KonfuseBulletRelease.enemy)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in Enemy5BulletRelease.enemy)
        {
            go.SetActive(true);
        }
        if (Magnum != null)
        {
            Magnum.SetActive(true);
            MagnumDie.instance.lives = MagnumLives;
            if (!MagnumDie.instance.canAttack)
            {
                StartCoroutine(MagnumDie.instance.TheSecondFaze());
            }
        }
        if(Goat != null)
        {
            Goat.SetActive(true);
            if (GoatMove.instance.isFirstPhase)
            {
                GoatBehaviour.instance.spawn = true;
                GoatBehaviour.instance.StartCoroutine(GoatBehaviour.instance.SmallGoatSpawn());
            }
            else
            {
                GoatMove.instance.shouldStart = true;
            }
        }
        foreach (GameObject go in SmallGoats)
        {
            if(go != null)
            {
                go.GetComponent<SmallGoatBehaviour>().StartCoroutine(go.GetComponent<SmallGoatBehaviour>().ShootBullet(true));
                go.SetActive(true);
            }
            
        }
        if (isSpeedUp && SpeedUpEffect.instance.duration != 0)
        {
            SpeedUpEffect.instance.duration = SpeedUpCount;
            StartCoroutine(SpeedUpEffect.instance.Speedup(SpeedUpEffect.instance.duration, true));
            isSpeedUp = false;
        }
        if (isShield && ShieldEffect.instance.duration != 0)
        {
            StartCoroutine(ShieldEffect.instance.Shield(ShieldCount, true));
            isShield = false;
        }
        if (isMistles && MistlesEffect.instance.duration != 0)
        {
            StartCoroutine(MistlesEffect.instance.SpawnGuns(MistlesCount, true));
            isMistles = false;
        }
    }

    private void DeactivateEnemies()
    {
        foreach (GameObject go in EnemyBulletRelease.enemy)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in Enemy3BulletRelease.enemy)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in KonfuseBulletRelease.enemy)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in Enemy2Behaviour.enemy)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in Enemy5BulletRelease.enemy)
        {
            go.SetActive(false);
        }
        Magnum = GameObject.FindGameObjectWithTag("Magnum");
        if (Magnum != null)
        {
            MagnumLives = MagnumDie.instance.lives;
            MagnumDie.instance.StopAllCoroutines();
            Magnum.SetActive(false);
            
        }
        Goat = GameObject.FindGameObjectWithTag("BigGoat");
        if (Goat != null)
        {
            Goat.SetActive(false);
            GoatDie.instance.StopAllCoroutines();
            GoatBehaviour.instance.spawn = false;
            GoatBehaviour.instance.StopAllCoroutines();
            GoatMove.instance.StopAllCoroutines();
        }
        SmallGoats = GameObject.FindGameObjectsWithTag("SmallGoat");
        foreach(GameObject go in SmallGoats)
        {
            if (gameObject != null)
            {
                go.GetComponent<SmallGoatBehaviour>().StopAllCoroutines();
                go.SetActive(false);
            }
        }
    }

    void DeactivateEffects()
    {
        if (SpeedUpEffect.instance.objectToMove.activeSelf)
        {
            SpeedUpCount = SpeedUpEffect.instance.duration;
            StopCoroutine(SpeedUpEffect.instance.Speedup(SpeedUpCount, false));
            ShootCartridge.shootInterval = SpeedUpEffect.instance.startInterval;
            isSpeedUp = true;
        }
        if(ShieldEffect.instance.actualShield != null)
        {
            if (ShieldEffect.instance.actualShield.activeSelf)
            {
                ShieldCount = ShieldEffect.instance.duration;
                StopCoroutine(ShieldEffect.instance.Shield(ShieldCount, false));
                Destroy(ShieldEffect.instance.actualShield);
                isShield = true;
            }
        }
        if (MistlesEffect.instance.rightGun != null && MistlesEffect.instance.leftGun != null)
        {
            if (MistlesEffect.instance.rightGun.activeSelf && MistlesEffect.instance.leftGun.activeSelf)
            {
                MistlesCount = MistlesEffect.instance.duration;
                StopCoroutine(MistlesEffect.instance.SpawnGuns(MistlesCount, false));
                Destroy(MistlesEffect.instance.leftGun);
                Destroy(MistlesEffect.instance.rightGun);
                isMistles = true;
            }
        }
        if (CoinCollect.Instance.coinObject != null)
        {
            Destroy(CoinCollect.Instance.coinObject);
        }
        if (CrystalCollect.Instance.crystallObject != null)
        {
            Destroy(CrystalCollect.Instance.crystallObject);
        }
    }

    private void UpdateHearts()
    {
        hearts = GameObject.FindGameObjectsWithTag("Heart");
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < PlayerDie.lives);
        }
    }

    private void ActivateSpecialGuns()
    {
        if (MistlesEffect.instance.rightGun != null)
        {
            MistlesEffect.instance.rightGun.SetActive(true);
        }
        if (MistlesEffect.instance.leftGun != null)
        {
            MistlesEffect.instance.leftGun.SetActive(true);
        }
        if (AimEffect.aimObject != null)
        {
            AimEffect.aimObject.SetActive(true);
        }
        if (ShieldEffect.instance.actualShield != null)
        {
            ShieldEffect.instance.actualShield.SetActive(true);
        }
    }

    private void DeactivateSpecialGuns()
    {
        if (MistlesEffect.instance.rightGun != null)
        {
            MistlesEffect.instance.rightGun.SetActive(false);
        }
        if (MistlesEffect.instance.leftGun != null)
        {
            MistlesEffect.instance.leftGun.SetActive(false);
        }
        if (AimEffect.aimObject != null)
        {
            AimEffect.aimObject.SetActive(false);
        }
        if (ShieldEffect.instance.actualShield != null)
        {
            ShieldEffect.instance.actualShield.SetActive(false);
        }
    }

    private void DestroySpecialGuns()
    {
        if (MistlesEffect.instance.rightGun != null)
        {
            Destroy(MistlesEffect.instance.rightGun);
        }
        if (MistlesEffect.instance.leftGun != null)
        {
            Destroy(MistlesEffect.instance.leftGun);
        }
        if (AimEffect.aimObject != null)
        {
            Destroy(AimEffect.aimObject);
        }
        if (ShieldEffect.instance.actualShield != null)
        {
            Destroy(ShieldEffect.instance.actualShield);
        }
    }

    // Helper function to toggle heart visibility
    private void ToggleHearts(bool visible)
    {
        foreach (GameObject heart in hearts)
        {
            heart.SetActive(visible);
        }
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause && player.activeSelf)
        {
            PauseClicked();
        }
    }
}