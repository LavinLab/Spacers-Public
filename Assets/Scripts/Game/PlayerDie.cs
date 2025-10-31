using GooglePlayGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDie : MonoBehaviour
{
    public static PlayerDie instance;
    public GameObject gameOver;
    [SerializeField] GameObject pause;
    public List<GameObject> gameObjects = new List<GameObject>();
    [SerializeField] GameObject restart;
    public static float lives, startLives;
    public Sprite halfOfHeart;
    private GameObject[] hearts;
    public GameObject heart;
    public static bool start = false;
    public int indexOfRebirth = 0;
    public GameObject damageX2;
    public Text damageX2Text;
    public Text livesText;
    public GameObject livesObject;
    private bool isSpeedDownActive = false;
    private Coroutine speedDownCoroutine;
    public float startInterval = 0;
    const string heartAchievment = "CgkIl9XD1bEWEAIQBQ";
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        lives = Core.GetLives();
        startLives = lives;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy3Bullet")
        {
            ShakeCameraEffect.instance.Shake();
            hearts = GameObject.FindGameObjectsWithTag("Heart");
            if (lives > 0 && !livesObject.activeSelf)
            {
                if (lives - Mathf.Floor(lives) == 0f)
                {
                    lives -= 0.5f;
                    int index = Mathf.FloorToInt(lives); // Use FloorToInt for consistency
                    if (index >= 0 && index < hearts.Length)
                    {
                        hearts[index].GetComponent<Image>().sprite = halfOfHeart;
                        Destroy(collision.gameObject);
                        if (isSpeedDownActive)
                        {
                            StopCoroutine(speedDownCoroutine);
                            if(SpeedUpEffect.instance.startInterval != 0)
                            {
                                ShootCartridge.shootInterval = SpeedUpEffect.instance.startInterval;
                            }
                            else
                            {
                                ShootCartridge.shootInterval = startInterval;
                            }
                            
                        }
                        speedDownCoroutine = StartCoroutine(Speeddown());
                    }
                }
                else if (lives - Mathf.Floor(lives) == 0.5f)
                {
                    lives -= 0.5f;
                    int index = Mathf.FloorToInt(lives); // Use FloorToInt for consistency
                    if (index >= 0 && index < hearts.Length)
                    {
                        Destroy(hearts[index]);
                        Destroy(collision.gameObject);
                        if (isSpeedDownActive)
                        {
                            StopCoroutine(speedDownCoroutine);
                            if (SpeedUpEffect.instance.startInterval != 0)
                            {
                                ShootCartridge.shootInterval = SpeedUpEffect.instance.startInterval;
                            }
                            else
                            {
                                ShootCartridge.shootInterval = startInterval;
                            }
                        }
                        speedDownCoroutine = StartCoroutine(Speeddown());
                    }
                }
            }
            else if (lives > 0 && livesObject.activeSelf)
            {
                lives -= 0.5f;
                livesText.text = lives.ToString();
                Destroy(collision.gameObject);
                if (isSpeedDownActive)
                {
                    StopCoroutine(speedDownCoroutine);
                    if (SpeedUpEffect.instance.startInterval != 0)
                    {
                        ShootCartridge.shootInterval = SpeedUpEffect.instance.startInterval;
                    }
                    else
                    {
                        ShootCartridge.shootInterval = startInterval;
                    }
                }
                speedDownCoroutine = StartCoroutine(Speeddown());
            }

        }
        if (collision.gameObject.tag == "EnemyBullet") {
            ShakeCameraEffect.instance.Shake();
            hearts = GameObject.FindGameObjectsWithTag("Heart");
            if (lives > 0 && !livesObject.activeSelf)
            {
                if (lives - Mathf.Floor(lives) == 0f)
                {
                    int index = Mathf.FloorToInt(lives - 1); // Use FloorToInt
                    if (index >= 0 && index < hearts.Length)
                    {
                        Destroy(hearts[index]);
                    }
                    lives--;
                    Destroy(collision.gameObject);
                }
                else if (lives - Mathf.Floor(lives) == 0.5f)
                {
                    lives -= 1f;
                    int index = Mathf.CeilToInt(lives); // Use FloorToInt
                    if (index >= 0 && index < hearts.Length)
                    {
                        Destroy(hearts[index]);
                        if(index-1 >= 0 && index-1 < hearts.Length)
                        {
                            hearts[index - 1].GetComponent<Image>().sprite = halfOfHeart;
                        }
                    }
                    Destroy(collision.gameObject);
                }
            }
            else if (lives > 0 && livesObject.activeSelf)
            {
                lives--;
                livesText.text = lives.ToString();
                Destroy(collision.gameObject);
            }

        }
        if (collision.gameObject.tag == "Enemy")
        {
            ShakeCameraEffect.instance.Shake();
            hearts = GameObject.FindGameObjectsWithTag("Heart");
            if (lives > 0 && !livesObject.activeSelf)
            {
                if (lives - Mathf.Floor(lives) == 0f)
                {
                    lives -= 0.5f;
                    int index = Mathf.FloorToInt(lives); // Use FloorToInt for consistency
                    if (index >= 0 && index < hearts.Length)
                    {
                        hearts[index].GetComponent<Image>().sprite = halfOfHeart;
                    }
                }
                else if (lives - Mathf.Floor(lives) == 0.5f)
                {
                    lives -= 0.5f;
                    int index = Mathf.FloorToInt(lives); // Use FloorToInt for consistency
                    if (index >= 0 && index < hearts.Length)
                    {
                        Destroy(hearts[index]);
                    }
                }
            }
            else if (lives > 0 && livesObject.activeSelf)
            {
                lives-=0.5f;
                livesText.text = lives.ToString();
            }

        }
        if (collision.gameObject.tag == "KonfuseBullet" || collision.gameObject.tag == "Konfuse")
        {
            ShakeCameraEffect.instance.Shake();
            Move.isOk = false;
            hearts = GameObject.FindGameObjectsWithTag("Heart");
            if (lives > 0 && !livesObject.activeSelf)
            {
                if (lives - Mathf.Floor(lives) == 0f)
                {
                    lives -= 0.5f;
                    int index = Mathf.FloorToInt(lives); // Use FloorToInt for consistency
                    if (index >= 0 && index < hearts.Length)
                    {
                        hearts[index].GetComponent<Image>().sprite = halfOfHeart;
                    }
                }
                else if (lives - Mathf.Floor(lives) == 0.5f)
                {
                    lives -= 0.5f;
                    int index = Mathf.FloorToInt(lives); // Use FloorToInt for consistency
                    if (index >= 0 && index < hearts.Length)
                    {
                        Destroy(hearts[index]);
                    }
                }
            }
            else if (lives > 0 && livesObject.activeSelf)
            {
                lives -= 0.5f;
                livesText.text = lives.ToString();
            }
            if(collision.gameObject.tag == "KonfuseBullet")
            {
                Destroy(collision.gameObject);
            }
            Invoke("isOkChange", 10f);
        }
        if (collision.gameObject.tag == "SmallGoatBullet")
        {
            ShakeCameraEffect.instance.Shake();
            hearts = GameObject.FindGameObjectsWithTag("Heart");
            if (lives > 0 && !livesObject.activeSelf)
            {
                if (lives - Mathf.Floor(lives) == 0f)
                {
                    lives -= 0.5f;
                    int index = Mathf.FloorToInt(lives); // Use FloorToInt for consistency
                    if (index >= 0 && index < hearts.Length)
                    {
                        hearts[index].GetComponent<Image>().sprite = halfOfHeart;
                    }
                }
                else if (lives - Mathf.Floor(lives) == 0.5f)
                {
                    lives -= 0.5f;
                    int index = Mathf.FloorToInt(lives); // Use FloorToInt for consistency
                    if (index >= 0 && index < hearts.Length)
                    {
                        Destroy(hearts[index]);
                    }
                }
            }
            else if (lives > 0 && livesObject.activeSelf)
            {
                lives -= 0.5f;
                livesText.text = lives.ToString();
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Magnum")
        {
            ShakeCameraEffect.instance.Shake();
            hearts = GameObject.FindGameObjectsWithTag("Heart");
            if (lives > 0 && !livesObject.activeSelf)
            {
                if (lives - Mathf.Floor(lives) == 0f)
                {
                    lives -= 0.5f;
                    int index = Mathf.FloorToInt(lives); // Use FloorToInt for consistency
                    if (index >= 0 && index < hearts.Length)
                    {
                        hearts[index].GetComponent<Image>().sprite = halfOfHeart;
                    }
                }
                else if (lives - Mathf.Floor(lives) == 0.5f)
                {
                    lives -= 0.5f;
                    int index = Mathf.FloorToInt(lives); // Use FloorToInt for consistency
                    if (index >= 0 && index < hearts.Length)
                    {
                        Destroy(hearts[index]);
                    }
                }
            }
            else if (lives > 0 && livesObject.activeSelf)
            {
                lives-=0.5f;
                livesText.text = lives.ToString();
            }

        }
        if(collision.gameObject.tag == "MagnumBullet")
        {
            ShakeCameraEffect.instance.Shake();
            hearts = GameObject.FindGameObjectsWithTag("Heart");
            if (lives > 0 && !livesObject.activeSelf)
            {
                if (lives - Mathf.Floor(lives) == 0f)
                {
                    int index = Mathf.FloorToInt(lives - 1); // Use FloorToInt
                    if (index >= 0 && index < hearts.Length)
                    {
                        Destroy(hearts[index]);
                    }
                    lives--;
                    Destroy(collision.gameObject);
                }
                else if (lives - Mathf.Floor(lives) == 0.5f)
                {
                    lives -= 1f;
                    int index = Mathf.CeilToInt(lives); // Use FloorToInt
                    if (index >= 0 && index < hearts.Length)
                    {
                        Destroy(hearts[index]);
                        if (index - 1 >= 0 && index - 1 < hearts.Length)
                        {
                            hearts[index - 1].GetComponent<Image>().sprite = halfOfHeart;
                        }

                    }
                    Destroy(collision.gameObject);
                }
            }else if(lives> 0 && livesObject.activeSelf)
            {
                lives--;
                livesText.text = lives.ToString();
                Destroy(collision.gameObject);
            }
            
        }
        if (collision.gameObject.tag == "SmallGoat")
        {
            ShakeCameraEffect.instance.Shake();
            hearts = GameObject.FindGameObjectsWithTag("Heart");
            if (lives > 0 && !livesObject.activeSelf)
            {
                if (lives - Mathf.Floor(lives) == 0f)
                {
                    int index = Mathf.FloorToInt(lives - 1); // Use FloorToInt
                    if (index >= 0 && index < hearts.Length)
                    {
                        Destroy(hearts[index]);
                    }
                    lives--;
                }
                else if (lives - Mathf.Floor(lives) == 0.5f)
                {
                    lives -= 1f;
                    int index = Mathf.CeilToInt(lives); // Use FloorToInt
                    if (index >= 0 && index < hearts.Length)
                    {
                        Destroy(hearts[index]);
                        if (index - 1 >= 0 && index - 1 < hearts.Length)
                        {
                            hearts[index - 1].GetComponent<Image>().sprite = halfOfHeart;
                        }
                    }
                }
            }
            else if (lives > 0 && livesObject.activeSelf)
            {
                lives--;
                livesText.text = lives.ToString();
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BigGoat")
        {
            ShakeCameraEffect.instance.Shake();
            hearts = GameObject.FindGameObjectsWithTag("Heart");
            if (lives > 0 && !livesObject.activeSelf)
            {
                if (lives - Mathf.Floor(lives) == 0f)
                {
                    int index = Mathf.FloorToInt(lives - 1); // Use FloorToInt
                    if (index >= 0 && index < hearts.Length)
                    {
                        Destroy(hearts[index]);
                    }
                    lives--;
                }
                else if (lives - Mathf.Floor(lives) == 0.5f)
                {
                    lives -= 1f;
                    int index = Mathf.CeilToInt(lives); // Use FloorToInt
                    if (index >= 0 && index < hearts.Length)
                    {
                        Destroy(hearts[index]);
                        if (index - 1 >= 0 && index - 1 < hearts.Length)
                        {
                            hearts[index - 1].GetComponent<Image>().sprite = halfOfHeart;
                        }

                    }
                }
            }
            else if (lives > 0 && livesObject.activeSelf)
            {
                lives--;
                livesText.text = lives.ToString();
            }

        }
    }

    void isOkChange()
    {
        Move.isOk = true;
    }
    public IEnumerator Speeddown()
    {
        isSpeedDownActive = true;
        CartridgeMove.Instance.speed -= 2;
        startInterval = ShootCartridge.shootInterval;
        ShootCartridge.shootInterval *= 2f;
        yield return new WaitForSeconds(4f);
        // Reset values and object position
        CartridgeMove.Instance.speed = 10f;
        if (SpeedUpEffect.instance.startInterval != 0)
        {
            ShootCartridge.shootInterval = SpeedUpEffect.instance.startInterval;
        }
        else
        {
            ShootCartridge.shootInterval = startInterval;
        }

        isSpeedDownActive = false;
    }
    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        if (start && lives <= 10)
        {
            for (int i = 0; i < lives; i++)
            {
                GameObject newHeart = Instantiate(heart); // Создаем новый объект heart
                newHeart.transform.SetParent(GameObject.Find("Canvas").transform, false); // Делаем его дочерним объектом canvas
                RectTransform heartRectTransform = newHeart.GetComponent<RectTransform>(); // Получаем компонент RectTransform нового объекта heart
                heartRectTransform.anchoredPosition = new Vector2(37 + i * 70, heartRectTransform.anchoredPosition.y); // Устанавливаем позицию Pos X начиная от 37 и каждый со смещением в 70
            }
            start = false;
        }else if (start && lives > 10)
        {
            gameObjects.Clear();
            gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Heart"));
            foreach (var gameObject in gameObjects)
            {
                if (gameObject != null)
                {
                    Destroy(gameObject);
                }
            }
            GameObject newHeart = Instantiate(heart); // Создаем новый объект heart
            newHeart.transform.SetParent(GameObject.Find("Canvas").transform, false); // Делаем его дочерним объектом canvas
            RectTransform heartRectTransform = newHeart.GetComponent<RectTransform>(); // Получаем компонент RectTransform нового объекта heart
            heartRectTransform.anchoredPosition = new Vector2(37, heartRectTransform.anchoredPosition.y);
            livesObject.SetActive(true);
            livesText.text = lives.ToString();
            start = false;
        }
        if(lives <= 0)
        {
            livesObject.SetActive(false);
            if (indexOfRebirth > 0)
            {
                lives += 1;
                gameObjects.Clear();
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Heart"));
                foreach (var gameObject in gameObjects)
                {
                    if (gameObject != null)
                    {
                        Destroy(gameObject);
                    }
                }
                start = true;
                indexOfRebirth--;
            }
            else
            {
                
                gameObjects.Clear();
                gameObjects.Add(GameObject.FindGameObjectWithTag("Magnum"));
                gameObjects.Add(GameObject.FindGameObjectWithTag("BigGoat"));
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag("EnemyBullet"));
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Enemy3Bullet"));
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Shield"));
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag("HealHeart"));
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Crystall"));
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Coin"));
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Aim"));
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag("AimBullet"));
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Heart"));
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag("KonfuseBullet"));
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag("MistlesBullet"));
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Mistles"));
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Konfuse"));
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag("SmallGoat"));
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag("ToDelete"));
                foreach (var gameObject in gameObjects)
                {
                    if (gameObject != null)
                    {
                        Destroy(gameObject);
                    }
                }
                restart.SetActive(true);
                gameOver.SetActive(true);
                pause.SetActive(false);
                gameObject.transform.position = new Vector3(0, -4, 0);
                gameObject.SetActive(false);
                SpeedUpEffect.instance.objectToMove.SetActive(false);
                SpeedUpEffect.instance.duration = 0;
                SpeedUpEffect.instance.countdownText.text = "0s";
                ShieldEffect.instance.objectToMove.SetActive(false);
                ShieldEffect.instance.duration = 0;
                ShieldEffect.instance.countdownText.text = "0s";
                MistlesEffect.instance.objectToMove.SetActive(false);
                MistlesEffect.instance.duration = 0;
                MistlesEffect.instance.countdownText.text = "0s";
                ShootCartridge.shootInterval = startInterval;
                damageX2.SetActive(false);
                damageX2Text.text = "0s";
                startLives = 0;
                PlayGamesPlatform.Instance.ReportProgress(heartAchievment, 100.0f, (bool success) => { });
            }
        }
    }

}
