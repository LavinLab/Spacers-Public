using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlOfShop : MonoBehaviour
{
    public static ControlOfShop instance;
    // Группировка UI элементов для удобного доступа
    [SerializeField] Text moneyText;
    [SerializeField] Text[] costTexts;
    [SerializeField] Text[] haveTexts;

    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject Ad;
    [SerializeField] GameObject Thanks;
    [SerializeField] GameObject PayButton;
    [SerializeField] GameObject ViewPort;
    public int dayCount;
    public Text dayCountText;
    public Text UpgradeText, SpeedUpDur, MaxSpeedUp, ShieldDur, MaxShield, MistlesDur, MaxMistles, Lives, MaxLives, Chanse, MaxChanse, goldenThanks;

    private byte closeCount = 1;

    // Определение типов улучшений и их стоимости
    readonly string[] upgradeNames = { "SpeedUp", "Shield", "Mistles", "Heart", "Chanse" };
    readonly int[] baseCosts = { 10, 10, 10, 20, 40 };

    // Использование словаря для данных об улучшениях
    Dictionary<string, UpgradeData> upgradeData;

    // Определение максимального количества покупок для каждого улучшения
    readonly Dictionary<string, int> maxPurchases = new Dictionary<string, int>()
    {
        { "SpeedUp", 10 },
        { "Shield", 10 },
        { "Mistles", 10 },
        { "Heart", 3},
        { "Chanse", 5 }
        // ... Добавьте другие улучшения и их максимальное количество ...
    };

    // Объект "Недостаточно монет"
    [SerializeField] GameObject notEnoughCoinsObject;
    [SerializeField] Text notEnoughCoinsText;

    public int moneycount;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("Language", 0) == 0)
        {
            goldenThanks.text = "Thanks";
            UpgradeText.text = "Upgrades";
            SpeedUpDur.text = "SpeedUp duration";
            MaxSpeedUp.text = "Max: 10";
            ShieldDur.text = "Shield duration";
            MaxShield.text = "Max: 10";
            MistlesDur.text = "Rockets duration";
            MaxMistles.text = "Max: 10";
            Lives.text = "Lives";
            MaxLives.text = "Max: 3";
            Chanse.text = "Chances";
            MaxChanse.text = "Max: 5";
        }
        else if (PlayerPrefs.GetInt("Language", 0) == 1)
        {
            goldenThanks.text = "Спасибо";
            UpgradeText.text = "Улучшения";
            SpeedUpDur.text = "Длительность ускорения";
            MaxSpeedUp.text = "Макс: 10";
            ShieldDur.text = "Длительность щита";
            MaxShield.text = "Макс: 10";
            MistlesDur.text = "Длительность ракетниц";
            MaxMistles.text = "Макс: 10";
            Lives.text = "Жизни";
            MaxLives.text = "Макс: 3";
            Chanse.text = "Шансы";
            MaxChanse.text = "Макс: 5";
        }
        AdjustViewportHeight();
        // Инициализация данных об улучшениях
        upgradeData = new Dictionary<string, UpgradeData>();
        for (int i = 0; i < upgradeNames.Length; i++)
        {
            upgradeData.Add(upgradeNames[i], new UpgradeData(baseCosts[i]));
        }

        // Загрузка сохраненных данных
        LoadUpgradeData();

        // Обновление UI
        UpdateMoneyText();
        UpdateUpgradeTexts();
    }

    void AdjustViewportHeight()
    {
        float aspectRatio = (float)Screen.width / Screen.height;

        if (aspectRatio == 9f / 21f || aspectRatio == 9f / 18f || aspectRatio == 9f / 20f)
        {
            ViewPort.GetComponent<RectTransform>().sizeDelta = new Vector2(ViewPort.GetComponent<RectTransform>().sizeDelta.x, 190);
        }
        if (aspectRatio == 9f / 16f)
        {
            ViewPort.GetComponent<RectTransform>().sizeDelta = new Vector2(ViewPort.GetComponent<RectTransform>().sizeDelta.x, 165);
        }
        if (aspectRatio == 480f / 800f)
        {
            ViewPort.GetComponent<RectTransform>().sizeDelta = new Vector2(ViewPort.GetComponent<RectTransform>().sizeDelta.x, 155);
        }
    }

    public void AddCoins()
    {
        if (closeCount == 1)
        {
            shopPanel.SetActive(false);
            Ad.SetActive(true);
            if (PlayerPrefs.HasKey("IsThanks") && PlayerPrefs.GetInt("IsThanks") != 0)
            {
                Purchaser.instance.ShowThanks();

            }
            else
            {
                PayButton.SetActive(true);
            }
            if (PlayerPrefs.HasKey("DayCount"))
            {
                dayCount = PlayerPrefs.GetInt("DayCount");
            }
            else
            {
                dayCount = 5;
                PlayerPrefs.SetInt("DayCount", dayCount);
            }
            dayCountText.text = dayCount + "/5";
        }
        else
        {
            shopPanel.SetActive(true);
            Ad.SetActive(false);
            Thanks.SetActive(false);
            PayButton.SetActive(false);
        }
        closeCount = (byte)-closeCount;
    }

    public void LoadUpgradeData()
    {
        foreach (string upgradeName in upgradeNames)
        {
            if (PlayerPrefs.HasKey(upgradeName + "Cost"))
            {
                upgradeData[upgradeName].cost = PlayerPrefs.GetInt(upgradeName + "Cost");
            }
            if (PlayerPrefs.HasKey(upgradeName + "Have"))
            {
                upgradeData[upgradeName].have = PlayerPrefs.GetInt(upgradeName + "Have");
            }
        }

        if (PlayerPrefs.HasKey("Money"))
        {
            moneycount = PlayerPrefs.GetInt("Money");
        }
        else
        {
            moneycount = 0;
            PlayerPrefs.SetInt("Money", moneycount);
        }
    }

    public void UpdateMoneyText()
    {
        // Форматирование текста денег (K, M, B)
        string moneyString = FormatNumber(moneycount);
        moneyText.text = moneyString;
    }

    public void UpdateUpgradeTexts()
    {
        for (int i = 0; i < upgradeNames.Length; i++)
        {
            string upgradeName = upgradeNames[i];
            costTexts[i].text = FormatNumber(upgradeData[upgradeName].cost) + " -";

            // Обновление текста "имеется" для отображения достижения максимума
            if (upgradeData[upgradeName].have >= maxPurchases[upgradeName])
            {
                if (PlayerPrefs.GetInt("Language", 0) == 0)
                {
                    haveTexts[i].text = "MAX";
                }
                else if (PlayerPrefs.GetInt("Language", 0) == 1)
                {
                    haveTexts[i].text = "МАКС";
                }

            }
            else
            {
                haveTexts[i].text = GetHaveText(upgradeName, upgradeData[upgradeName].have);
            }
        }
    }

    string FormatNumber(int number)
    {
        if (number >= 1000000000)
        {
            return (number / 1000000000) + "B";
        }
        else if (number >= 1000000)
        {
            return (number / 1000000) + "M";
        }
        else if (number >= 1000)
        {
            return (number / 1000) + "K";
        }
        else
        {
            return number.ToString();
        }
    }

    string GetHaveText(string upgradeName, int haveValue)
    {
        switch (upgradeName)
        {
            case "SpeedUp":
            case "Shield":
            case "Mistles":
                return "+" + haveValue + "s";
            case "Heart":
                return "+" + haveValue;
            case "Chanse":
                return (haveValue + 40) + "%";
            default:
                return "";
        }
    }

    public void CloseClicked()
    {
        SceneManager.LoadScene("Game");
    }

    public void AddUpgrade(string upgradeName)
    {
        if (!upgradeData.ContainsKey(upgradeName))
        {
            Debug.LogError("Неверное название улучшения: " + upgradeName);
            return;
        }

        // Проверка достижения максимума покупок
        if (upgradeData[upgradeName].have >= maxPurchases[upgradeName])
        {
            // Показать индикацию, что достигнут максимум
            return;
        }

        if (moneycount < upgradeData[upgradeName].cost)
        {
            // Недостаточно монет!
            if (!notEnoughCoinsObject.activeSelf)
            {
                StartCoroutine(ShowNotEnoughCoins(upgradeData[upgradeName].cost - moneycount));
            }

            return;
        }

        moneycount -= upgradeData[upgradeName].cost;
        upgradeData[upgradeName].cost *= 2;
        upgradeData[upgradeName].have++;

        // Сохранение данных
        PlayerPrefs.SetInt("Money", moneycount);
        PlayerPrefs.SetInt(upgradeName + "Cost", upgradeData[upgradeName].cost);
        PlayerPrefs.SetInt(upgradeName + "Have", upgradeData[upgradeName].have);

        // Обновление UI
        UpdateMoneyText();
        UpdateUpgradeTexts();
    }

    IEnumerator ShowNotEnoughCoins(int missingCoins)
    {
        notEnoughCoinsObject.SetActive(true);
        if (PlayerPrefs.GetInt("Language", 0) == 0)
        {
            notEnoughCoinsText.text = $"You need {missingCoins} more coins!";
        }
        else if (PlayerPrefs.GetInt("Language", 0) == 1)
        {
            notEnoughCoinsText.text = $"Вам нужно ещё {missingCoins} монет!";
        }


        // Сохранение оригинальной позиции
        Vector3 originalPosition = notEnoughCoinsObject.transform.position;

        // Плавное перемещение объекта в центр
        float moveSpeed = 20f;
        Vector3 centerPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10f));

        while (Vector3.Distance(notEnoughCoinsObject.transform.position, centerPosition) > 0.01f)
        {
            notEnoughCoinsObject.transform.position = Vector3.MoveTowards(notEnoughCoinsObject.transform.position, centerPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Показать объект


        // Ожидание несколько секунд
        yield return new WaitForSeconds(2f);

        // Плавное возвращение объекта на исходную позицию
        while (Vector3.Distance(notEnoughCoinsObject.transform.position, originalPosition) > 0.01f)
        {
            notEnoughCoinsObject.transform.position = Vector3.MoveTowards(notEnoughCoinsObject.transform.position, originalPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        notEnoughCoinsObject.SetActive(false);
    }

    // Вспомогательный класс для хранения данных об улучшениях
    class UpgradeData
    {
        public int cost;
        public int have;

        public UpgradeData(int baseCost)
        {
            this.cost = baseCost;
            this.have = 0;
        }
    }
}