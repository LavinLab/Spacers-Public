using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.UI;

public class CoinCollect : MonoBehaviour
{
    public static CoinCollect Instance;
    public GameObject coinPrefab; // ������ �� ��� ������
    public Transform characterTransform; // ������ �� Transform ������ ���������
    public GameObject coinObject;
    private bool coinActive = false;
    private Coroutine CoinCoroutine;
    const string leaderBoardId = "CgkIl9XD1bEWEAIQAw";
    private void Awake()
    {
        Instance=this;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            if (coinActive)
            {
                StopCoroutine(CoinCoroutine);
                Destroy(coinObject);
            }
            CoinCoroutine = StartCoroutine(Coin());
            Destroy(collision.gameObject);
            Social.ReportScore(PlayerPrefs.GetInt("Money"), leaderBoardId, (bool success) => { });
        }
    }

    public IEnumerator Coin()
    {
        coinActive = true;
        Vector3 spawnPosition = characterTransform.position + new Vector3(0, 0.7f, 0);
        coinObject = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 1);
        coinObject.tag = "ToDelete";
        coinObject.GetComponentInChildren<Text>().text = "";
        while (coinObject.transform.position.y < spawnPosition.y + 0.3f)
        {
            coinObject.transform.position += new Vector3(0, 0.1f, 0);
            yield return null;
        }
        for (int i = 0; i < 30; i++)
        {
            coinObject.transform.Rotate(0, 360 * Time.deltaTime, 0);
            yield return null;
        }
        Destroy(coinObject);

        coinActive = false;
    }
}
