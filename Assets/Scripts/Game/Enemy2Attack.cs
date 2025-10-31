using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Attack : MonoBehaviour
{
    public float speed = 10f; // скорость врага
    public float returnSpeed = 5f; // скорость возвращения
    public float waitTime = 1f; // время ожидания на месте
    private Vector3 startPosition; // начальная позиция
    private GameObject player; // объект игрока
    private Vector3 targetPosition; // позиция цели
    private bool isAttacking = false; // атакует ли враг
    private bool isReturning = false; // возвращается ли враг

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position; // сохраняем начальную позицию
        player = GameObject.FindGameObjectWithTag("Player"); // находим игрока
        StartCoroutine(AttackPlayer()); // начинаем процесс атаки
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            // двигаемся к игроку
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            // если достигли игрока, начинаем ожидание
            if (transform.position == targetPosition)
            {
                isAttacking = false;
                if (!isReturning)
                {
                    StartCoroutine(WaitAtPlayer());
                }
            }
        }
        else if (isReturning)
        {
            // возвращаемся на начальную позицию
            transform.position = Vector3.MoveTowards(transform.position, startPosition, returnSpeed * Time.deltaTime);
            // если вернулись, начинаем новую атаку
            if (transform.position == startPosition)
            {
                isReturning = false;
                returnSpeed = 10f;
                StartCoroutine(AttackPlayer());
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ActualShield")
        {
            StopAllCoroutines(); // останавливаем все корутины
            isAttacking = false;
            isReturning = true;
            returnSpeed = 20f;
        }
    }
    IEnumerator AttackPlayer()
    {
        // ждем рандомное время
        yield return new WaitForSeconds(Random.Range(5, 10));
        targetPosition = player.transform.position; // определяем позицию игрока
        isAttacking = true;
    }

    IEnumerator WaitAtPlayer()
    {
        // ждем на месте
        yield return new WaitForSeconds(waitTime);
        isReturning = true;
    }
}
