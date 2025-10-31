using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Attack : MonoBehaviour
{
    public float speed = 10f; // �������� �����
    public float returnSpeed = 5f; // �������� �����������
    public float waitTime = 1f; // ����� �������� �� �����
    private Vector3 startPosition; // ��������� �������
    private GameObject player; // ������ ������
    private Vector3 targetPosition; // ������� ����
    private bool isAttacking = false; // ������� �� ����
    private bool isReturning = false; // ������������ �� ����

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position; // ��������� ��������� �������
        player = GameObject.FindGameObjectWithTag("Player"); // ������� ������
        StartCoroutine(AttackPlayer()); // �������� ������� �����
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            // ��������� � ������
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            // ���� �������� ������, �������� ��������
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
            // ������������ �� ��������� �������
            transform.position = Vector3.MoveTowards(transform.position, startPosition, returnSpeed * Time.deltaTime);
            // ���� ���������, �������� ����� �����
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
            StopAllCoroutines(); // ������������� ��� ��������
            isAttacking = false;
            isReturning = true;
            returnSpeed = 20f;
        }
    }
    IEnumerator AttackPlayer()
    {
        // ���� ��������� �����
        yield return new WaitForSeconds(Random.Range(5, 10));
        targetPosition = player.transform.position; // ���������� ������� ������
        isAttacking = true;
    }

    IEnumerator WaitAtPlayer()
    {
        // ���� �� �����
        yield return new WaitForSeconds(waitTime);
        isReturning = true;
    }
}
