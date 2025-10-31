using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3BulletRelease : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float minTime = 1f;
    public float maxTime = 5f;
    public static List<Enemy3BulletRelease> allEnemies = new List<Enemy3BulletRelease>();
    public static List<GameObject> enemy = new List<GameObject>();
    public SpriteRenderer spriteRenderer;
    public Sprite mouse1;
    public Sprite mouse2;
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
            bullet.GetComponent<Enemy3BulletShoot>().ShootAtPlayer();
            spriteRenderer.sprite = mouse2;
            yield return new WaitForSeconds(0.3f);
            spriteRenderer.sprite = mouse1;
        }
    }
    private void OnDestroy()
    {
        allEnemies.Remove(this);
        enemy.Remove(gameObject);
    }
}
