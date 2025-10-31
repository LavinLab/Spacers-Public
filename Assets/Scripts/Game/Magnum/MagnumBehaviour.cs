using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnumBehaviour : MonoBehaviour
{
    public float speed = 5f; // Adjustable speed for the object
    public static MagnumBehaviour instance;
    private Vector3 currentDirection;
    private Camera mainCamera;
    private int[] Degrees = { -45, 45 };
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
        mainCamera = Camera.main; // Get the main camera reference
        RandomizeDirection(); // Set a random initial direction
    }

    void Update()
    {
        // Move the object in the current direction
        transform.position += currentDirection * speed * Time.deltaTime;

        // Check for collisions with camera boundaries
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
        if (viewportPosition.x < 0 || viewportPosition.x > 1 ||
            viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            // Reflect the direction based on the collided boundary
            if (viewportPosition.x < 0 || viewportPosition.x > 1)
            {
                currentDirection.x *= -1;
            }
            if (viewportPosition.y < 0 || viewportPosition.y > 1)
            {
                currentDirection.y *= -1;
            }
        }
    }

    void RandomizeDirection()
    {
        // Generate a random angle between -45 and 45 degrees
        float angle = Degrees[Random.Range(0, Degrees.Length)];

        // Convert the angle to radians
        float radians = angle * Mathf.Deg2Rad;

        // Calculate the direction vector
        currentDirection = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0);
    }
}