using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalCollect : MonoBehaviour
{
    public static CrystalCollect Instance;
    public GameObject crystallPrefab; // ������ �� ��� ������
    public Transform characterTransform; // ������ �� Transform ������ ���������
    public GameObject crystallObject;
    private bool crystallActive = false;
    private Coroutine crystallCoroutine;
    private void Awake()
    {
        Instance = this;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Crystall")
        {
            if (crystallActive)
            {
                StopCoroutine(crystallCoroutine);
                Destroy(crystallObject);
            }
            crystallCoroutine = StartCoroutine(Crystall());
            Destroy(collision.gameObject);
        }
    }

    public IEnumerator Crystall()
    {
        crystallActive = true;

        // ������� ������ �� ������ ������� ���� ���� ���������
        Vector3 spawnPosition = characterTransform.position + new Vector3(0, 0.7f, 0);
        crystallObject = Instantiate(crystallPrefab, spawnPosition, Quaternion.identity);
        PlayerPrefs.SetInt("Crystall", PlayerPrefs.GetInt("Crystall") + 1);
        crystallObject.tag = "ToDelete";
        crystallObject.GetComponentInChildren<Text>().text = "";
        // ������ ��������� ������ �� 0.1 ������� ���� �����
        while (crystallObject.transform.position.y < spawnPosition.y + 0.3f)
        {
            crystallObject.transform.position += new Vector3(0, 0.1f, 0);
            yield return null;
        }

        // ������ ������������ ������ ������ ����� ���
        for (int i = 0; i < 30; i++)
        {
            crystallObject.transform.Rotate(0, 360 * Time.deltaTime, 0);
            yield return null;
        }

        // ���������� ������
        Destroy(crystallObject);

        crystallActive = false;
    }
}
