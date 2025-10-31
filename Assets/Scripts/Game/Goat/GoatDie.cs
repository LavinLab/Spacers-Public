using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoatDie : MonoBehaviour
{
    private float lives;
    private float startlives;
    private float damage = 1;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject crystall;
    [SerializeField] GameObject heal;
    private bool changedHalf = false;
    public static GoatDie instance;
    [SerializeField] Image healthBar;
    [SerializeField] Text healthText;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Core.GetDamage();
        damage = Core.damage;
        lives = ChangeWave.count * 5 * damage;
        healthText.text = lives.ToString();
        startlives = lives;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ComplexBullet")
        {
            if (lives <= 0)
            {
                Destroy(collision.gameObject);
                ScoreManager.instance.count += 199;
                ScoreManager.instance.IncreaseScore();
                SpawnCoins();
                SpawnCrystall();
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
            if(lives <= 0)
            {
                Destroy(collision.gameObject);
                ScoreManager.instance.count += 199;
                ScoreManager.instance.IncreaseScore();
                SpawnCoins();
                SpawnCrystall();
                Destroy(gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
                lives -= damage;
            }
        }
        if (collision.gameObject.tag == "FireBullet")
        {
            if (lives <= 0)
            {
                ScoreManager.instance.count += 199;
                ScoreManager.instance.IncreaseScore();
                SpawnCoins();
                SpawnCrystall();
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
                ScoreManager.instance.count += 199;
                ScoreManager.instance.IncreaseScore();
                SpawnCoins();
                SpawnCrystall();
                Destroy(collision.gameObject);
                Destroy(gameObject);

            }
            else
            {
                Destroy(collision.gameObject);
                lives -= 0.5f;
            }
        }
        if (collision.gameObject.tag == "MistlesBullet")
        {
            if (lives <= 0)
            {
                Destroy(collision.gameObject);
                ScoreManager.instance.count += 199;
                ScoreManager.instance.IncreaseScore();
                SpawnCoins();
                SpawnCrystall();
                Destroy(gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
                lives -= damage * 0.2f;
            }
        }
        if (collision.gameObject.tag == "AimBullet")
        {
            if (lives <= 0)
            {
                Destroy(collision.gameObject);
                ScoreManager.instance.count += 199;
                ScoreManager.instance.IncreaseScore();
                SpawnCoins();
                SpawnCrystall();
                Destroy(gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
                lives -= damage * 0.5f;
            }
        }
    }
    private void Update()
    {
        healthBar.fillAmount = lives / startlives;
        healthText.text = System.Convert.ToInt32(lives).ToString();
        if (lives <= startlives / 2 && !changedHalf)
        {
            GoatMove.instance.isFirstPhase = false;
            GoatBehaviour.instance.spawn = false;
            GoatBehaviour.instance.StopAllCoroutines();
            GameObject spawnedHeal = Instantiate(heal, transform.position, Quaternion.identity);
            spawnedHeal.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -4);
            changedHalf = true;
            foreach(GameObject small in SmallGoatBehaviour.enemySmallGoats)
            {
                if (small != null)
                {
                    Destroy(small);
                }
            }
        }
    }

    void SpawnCoins()
    {
        int numberOfCoins = Random.Range(20, 100);
        GameObject actualCoin = null;
        for (int i = 0; i < numberOfCoins; i++)
        {
            actualCoin = Instantiate(coin, transform.position, Quaternion.identity);
            actualCoin.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -4);
        }
        if(actualCoin != null)
        {
            actualCoin.GetComponentInChildren<Text>().text = "x" + numberOfCoins;
        }
    }
    void SpawnCrystall()
    {
        int numberOfCrystalls = Random.Range(1, 10);
        GameObject actualCrystall = null;
        for (int i = 0; i < numberOfCrystalls; i++)
        {
            actualCrystall = Instantiate(crystall, transform.position, Quaternion.identity);
            actualCrystall.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -6);
        }
        if(actualCrystall != null)
        {
            actualCrystall.GetComponentInChildren<Text>().text = "x" + numberOfCrystalls;
        }
    }
    private void OnDestroy()
    {
        foreach(var goat in SmallGoatBehaviour.enemySmallGoats)
        {
            if(goat != null)
            {
                Destroy(goat);
            }
        }
    }
}
