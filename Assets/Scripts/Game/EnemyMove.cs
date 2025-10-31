using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 0.5f;
    private Vector3 targetPosition;
    private Vector3 centerPosition;

    void Start()
    {
        centerPosition = transform.position;
        targetPosition = GetRandomPosition();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = GetRandomPosition();
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 0.3f;
        return centerPosition + new Vector3(randomDirection.x, randomDirection.y, 0);
    }
}
