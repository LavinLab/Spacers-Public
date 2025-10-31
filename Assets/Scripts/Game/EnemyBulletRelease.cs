using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletRelease : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float minTime = 1f;
    public float maxTime = 5f;
    public static List<EnemyBulletRelease> allEnemies = new List<EnemyBulletRelease>();
    public static List<GameObject> enemy = new List<GameObject>();
    public static bool shoot = false;
    void Start()
    {
        allEnemies.Add(this);
        enemy.Add(gameObject);
    }

    public IEnumerator ShootBullet()
    {
        while (shoot)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));

            Vector3 spawnPosition = transform.position;
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            bullet.GetComponent<EnemyBulletShoot>().ShootAtPlayer();
        }
    }
    private void OnDestroy()
    {
        allEnemies.Remove(this);
        enemy.Remove(gameObject);
    }
}
