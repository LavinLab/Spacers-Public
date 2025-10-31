using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3BulletShoot : MonoBehaviour
{
    public float speed = 10f;
    public float growthRate = 3f; // �������� ����� �������

    void Start()
    {
    }

    public void ShootAtPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }
    private void Update()
    {
        gameObject.transform.localScale += new Vector3(growthRate, growthRate, growthRate) * Time.deltaTime;
    }
}
