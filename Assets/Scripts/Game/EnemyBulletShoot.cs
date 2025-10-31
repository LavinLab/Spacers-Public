using UnityEngine;

public class EnemyBulletShoot : MonoBehaviour
{
    public float speed = 10f;

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
