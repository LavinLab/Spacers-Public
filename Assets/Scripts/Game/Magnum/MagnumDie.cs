using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnumDie : MonoBehaviour
{
    public static MagnumDie instance;
    public float lives;
    public static float startLives;
    public GameObject[] effectPrefabs;
    private float damage; // Array for your effect prefabs
    public Animator animator;
    public GameObject prefabToSpawn;
    [SerializeField] Sprite Magnum2;
    [SerializeField] Sprite Magnum3;
    private int healCount = 0;
    public bool canAttack = true;
    public GameObject HealPrefab;
    GameObject healHeart;
    private float damageThreshold;
    public GameObject coin;
    public GameObject crystall;
    [SerializeField] Image healthBar;
    [SerializeField] Text healthText;

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
    // Flag to track if the coroutine is already running
    private bool secondPhaseRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        Core.GetDamage();
        damage = Core.damage;
        lives = ChangeWave.count * 5 * damage;
        startLives = lives;
        healthText.text = lives.ToString();
        damageThreshold = lives / (ChangeWave.count * damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (canAttack)
            {
                if (lives <= 1)
                {
                    ScoreManager.instance.count += 99;
                    ScoreManager.instance.IncreaseScore();
                    Destroy(collision.gameObject);
                    SpawnCrystal();
                    SpawnCoins();
                    SecondPhase.instance.enabled = false;
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(collision.gameObject);
                    lives -= damage;
                }
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.tag == "FireBullet")
        {
            if (canAttack)
            {
                if (lives <= 1)
                {
                    ScoreManager.instance.count += 99;
                    ScoreManager.instance.IncreaseScore();
                    SpawnCrystal();
                    SpawnCoins();
                    SecondPhase.instance.enabled = false;
                    Destroy(gameObject);
                }
                else
                {
                    lives -= damage;
                }
            }
        }
        if (collision.gameObject.tag == "AddFireBullet")
        {
            if (canAttack)
            {
                if (lives <= 1)
                {
                    ScoreManager.instance.count += 99;
                    ScoreManager.instance.IncreaseScore();
                    SpawnCrystal();
                    SpawnCoins();
                    SecondPhase.instance.enabled = false;
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(collision.gameObject);
                    lives -= 0.5f;
                }
            }
        }
        if (collision.gameObject.tag == "MistlesBullet")
        {
            if (canAttack)
            {
                if (lives <= 1)
                {
                    ScoreManager.instance.count += 99;
                    ScoreManager.instance.IncreaseScore();
                    Destroy(collision.gameObject);
                    SpawnCrystal();
                    SpawnCoins();
                    SecondPhase.instance.enabled = false;
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(collision.gameObject);
                    lives -= (float)damage * 0.2f;
                }
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.tag == "AimBullet")
        {
            if (canAttack)
            {
                if (lives <= 1)
                {
                    ScoreManager.instance.count += 99;
                    ScoreManager.instance.IncreaseScore();
                    Destroy(collision.gameObject);
                    SpawnCrystal();
                    SpawnCoins();
                    SecondPhase.instance.enabled = false;
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(collision.gameObject);
                    lives -= (float)damage * 0.5f;
                }
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
        if (lives > 0 && canAttack && healCount < 5) // Check if Magnum is alive and can be attacked
        {
            // Check if damage taken exceeds the threshold and healHeart doesn't exist
            if (lives <= startLives - damageThreshold * healCount && healHeart == null)
            {
                GiveHeal();
                healCount++;
            }
        }
    }

    public IEnumerator TheSecondFaze()
    {
        MagnumBehaviour.instance.enabled = false;
        canAttack = false;
        float speed = 10f;
        // Move to the target position
        Vector3 targetPosition = new Vector3(0f, 3f, transform.position.z);
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f) // Adjust threshold as needed
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);// Wait for next frame
        }

        // Deactivate MagnumBehaviour

        // Spawn random effects for 1 second
        for (int i = 0; i < 60; i++) // Adjust the number of effects as needed
        {
            int randomIndex = Random.Range(0, effectPrefabs.Length);
            Vector3 randomPosition = RandomPointInSpriteBounds();
            GameObject effect = Instantiate(effectPrefabs[randomIndex], randomPosition, Quaternion.identity);

            // Apply random upward force
            Rigidbody2D rb = effect.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(1f, 3f)), ForceMode2D.Impulse);
            }
            if (gameObject.activeSelf)
            {
                StartCoroutine(ShakeObject(0.2f, 0.1f));
            }
            yield return new WaitForSeconds(0.1f); // Adjust the delay between effects
        }
        yield return new WaitForSeconds(1f);
        // Change sprite (assuming you have a SpriteRenderer component)
        GetComponent<SpriteRenderer>().sprite = Magnum2;

        // Spawn 21 objects below the current object
        for (int i = 0; i < 21; i++)
        {
            float xPos = Mathf.Lerp(-1f, 1f, (float)i / 20f); // Evenly distribute along x-axis
            Vector3 spawnPosition = transform.position + new Vector3(xPos, -0.5f, 0);
            GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

            // Apply upward force
            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(Vector2.up * 2f, ForceMode2D.Impulse); // Adjust force as needed
            }

            if (gameObject.activeSelf)
            {
                StartCoroutine(ShakeObject(0.2f, 0.1f)); // Adjust duration and intensity as needed
            }
            
        }
        GetComponent<SpriteRenderer>().sprite = Magnum3;
        // Activate animator after 0.5 seconds
        yield return new WaitForSeconds(1f);
        animator.enabled = true;
        yield return new WaitForSeconds(0.2f);
        SecondPhase.instance.enabled = true;
        yield return new WaitForSeconds(0.2f);
        canAttack = true;
    }
    IEnumerator ShakeObject(float duration, float intensity)
    {
        float elapsedTime = 0f;
        Vector3 originalPosition = transform.position;

        while (elapsedTime < duration)
        {
            float xOffset = Random.Range(-intensity, intensity);
            transform.position = originalPosition + new Vector3(xOffset, 0f, 0f);
            elapsedTime += Time.deltaTime;
            
            yield return new WaitForSeconds(0.05f);
        }
        transform.position = originalPosition;

        // Reset to original position
    }
    // Function to get a random point within the sprite's bounds
    Vector3 RandomPointInSpriteBounds()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Bounds bounds = spriteRenderer.bounds;
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            transform.position.z
        );
    }
    // ������� ��� �������� ��������
    void SpawnCoins()
    {
        int numberOfCoins = ChangeWave.count;
        GameObject actualCoin = null;
        for (int i = 0; i < numberOfCoins; i++)
        {
            actualCoin = Instantiate(coin, transform.position, Quaternion.identity);
            actualCoin.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -4);
        }
        if (actualCoin != null)
        {
            actualCoin.GetComponentInChildren<Text>().text = "x" + numberOfCoins;
        }
    }
    void SpawnCrystal()
    {
        int numberOfCrystalls = Random.Range(0, 6);
        GameObject actualCrystall = null;
        for (int i = 0; i < numberOfCrystalls; i++)
        {
            actualCrystall = Instantiate(crystall, transform.position, Quaternion.identity);
            actualCrystall.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -6);
        }
        if (actualCrystall != null)
        {
            actualCrystall.GetComponentInChildren<Text>().text = "x" + numberOfCrystalls;
        }
    }
    void GiveHeal()
    {
        healHeart = Instantiate(HealPrefab, transform.position, Quaternion.identity); // �������� �������
        healHeart.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -4);
    }
    private void Update()
    {
        healthBar.fillAmount = lives / startLives;
        healthText.text = System.Convert.ToInt32(lives).ToString();
        if (lives <= startLives * 0.5f && !secondPhaseRunning) // Check if coroutine is not already running
        {
            secondPhaseRunning = true;
            StartCoroutine(TheSecondFaze());
        }
    }
    void OnDestroy()
    {
        secondPhaseRunning = false; // Reset flag when Magnum is destroyed
    }
}
