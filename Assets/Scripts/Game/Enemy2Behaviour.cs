using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Behaviour : MonoBehaviour
{
    public static List<GameObject> enemy = new List<GameObject>();
    public float speed = 0.5f;
    private Vector3 targetPosition;
    private Vector3 centerPosition; // Добавлено: переменная для хранения изначального поворота

    void Start()
    {
        enemy.Add(gameObject);
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
    
    private void OnDestroy()
    {
        enemy.Remove(gameObject);
    }
}
