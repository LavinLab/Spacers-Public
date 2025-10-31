using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGunBehaviour : MonoBehaviour
{
    [SerializeField] float offset;
    [SerializeField] float speed;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] string[] enemyTags = { "Enemy", "Konfuse", "SmallGoat", "BigGoat", "Magnum" };

    private GameObject player;
    private List<GameObject> enemies = new List<GameObject>();
    public bool shoot = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Shoot());
    }

    private void OnEnable()
    {
        shoot = true;
        StartCoroutine(Shoot());
    }

    private Vector3 ChooseEnemyPos()
    {
        enemies.Clear();
        foreach (string tag in enemyTags)
        {
            if(GameObject.FindGameObjectsWithTag(tag) != null)
            {
                enemies.AddRange(GameObject.FindGameObjectsWithTag(tag));
            }
        }
        if(enemies.Count <= 0)
        {
            return Vector3.up;
        }
        else
        {
            return enemies[Random.Range(0, enemies.Count - 1)].transform.position;
        }
    }

    private void Update()
    {
        Vector3 playerPos = player.transform.position;
        transform.position = new Vector3(playerPos.x - offset, playerPos.y, playerPos.z);
    }

    public IEnumerator Shoot()
    {
        while (shoot)
        {
            Vector3 direction = (ChooseEnemyPos() - transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * speed * Time.deltaTime;
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    private void OnDisable()
    {
        shoot = false;
        StopAllCoroutines();
    }
}