using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGoatBehaviour : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject bulletPrefab;
    public float speed = 6f;
    public float minTime = 1f;
    public float maxTime = 3f;
    public static List<SmallGoatBehaviour> allSmallGoats = new List<SmallGoatBehaviour>();
    public static List<GameObject> enemySmallGoats = new List<GameObject>();
    public static SmallGoatBehaviour instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        allSmallGoats.Add(this);
        enemySmallGoats.Add(gameObject);
        StartCoroutine(ShootBullet(true));
    }
    public IEnumerator ShootBullet(bool start)
    {
        while (start)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));

            Vector3 spawnPosition = transform.position;
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            ShootAtPlayer(bullet);
        }
    }

    public void ShootAtPlayer(GameObject bullet)
    {
        GameObject playerToFollow = GameObject.FindGameObjectWithTag("Player");
        if (playerToFollow != null)
        {
            Vector3 direction = (playerToFollow.transform.position - bullet.transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized * -1;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    private void OnDestroy()
    {
        allSmallGoats.Remove(this);
        enemySmallGoats.Remove(gameObject);
    }
}
