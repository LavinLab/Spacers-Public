using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWave : MonoBehaviour
{
    public static ChangeWave instance;
    [SerializeField] private GameObject Wave;
    [SerializeField] public Text WaveText;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] otherEnemiesPrefab;
    [SerializeField] GameObject Magnum;
    [SerializeField] GameObject Goat;
    public GameObject player;
    GameObject ActualMagnum;
    GameObject ActualGoat;
    public static int count = 1;
    private int newcount = 3;
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
    void Update()
    {
        
        if (player.activeSelf && ActualGoat == null && Enemy5BulletRelease.enemy.Count == 0 && KonfuseBulletRelease.enemy.Count == 0 && EnemyBulletRelease.enemy.Count == 0 && Enemy3BulletRelease.enemy.Count == 0 && Enemy2Behaviour.enemy.Count == 0 && count > 0)
        {
            if(PlayerPrefs.GetInt("Language", 0) == 0)
            {
                WaveText.text = "Wave: " + count;
            }
            else if(PlayerPrefs.GetInt("Language", 0) == 1)
            {
                WaveText.text = "Волна: " + count;
            }

            if (count % 10 == 9 && (int)count / 10 % 2 == 1 && player.activeSelf)
            {
                ActualGoat = Instantiate(Goat);
                count++;
            }
            else
            {
                Destroy(ActualGoat);
            }
            
        }
        if (player.activeSelf && ActualMagnum == null && Enemy5BulletRelease.enemy.Count == 0 && KonfuseBulletRelease.enemy.Count == 0 && EnemyBulletRelease.enemy.Count == 0 && Enemy3BulletRelease.enemy.Count == 0 && Enemy2Behaviour.enemy.Count == 0 && count > 0)
        {
            if (PlayerPrefs.GetInt("Language", 0) == 0)
            {
                WaveText.text = "Wave: " + count;
            }
            else if (PlayerPrefs.GetInt("Language", 0) == 1)
            {
                WaveText.text = "Волна: " + count;
            }
            if (count % 10 == 9 && player.activeSelf)
            {
                ActualMagnum = Instantiate(Magnum);
                count++;
            }
            else
            {
                Destroy(ActualMagnum);
            }
        }
        
        if (player.activeSelf && Enemy5BulletRelease.enemy.Count == 0 && KonfuseBulletRelease.enemy.Count == 0 && EnemyBulletRelease.enemy.Count == 0 && Enemy3BulletRelease.enemy.Count == 0 && Enemy2Behaviour.enemy.Count == 0 && count >= 0 && ActualMagnum == null && ActualGoat == null)
        {
            if (PlayerPrefs.GetInt("Language", 0) == 0)
            {
                WaveText.text = "Wave: " + count;
            }
            else if (PlayerPrefs.GetInt("Language", 0) == 1)
            {
                WaveText.text = "Волна: " + count;
            }
            count++;
            if (count % 10 != 0 || count == 0)
            {
                newcount = 3;
                StartCoroutine(SkrollBG());

                float cameraHeight = 2f * Camera.main.orthographicSize;
                float cameraWidth = cameraHeight * Camera.main.aspect;

                int enemyCount = UnityEngine.Random.Range(2, 5);

                float rowSpacing = 1.0f;
                float offsetFromTop = 1f;

                for (int i = 0; i < enemyCount; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        float x = -cameraWidth / 2 + cameraWidth / (enemyCount + 1) * (i + 1);
                        float y = cameraHeight / 2 - offsetFromTop - j * rowSpacing;

                        GameObject enemyToSpawn = enemyPrefab;
                        // Íà÷èíàÿ ñ òðåòüåé âîëíû, åñòü øàíñ ïîÿâëåíèÿ äðóãîãî âðàãà
                        if (count >= 5 && UnityEngine.Random.value < 0.5f && newcount > 0)
                        {
                            enemyToSpawn = otherEnemiesPrefab[Random.Range(0, otherEnemiesPrefab.Length)];
                            newcount--;
                        }


                        var enemy = Instantiate(enemyToSpawn, new Vector3(x, y, 0), Quaternion.identity);
                        var bulletRelease = enemy.GetComponent<EnemyBulletRelease>();
                        if (bulletRelease != null)
                        {
                            bulletRelease.gameObject.SetActive(true);
                            EnemyBulletRelease.shoot = true;
                            bulletRelease.StartCoroutine(bulletRelease.ShootBullet());
                        }
                        var bullet3Release = enemy.GetComponent<Enemy3BulletRelease>();
                        if (bullet3Release != null)
                        {
                            bullet3Release.gameObject.SetActive(true);
                            Enemy3BulletRelease.shoot = true;
                            bullet3Release.StartCoroutine(bullet3Release.ShootBullet());
                        }
                        var bulletKonfuseRelease = enemy.GetComponent<KonfuseBulletRelease>();
                        if (bulletKonfuseRelease != null)
                        {
                            bulletKonfuseRelease.gameObject.SetActive(true);
                            KonfuseBulletRelease.shoot = true;
                            bulletKonfuseRelease.StartCoroutine(bulletKonfuseRelease.ShootBullet());
                        }
                        var bulletEnemy5Release = enemy.GetComponent<Enemy5BulletRelease>();
                        if (bulletEnemy5Release != null)
                        {
                            bulletEnemy5Release.gameObject.SetActive(true);
                            Enemy5BulletRelease.shoot = true;
                            bulletEnemy5Release.StartCoroutine(bulletEnemy5Release.ShootBullet());
                        }
                    }
                }
            }
        }
    }
    IEnumerator SkrollBG()
    {
        BG.instance.y = 0.5f;
        yield return new WaitForSeconds(0.5f);
        BG.instance.y = 0.05f; // Âåðíóòü ñêîðîñòü ê èñõîäíîìó çíà÷åíèþ
    }
}
