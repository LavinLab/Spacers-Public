using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGoatDie : MonoBehaviour
{
    private bool complexShot = false;
    public GameObject fulminantBullet;
    private int killsBeforeHeal;
    public Transform bulletSpawnPoint;
    public static float damage;
    private float lives = ChangeWave.count * 5 / 10;
    // Start is called before the first frame update
    void Start()
    {
        damage = 1;
        Core.GetDamage();
        complexShot = Core.complexShot;
        killsBeforeHeal = Core.killsBeforeHeal;
        damage = Core.damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ComplexBullet")
        {
            if (lives <= 1)
            {
                ScoreManager.instance.IncreaseScore();
                Destroy(collision.gameObject);
                if (PlayerPrefs.GetInt("ChosenShip") == 1 && PlayerPrefs.GetInt("ImmortalStars") == 4)
                {
                    killsBeforeHeal += 1;
                }
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
                if (PlayerPrefs.GetInt("ChosenShip") == 1 && PlayerPrefs.GetInt("ImmortalStars") == 4)
                {
                    killsBeforeHeal += 1;
                }
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
            if (lives <= 1)
            {
                ScoreManager.instance.IncreaseScore();
                Destroy(collision.gameObject);
                if (PlayerPrefs.GetInt("ChosenShip") == 1 && PlayerPrefs.GetInt("ImmortalStars") == 4)
                {
                    killsBeforeHeal += 1;
                }
                
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
                if (PlayerPrefs.GetInt("ChosenShip") == 1 && PlayerPrefs.GetInt("ImmortalStars") == 4)
                {
                    killsBeforeHeal += 1;
                }
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

    // Update is called once per frame
    void Update()
    {
        if (killsBeforeHeal >= 30)
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
