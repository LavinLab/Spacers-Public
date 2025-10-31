using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDie : MonoBehaviour
{
    public float lives;
    public GameObject[] effectPrefabs;
    public static float damage;// ������ ��� ����� �������� ��������
    private bool complexShot = false;
    public GameObject fulminantBullet;
    private int killsBeforeHeal;
    public Transform bulletSpawnPoint;
    const string kills50Achievement = "CgkIl9XD1bEWEAIQBg";
    const string kills1000Achievement = "CgkIl9XD1bEWEAIQBw";
    const string kills10000Achievement = "CgkIl9XD1bEWEAIQCA";

    // Start is called before the first frame update
    void Start()
    {
        lives = ChangeWave.count;
        Core.GetDamage();
        damage = Core.damage;
        killsBeforeHeal = Core.killsBeforeHeal;
        complexShot = Core.complexShot;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ComplexBullet")
        {
            if (lives <= 1)
            {
                ScoreManager.instance.IncreaseScore();
                Destroy(collision.gameObject);
                SpawnEffect();
                if (PlayerPrefs.GetInt("ChosenShip") == 1 && PlayerPrefs.GetInt("ImmortalStars") == 4)
                {
                    killsBeforeHeal += 1;
                }
                PlayGamesPlatform.Instance.IncrementAchievement(kills50Achievement, 1, (bool success) => { });
                PlayGamesPlatform.Instance.IncrementAchievement(kills1000Achievement, 1, (bool success) => { });
                PlayGamesPlatform.Instance.IncrementAchievement(kills10000Achievement, 1, (bool success) => { });
                Destroy(gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
                lives -= damage / 2f;
            }
        }
        if (collision.gameObject.tag == "Bullet")
        {
            if (lives <= 1)
            {
                ScoreManager.instance.IncreaseScore();
                Destroy(collision.gameObject);
                SpawnEffect();
                if (PlayerPrefs.GetInt("ChosenShip") == 1 && PlayerPrefs.GetInt("ImmortalStars") == 4)
                {
                    killsBeforeHeal += 1;
                }
                PlayGamesPlatform.Instance.IncrementAchievement(kills50Achievement, 1, (bool success) => { });
                PlayGamesPlatform.Instance.IncrementAchievement(kills1000Achievement, 1, (bool success) => { });
                PlayGamesPlatform.Instance.IncrementAchievement(kills10000Achievement, 1, (bool success) => { });
                Destroy(gameObject);

            }
            else
            {
                Destroy(collision.gameObject);
                if (PlayerPrefs.GetInt("ImmortalStars") == 3 && PlayerPrefs.GetInt("ChosenShip") == 1 || PlayerPrefs.GetInt("ImmortalStars") == 4 && PlayerPrefs.GetInt("ChosenShip") == 1)
                {
                    damage = Random.Range(4, 7);
                }
                if (complexShot)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        GameObject complexBullet = Instantiate(fulminantBullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                        complexBullet.tag = "ComplexBullet";
                        complexBullet.GetComponent<CartridgeMove>().speed = 5f;
                        if (i == 0)
                        {
                            complexBullet.GetComponent<CartridgeMove>().direction = Vector3.up;
                        }
                        else if (i == 1)
                        {
                            complexBullet.GetComponent<CartridgeMove>().direction = Vector3.left;
                        }
                        else if (i == 2)
                        {
                            complexBullet.GetComponent<CartridgeMove>().direction = Vector3.right;
                        }
                        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                        Invoke("EnableCollider", 0.2f);
                    }
                }
                lives -= damage;
            }
        }
        if (collision.gameObject.tag == "FireBullet")
        {
            if (lives <= 1)
            {
                ScoreManager.instance.IncreaseScore();
                SpawnEffect();
                PlayGamesPlatform.Instance.IncrementAchievement(kills50Achievement, 1, (bool success) => { });
                PlayGamesPlatform.Instance.IncrementAchievement(kills1000Achievement, 1, (bool success) => { });
                PlayGamesPlatform.Instance.IncrementAchievement(kills10000Achievement, 1, (bool success) => { });
                Destroy(gameObject);

            }
            else
            {
                lives -= damage;
            }
        }
        if (collision.gameObject.tag == "AddFireBullet")
        {
            if (lives <= 1)
            {
                ScoreManager.instance.IncreaseScore();
                SpawnEffect();
                PlayGamesPlatform.Instance.IncrementAchievement(kills50Achievement, 1, (bool success) => { });
                PlayGamesPlatform.Instance.IncrementAchievement(kills1000Achievement, 1, (bool success) => { });
                PlayGamesPlatform.Instance.IncrementAchievement(kills10000Achievement, 1, (bool success) => { });
                Destroy(collision.gameObject);
                Destroy(gameObject);

            }
            else
            {
                Destroy(collision.gameObject);
                lives -= damage;
            }
        }
        if (collision.gameObject.tag == "MistlesBullet")
        {
            if (lives <= 1)
            {
                ScoreManager.instance.IncreaseScore();
                Destroy(collision.gameObject);
                SpawnEffect();
                if (PlayerPrefs.GetInt("ChosenShip") == 1 && PlayerPrefs.GetInt("ImmortalStars") == 4)
                {
                    killsBeforeHeal += 1;
                }
                PlayGamesPlatform.Instance.IncrementAchievement(kills50Achievement, 1, (bool success) => { });
                PlayGamesPlatform.Instance.IncrementAchievement(kills1000Achievement, 1, (bool success) => { });
                PlayGamesPlatform.Instance.IncrementAchievement(kills10000Achievement, 1, (bool success) => { });
                Destroy(gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
                lives -= (float)damage / 0.2f;
            }
        }
        if (collision.gameObject.tag == "AimBullet")
        {
            if (lives <= 1)
            {
                ScoreManager.instance.IncreaseScore();
                Destroy(collision.gameObject);
                SpawnEffect();
                if (PlayerPrefs.GetInt("ChosenShip") == 1 && PlayerPrefs.GetInt("ImmortalStars") == 4)
                {
                    killsBeforeHeal += 1;
                }
                PlayGamesPlatform.Instance.IncrementAchievement(kills50Achievement, 1, (bool success) => { });
                PlayGamesPlatform.Instance.IncrementAchievement(kills1000Achievement, 1, (bool success) => { });
                PlayGamesPlatform.Instance.IncrementAchievement(kills10000Achievement, 1, (bool success) => { });
                Destroy(gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
                lives -= (float)damage / 0.5f;
            }
        }
    }
    void EnableCollider()
    {
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    }
    // ������� ��� �������� ��������
    void SpawnEffect()
    {
        if (PlayerPrefs.HasKey("ChanseHave"))
        {
            if (Random.value > (float)(PlayerPrefs.GetInt("ChanseHave") + 40) / 100)
            {
                return;
            }
        }
        else
        {
            if (Random.value > 0.4f) // 60% ����, ��� ������ �� ����������
            {
                return;
            }
        }
        int coinIndex = 0; // ������ ������� ������ � ������� effectPrefabs
        int crystalIndex = 1; // ������ ������� ��������� � ������� effectPrefabs
        int effectIndex;

        // 15% ����, ��� ��������� ������ ������ ��� ���������
        if (Random.value < 0.15f)
        {
            // ���� PlayerPrefs.GetInt("ChosenShip") == 3, �� �������� ���� ������, ���� ��������
            if (PlayerPrefs.GetInt("ChosenShip") == 3)
            {
                effectIndex = (Random.value < 0.5f) ? coinIndex : crystalIndex;
            }
            else // ����� �������� ������ ������
            {
                effectIndex = coinIndex;
            }
        }
        else
        {
            // ����� ���������� �������, �������� ������ ������ � ���������
            do
            {
                effectIndex = Random.Range(0, effectPrefabs.Length);
            }
            while (effectIndex == coinIndex || effectIndex == crystalIndex);
        }

        GameObject effect = Instantiate(effectPrefabs[effectIndex], transform.position, Quaternion.identity); // �������� �������
        effect.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -4); // ���������� �������� ����
    }


    // Update is called once per frame

    void Update()
    {
        if(killsBeforeHeal >= 30)
        {
            // Calculate max hearts and current lives
            int maxHearts = Core.GetLives(); // Default lives

            
            float currentLives = PlayerDie.lives;

            // Increase lives if possible
            if (currentLives < maxHearts)
            {
                PlayerDie.lives = Mathf.Min(currentLives + 1f, maxHearts);
            }

            // Update heart display
            HealHeart.instance.UpdateHeartsDisplay();
            killsBeforeHeal = 0;
        }
    }
}
