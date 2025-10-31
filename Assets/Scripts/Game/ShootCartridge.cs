using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ShootCartridge : MonoBehaviour
{
    public GameObject classicPrefab;
    public GameObject immortalPrefab;
    public GameObject fulminantPrefab;
    public GameObject firethrowerPrefab;
    public GameObject coinPrefab;
    private GameObject bulletPrefab;
    public Sprite[] fires;
    public Transform shootPoint;
    public static float shootInterval;
    private float timer;
    private GameObject bullet;
    public static ShootCartridge instance;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        if (PlayerPrefs.HasKey("ChosenShip"))
        {
            if (PlayerPrefs.GetInt("ChosenShip") == 0)
            {
                bulletPrefab = classicPrefab;
                if (PlayerPrefs.GetInt("ClassicStars") == 0)
                {
                    shootInterval = 1f;
                }
                else if (PlayerPrefs.GetInt("ClassicStars") == 1)
                {
                    shootInterval = 1f;
                }
                else if (PlayerPrefs.GetInt("ClassicStars") == 2)
                {
                    shootInterval = 0.5f;
                }
                else if (PlayerPrefs.GetInt("ClassicStars") == 3 || PlayerPrefs.GetInt("ClassicStars") == 4)
                {
                    shootInterval = 0.5f;
                }
            }
            else if (PlayerPrefs.GetInt("ChosenShip") == 1)
            {
                bulletPrefab = immortalPrefab;
                if (PlayerPrefs.GetInt("ImmortalStars") == 0)
                {
                    shootInterval = 0.75f;
                }
                else if (PlayerPrefs.GetInt("ImmortalStars") == 1)
                {
                    shootInterval = 0.5f;
                }
                else if (PlayerPrefs.GetInt("ImmortalStars") == 2)
                {
                    shootInterval = 0.5f;
                }
                else if (PlayerPrefs.GetInt("ImmortalStars") == 3 || PlayerPrefs.GetInt("ImmortalStars") == 4)
                {
                    shootInterval = 0.5f;
                }
            }
            else if (PlayerPrefs.GetInt("ChosenShip") == 2)
            {
                bulletPrefab = fulminantPrefab;
                if (PlayerPrefs.GetInt("FulminantStars") == 0)
                {
                    shootInterval = 2f;
                }
                else if (PlayerPrefs.GetInt("FulminantStars") == 1)
                {
                    shootInterval = 2f;
                }
                else if (PlayerPrefs.GetInt("FulminantStars") == 2)
                {
                    shootInterval = 1f;
                }
                else if (PlayerPrefs.GetInt("FulminantStars") == 3 || PlayerPrefs.GetInt("FulminantStars") == 4)
                {
                    shootInterval = 0.5f;
                }
            }
            else if (PlayerPrefs.GetInt("ChosenShip") == 3)
            {
                bulletPrefab = coinPrefab;
                shootInterval = 1f;
            }
            else if (PlayerPrefs.GetInt("ChosenShip") == 4)
            {
                bulletPrefab = firethrowerPrefab;
                if (PlayerPrefs.GetInt("FirethrowerStars") == 0 || PlayerPrefs.GetInt("FirethrowerStars") == 1)
                {
                    shootInterval = 3f;
                }
                else if(PlayerPrefs.GetInt("FirethrowerStars") == 2 || PlayerPrefs.GetInt("FirethrowerStars") == 3 || PlayerPrefs.GetInt("FirethrowerStars") == 4)
                {
                    shootInterval = 2f;
                }
            }
            
        }
        else
        {
            bulletPrefab = classicPrefab;
            shootInterval = 1f;
        }
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > shootInterval)
        {
            Shoot();
            timer = 0;
        }
    }

    void Shoot()
    {
        if(PlayerPrefs.GetInt("ChosenShip") == 4)
        {
            StartCoroutine(FireLine());
        }
        else
        {
            bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        }
        
        if (PlayerPrefs.GetInt("ChosenShip") == 3)
        {
            bullet.tag = "Bullet";
            bullet.GetComponentInChildren<Text>().text = "";
            bullet.AddComponent<CartridgeMove>();
        }
    }

    IEnumerator FireLine()
    {
        for(int i = 1; i < 4; i++)
        {
            bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            bullet.GetComponentInChildren<Light2D>().intensity = i * 3;
            bullet.GetComponent<SpriteRenderer>().sprite = fires[i-1];
            yield return new WaitForSeconds(0.05f);
        }
    }
}
