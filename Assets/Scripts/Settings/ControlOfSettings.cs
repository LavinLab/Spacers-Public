using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlOfSettings : MonoBehaviour
{
    public GameObject InfoFrame;
    Vector3 originalPosition;
    public GameObject EnglishChosen, RussianChosen;
    public Text SettingsText, CloudSave, SaveGame, LoadGame, Language, MaxScore, Score, Wave;
    public static ControlOfSettings instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("Language", 0) == 0)
        {
            EnglishChosen.SetActive(true);
            RussianChosen.SetActive(false);
            SettingsText.text = "Settings";
            CloudSave.text = "Cloud save";
            SaveGame.text = "Save game";
            LoadGame.text = "Load game";
            Language.text = "Language";
        }
        else if (PlayerPrefs.GetInt("Language", 0) == 1)
        {
            EnglishChosen.SetActive(false);
            RussianChosen.SetActive(true);
            SettingsText.text = "Настройки";
            CloudSave.text = "Сохранения в облаке";
            SaveGame.text = "Сохранить игру";
            LoadGame.text = "Загрузить игру";
            Language.text = "Язык";
        }
    }

    public void EnglishButton()
    {
        PlayerPrefs.SetInt("Language", 0);
        EnglishChosen.SetActive(true);
        RussianChosen.SetActive(false);
        SettingsText.text = "Settings";
        CloudSave.text = "Cloud save";
        SaveGame.text = "Save game";
        LoadGame.text = "Load game";
        Language.text = "Language";
        MaxScore.text = "Max score: " + PlayerPrefs.GetInt("MaxScore", 0);
        Score.text = "Score: " + ScoreManager.instance.count;
        Wave.text = "Wave: " + ChangeWave.count;
    }

    public void RussianButton()
    {
        PlayerPrefs.SetInt("Language", 1);
        EnglishChosen.SetActive(false);
        RussianChosen.SetActive(true);
        SettingsText.text = "Настройки";
        CloudSave.text = "Сохранения в облаке";
        SaveGame.text = "Сохранить игру";
        LoadGame.text = "Загрузить игру";
        Language.text = "Язык";
        MaxScore.text = "Рекорд: " + PlayerPrefs.GetInt("MaxScore", 0);
        Score.text = "Счёт: " + ScoreManager.instance.count;
        Wave.text = "Волна: " + ChangeWave.count;
    }


    public void OpenSettings()
    {
        if (!InfoFrame.activeSelf)
        {
            StartCoroutine(ShowInfo());
        }
        
    }
    public void CloseSettings()
    {
        StartCoroutine(HideInfo());

    }
    IEnumerator ShowInfo()
    {
        InfoFrame.SetActive(true);
        originalPosition = InfoFrame.transform.position;

        float moveSpeed = 20f;
        Vector3 centerPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10f));

        while (Vector3.Distance(InfoFrame.transform.position, centerPosition) > 0.01f)
        {
            InfoFrame.transform.position = Vector3.MoveTowards(InfoFrame.transform.position, centerPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator HideInfo()
    {
        float moveSpeed = 20f;
        Vector3 centerPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10f));

        while (Vector3.Distance(InfoFrame.transform.position, originalPosition) > 0.01f)
        {
            InfoFrame.transform.position = Vector3.MoveTowards(InfoFrame.transform.position, originalPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        InfoFrame.SetActive(false);
    }
}
