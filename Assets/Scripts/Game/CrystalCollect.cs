using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalCollect : MonoBehaviour
{
    public static CrystalCollect Instance;
    public GameObject crystallPrefab; // ссылка на ваш префаб
    public Transform characterTransform; // ссылка на Transform вашего персонажа
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

        // Создаем объект на основе префаба чуть выше персонажа
        Vector3 spawnPosition = characterTransform.position + new Vector3(0, 0.7f, 0);
        crystallObject = Instantiate(crystallPrefab, spawnPosition, Quaternion.identity);
        PlayerPrefs.SetInt("Crystall", PlayerPrefs.GetInt("Crystall") + 1);
        crystallObject.tag = "ToDelete";
        crystallObject.GetComponentInChildren<Text>().text = "";
        // Плавно поднимаем объект на 0.1 единицу мира вверх
        while (crystallObject.transform.position.y < spawnPosition.y + 0.3f)
        {
            crystallObject.transform.position += new Vector3(0, 0.1f, 0);
            yield return null;
        }

        // Плавно поворачиваем объект вокруг своей оси
        for (int i = 0; i < 30; i++)
        {
            crystallObject.transform.Rotate(0, 360 * Time.deltaTime, 0);
            yield return null;
        }

        // Уничтожаем объект
        Destroy(crystallObject);

        crystallActive = false;
    }
}
