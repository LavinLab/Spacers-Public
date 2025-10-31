using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5BulletShoot : MonoBehaviour
{
    public float speed = 1f;
    public float homingRange = 10f; // adjust this value to control how far the bullet will chase the player
    private Vector3 direction;
    private GameObject player;
    [SerializeField] Rigidbody2D rb;

    void Start()
    {
        
    }

    public void ShootAtPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            StartCoroutine(ChasePlayer());
        }
    }

    private IEnumerator ChasePlayer()
    {
        while (Vector3.Distance(transform.position, player.transform.position) > homingRange)
        {
            direction = (player.transform.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction * -1);
            yield return null; // wait for the next frame
        }

        // if the bullet reaches the homing range, give it a final boost towards the player
        rb.linearVelocity = direction * speed * 2f;
    }
}