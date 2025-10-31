using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimEffect : MonoBehaviour
{
    public GameObject aimPrefab; // ������ �� ��� ������
    public GameObject bulletPrefab; // ������ �� ������ ����
    public Transform characterTransform;
    public static GameObject aimObject;// ������ �� Transform ������ ���������

    private bool AimActive = false;
    private Coroutine AimCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Aim")
        {
            if (AimActive)
            {
                StopCoroutine(AimCoroutine);
                Destroy(aimObject);
            }
            AimCoroutine = StartCoroutine(Aim());
            Destroy(collision.gameObject);
        }
    }

    public IEnumerator Aim()
    {
        AimActive = true;

        Vector3 spawnPosition = characterTransform.position + new Vector3(0, 0.9f, 0);
        aimObject = Instantiate(aimPrefab, spawnPosition, Quaternion.identity);
        aimObject.tag = "ToDelete";
        aimObject.transform.localScale *= 2;
        while (aimObject.transform.position.y < spawnPosition.y + 1f)
        {
            aimObject.transform.position += new Vector3(0, 1f, 0) * Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < 180; i++)
        {
            aimObject.transform.Rotate(0, 0, 50 * Time.deltaTime);

            GameObject bullet = Instantiate(bulletPrefab, aimObject.transform.position, Quaternion.Euler(0, 0, i * 20));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = bullet.transform.up * 15f;
            yield return null;
        }

        Destroy(aimObject);

        AimActive = false;
    }
}