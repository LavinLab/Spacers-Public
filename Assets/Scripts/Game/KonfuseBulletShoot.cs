using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonfuseBulletShoot : MonoBehaviour
{
    public float speed = 2f;

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
}
