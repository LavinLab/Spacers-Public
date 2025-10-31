using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPhase : MonoBehaviour
{
    public static SecondPhase instance;
    [SerializeField] private float movementSpeed = 0.5f; // Adjust movement speed
    [SerializeField] private float projectileSpeed = 5f; // Adjust projectile speed
    [SerializeField] private GameObject projectilePrefab; // Assign the projectile prefab
    [SerializeField] private float shootingInterval = 360f; // Adjust shooting interval
    private Transform playerTransform;
    [SerializeField] Material MagnumMaterial;
    private float timer;
    private Vector3 direction = Vector3.right; // Initial direction
    private Camera mainCamera;
    private bool isChanged = false;

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
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        mainCamera = Camera.main; // Get the main camera
        isChanged = false;
        MagnumDie.instance.animator.SetBool("IsOpenEye", true);
        GetComponent<SpriteRenderer>().material = MagnumMaterial;
    }

    void Update()
    {
        if(MagnumDie.instance.lives <= MagnumDie.startLives / 4 && !isChanged)
        {
            isChanged = true;
            movementSpeed *= 2f;
            shootingInterval /= 2f;
        }
        transform.position += direction/2 * movementSpeed * Time.deltaTime;

        // Check for camera bounds and reverse direction
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);
        if (viewportPos.x < 0 || viewportPos.x > 1)
        {
            direction = -direction;
        }

        // Shooting projectiles towards the player
        timer += Time.deltaTime;
        if (timer >= shootingInterval)
        {
            timer = 0;

            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
            projectile.GetComponent<Rigidbody2D>().linearVelocity = directionToPlayer * projectileSpeed;

            // Rotate projectile to face the player (assuming it has a SpriteRenderer)
            float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            projectile.GetComponent<SpriteRenderer>().flipX = angleToPlayer > 90 || angleToPlayer < -90;
        }
    }
    private void OnDestroy()
    {
        isChanged = false;
    }
}