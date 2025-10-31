using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mistle : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    private float shootInterval = 1.5f;
    private float timer;


    private void Start()
    {
        
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > shootInterval)
        {
            Shoot();
            timer = 0;
        }
    }
    void Shoot()
    {
        if(gameObject.tag == "MistlesShoot")
        {
            Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        }
    }
}
