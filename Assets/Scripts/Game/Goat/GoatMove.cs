using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class GoatMove : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 targetPosition;
    private Vector3 centerPosition;
    private float minWaitTime = 0.4f;
    private float maxWaitTime = 1f;
    public bool isFirstPhase = true;
    public bool shouldStart = true;
    Vector3 direction;
    GameObject player;
    public static GoatMove instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        centerPosition = transform.position;
        targetPosition = GetRandomPosition();
    }

    void Update()
    {
        if (isFirstPhase)
        {
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                targetPosition = GetRandomPosition();
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            if (shouldStart)
            {
                StartCoroutine(SecondPhase());
                shouldStart = false;
            }
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }


    IEnumerator SecondPhase()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        targetPosition = player.transform.position;
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * 2.5f * Time.deltaTime);
            yield return null;
        }
        shouldStart = true;
    }
    

    Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 2f;
        return centerPosition + new Vector3(randomDirection.x, randomDirection.y, 0);
    }
}
