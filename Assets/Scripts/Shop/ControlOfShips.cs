using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlOfShips : MonoBehaviour
{
    public static ControlOfShips instance;
    public GameObject InfoFrame;
    public Image imageOfShip;
    public Sprite classicSprite;
    public Sprite immortalSprite;
    public Sprite fulminantSprite;
    public Sprite firethrowerSprite;
    public Sprite goldenWarriorSprite;
    public Text textOfShip;
    public GameObject notBoughtText;
    public GameObject[] stars;
    public Text livesText;
    public Text livesCouldBeAdded;
    public Text damageText;
    public Text damageCouldBeAdded;
    public Text shootingSpeedText;
    public Text shootingSpeedCouldBeAdded;
    Vector3 originalPosition;
    public GameObject[] couldBeAdded;
    public GameObject chooseButton;
    public Image currencyClassic;
    public Image currencyImmortal;
    public Image currencyFulminant;
    public Image currencyFirethrower;
    public GameObject buttonClassic;
    public GameObject buttonImmortal;
    public GameObject buttonFulminant;
    public GameObject buttonFirethrower;
    public Text currencyNeedClassic;
    public Text currencyNeedImmortal;
    public Text currencyNeedFulminant;
    public Text currencyNeedFirethrower;
    public Sprite coin;
    public Sprite crystall;
    public Sprite add;
    public Sprite upgrade;
    public Text crystallText;
    public GameObject firstPassive;
    public Image firstPassiveImage;
    public Sprite activeImmortal;
    public Sprite passiveImmortal;
    public GameObject secondPassive;
    public Image secondPassiveImage;
    public GameObject[] otherTexts;
    public Sprite secret;
    public Sprite damageX2;
    public Sprite bloodyHeart;
    public Sprite bulletCloned, fireSprite, addGun;
    public Text secondPassiveName;
    public Text firstPassiveName;
    public Text secondPassiveTextInfo;
    public Text firstPassiveTextInfo;
    public Text shipText;
    const string immortalAchievment = "CgkIl9XD1bEWEAIQAg";
    const string fulminantAchievment = "CgkIl9XD1bEWEAIQBA";
    const string flamethrowerAchievment = "CgkIl9XD1bEWEAIQCQ";
    public Text Ships, Damage, ShootingSpeed, Lives;

    private const int MAX_UPGRADE_LEVEL = 4; // Максимальный уровень улучшения для всех кораблей

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("Language", 0) == 0)
        {
            Ships.text = "Ships";
            Damage.text = "Damage";
            ShootingSpeed.text = "Shooting speed";
            Lives.text = "Lives";
        }
        else if (PlayerPrefs.GetInt("Language", 0) == 1)
        {
            Ships.text = "Корабли";
            Damage.text = "Урон";
            ShootingSpeed.text = "Скорость стрельбы";
            Lives.text = "Жизни";
        }

        if (PlayerPrefs.HasKey("IsThanks") && PlayerPrefs.GetInt("IsThanks") != 0)
        {
            Purchaser.instance.goldenWarrior.SetActive(true);
        }

        if (!PlayerPrefs.HasKey("ClassicStars"))
        {
            PlayerPrefs.SetInt("ClassicStars", 0);
        }

        if (!PlayerPrefs.HasKey("ChosenShip"))
        {
            PlayerPrefs.SetInt("ChosenShip", 0);
        }

        if (PlayerPrefs.HasKey("Crystall"))
        {
            crystallText.text = PlayerPrefs.GetInt("Crystall").ToString();
        }
        else
        {
            crystallText.text = "0";
            PlayerPrefs.SetInt("Crystall", 0);
        }

        LoadShipCostsFromPlayerPrefs();
        InitializeButtonStatesAndListeners();
        UpdateCurrencyTexts();
    }

    private void LoadShipCostsFromPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("ClassicCost"))
        {
            currencyNeedClassic.text = GetCostText(PlayerPrefs.GetInt("ClassicCost"), PlayerPrefs.GetInt("ClassicStars"));
        }
        else
        {
            currencyNeedClassic.text = "100";
            PlayerPrefs.SetInt("ClassicCost", 100);
        }

        if (PlayerPrefs.HasKey("ImmortalCost"))
        {
            currencyNeedImmortal.text = GetCostText(PlayerPrefs.GetInt("ImmortalCost"), PlayerPrefs.GetInt("ImmortalStars"));
        }
        else
        {
            currencyNeedImmortal.text = "50";
            PlayerPrefs.SetInt("ImmortalCost", 50);
        }

        if (PlayerPrefs.HasKey("FulminantCost"))
        {
            currencyNeedFulminant.text = GetCostText(PlayerPrefs.GetInt("FulminantCost"), PlayerPrefs.GetInt("FulminantStars"));
        }
        else
        {
            currencyNeedFulminant.text = "75";
            PlayerPrefs.SetInt("FulminantCost", 75);
        }

        if (PlayerPrefs.HasKey("FirethrowerCost"))
        {
            currencyNeedFirethrower.text = GetCostText(PlayerPrefs.GetInt("FirethrowerCost"), PlayerPrefs.GetInt("FirethrowerStars"));
        }
        else
        {
            currencyNeedFirethrower.text = "100";
            PlayerPrefs.SetInt("FirethrowerCost", 100);
        }
    }

    private void InitializeButtonStatesAndListeners()
    {
        if (PlayerPrefs.HasKey("ClassicStars"))
        {
            currencyClassic.sprite = coin;
            buttonClassic.GetComponent<Image>().sprite = upgrade;
            buttonClassic.GetComponent<Button>().onClick.AddListener(UpgradeClassic);
        }
        else
        {
            currencyClassic.sprite = crystall;
            buttonClassic.GetComponent<Image>().sprite = add;
        }

        if (PlayerPrefs.HasKey("ImmortalStars") && PlayerPrefs.GetInt("ImmortalStars") != -1)
        {
            currencyImmortal.sprite = coin;
            buttonImmortal.GetComponent<Image>().sprite = upgrade;
            buttonImmortal.GetComponent<Button>().onClick.AddListener(UpgradeImmortal);
            buttonImmortal.GetComponent<Button>().onClick.RemoveListener(AddImmortal);
        }
        else
        {
            currencyImmortal.sprite = crystall;
            buttonImmortal.GetComponent<Image>().sprite = add;
            buttonImmortal.GetComponent<Button>().onClick.RemoveListener(UpgradeImmortal);
            buttonImmortal.GetComponent<Button>().onClick.AddListener(AddImmortal);
        }

        if (PlayerPrefs.HasKey("FulminantStars") && PlayerPrefs.GetInt("FulminantStars") != -1)
        {
            currencyFulminant.sprite = coin;
            buttonFulminant.GetComponent<Image>().sprite = upgrade;
            buttonFulminant.GetComponent<Button>().onClick.AddListener(UpgradeFulminant);
            buttonFulminant.GetComponent<Button>().onClick.RemoveListener(AddFulminant);
        }
        else
        {
            currencyFulminant.sprite = crystall;
            buttonFulminant.GetComponent<Image>().sprite = add;
            buttonFulminant.GetComponent<Button>().onClick.RemoveListener(UpgradeFulminant);
            buttonFulminant.GetComponent<Button>().onClick.AddListener(AddFulminant);
        }

        if (PlayerPrefs.HasKey("FirethrowerStars") && PlayerPrefs.GetInt("FirethrowerStars") != -1)
        {
            currencyFirethrower.sprite = coin;
            buttonFirethrower.GetComponent<Image>().sprite = upgrade;
            buttonFirethrower.GetComponent<Button>().onClick.AddListener(UpgradeFirethrower);
            buttonFirethrower.GetComponent<Button>().onClick.RemoveListener(AddFirethrower);
        }
        else
        {
            currencyFirethrower.sprite = crystall;
            buttonFirethrower.GetComponent<Image>().sprite = add;
            buttonFirethrower.GetComponent<Button>().onClick.RemoveListener(UpgradeFirethrower);
            buttonFirethrower.GetComponent<Button>().onClick.AddListener(AddFirethrower);
        }
    }

    public void UpgradeClassic()
    {
        int currentStars = PlayerPrefs.GetInt("ClassicStars");
        if (currentStars < MAX_UPGRADE_LEVEL && ControlOfShop.instance.moneycount >= PlayerPrefs.GetInt("ClassicCost"))
        {
            ControlOfShop.instance.moneycount -= PlayerPrefs.GetInt("ClassicCost");
            PlayerPrefs.SetInt("Money", ControlOfShop.instance.moneycount);
            ControlOfShop.instance.UpdateMoneyText();

            int classicCost = PlayerPrefs.GetInt("ClassicCost") + 100;
            PlayerPrefs.SetInt("ClassicCost", classicCost);
            currencyNeedClassic.text = GetCostText(classicCost, PlayerPrefs.GetInt("ClassicStars"));

            PlayerPrefs.SetInt("ClassicStars", currentStars + 1);
        }
        UpdateCurrencyTexts();
    }

    public void AddImmortal()
    {
        if (PlayerPrefs.GetInt("Crystall") >= PlayerPrefs.GetInt("ImmortalCost"))
        {
            PlayGamesPlatform.Instance.ReportProgress(immortalAchievment, 100.0f, (bool success) => { });
            PlayerPrefs.SetInt("ImmortalStars", 0);
            PlayerPrefs.SetInt("Crystall", PlayerPrefs.GetInt("Crystall") - PlayerPrefs.GetInt("ImmortalCost"));
            crystallText.text = PlayerPrefs.GetInt("Crystall").ToString();
            int immortalCost = 50;
            PlayerPrefs.SetInt("ImmortalCost", immortalCost);
            currencyNeedImmortal.text = GetCostText(immortalCost, PlayerPrefs.GetInt("ImmortalStars"));

            currencyImmortal.sprite = coin;
            buttonImmortal.GetComponent<Button>().onClick.AddListener(UpgradeImmortal);
            buttonImmortal.GetComponent<Image>().sprite = upgrade;
            buttonImmortal.GetComponent<Button>().onClick.RemoveListener(AddImmortal);
        }
    }

    public void UpgradeImmortal()
    {
        int currentStars = PlayerPrefs.GetInt("ImmortalStars");
        if (currentStars < MAX_UPGRADE_LEVEL && ControlOfShop.instance.moneycount >= PlayerPrefs.GetInt("ImmortalCost"))
        {
            ControlOfShop.instance.moneycount -= PlayerPrefs.GetInt("ImmortalCost");
            PlayerPrefs.SetInt("Money", ControlOfShop.instance.moneycount);
            ControlOfShop.instance.UpdateMoneyText();

            int immortalCost = PlayerPrefs.GetInt("ImmortalCost") * 2;
            PlayerPrefs.SetInt("ImmortalCost", immortalCost);
            currencyNeedImmortal.text = GetCostText(immortalCost, PlayerPrefs.GetInt("ImmortalStars"));

            PlayerPrefs.SetInt("ImmortalStars", currentStars + 1);
        }
        UpdateCurrencyTexts();
    }

    public void AddFulminant()
    {
        if (PlayerPrefs.GetInt("Crystall") >= PlayerPrefs.GetInt("FulminantCost"))
        {
            Social.ReportProgress(fulminantAchievment, 100.0f, (bool success) => { });
            PlayerPrefs.SetInt("FulminantStars", 0);
            PlayerPrefs.SetInt("Crystall", PlayerPrefs.GetInt("Crystall") - PlayerPrefs.GetInt("FulminantCost"));
            crystallText.text = PlayerPrefs.GetInt("Crystall").ToString();
            int fulminantCost = 75;
            PlayerPrefs.SetInt("FulminantCost", fulminantCost);
            currencyNeedFulminant.text = GetCostText(fulminantCost, PlayerPrefs.GetInt("FulminantStars"));

            currencyFulminant.sprite = coin;
            buttonFulminant.GetComponent<Button>().onClick.AddListener(UpgradeFulminant);
            buttonFulminant.GetComponent<Image>().sprite = upgrade;
            buttonFulminant.GetComponent<Button>().onClick.RemoveListener(AddFulminant);
        }
    }

    public void AddFirethrower()
    {
        if (PlayerPrefs.GetInt("Crystall") >= PlayerPrefs.GetInt("FirethrowerCost"))
        {
            PlayGamesPlatform.Instance.ReportProgress(flamethrowerAchievment, 100.0f, (bool success) => { });
            PlayerPrefs.SetInt("FirethrowerStars", 0);
            PlayerPrefs.SetInt("Crystall", PlayerPrefs.GetInt("Crystall") - PlayerPrefs.GetInt("FirethrowerCost"));
            crystallText.text = PlayerPrefs.GetInt("Crystall").ToString();
            int firethrowerCost = 60;
            PlayerPrefs.SetInt("FulminantCost", firethrowerCost);
            currencyNeedFirethrower.text = GetCostText(firethrowerCost, PlayerPrefs.GetInt("FirethrowerStars"));

            currencyFirethrower.sprite = coin;
            buttonFirethrower.GetComponent<Button>().onClick.AddListener(UpgradeFirethrower);
            buttonFirethrower.GetComponent<Image>().sprite = upgrade;
            buttonFirethrower.GetComponent<Button>().onClick.RemoveListener(AddFirethrower);
        }
    }

    public void UpgradeFulminant()
    {
        int currentStars = PlayerPrefs.GetInt("FulminantStars");
        if (currentStars < MAX_UPGRADE_LEVEL && ControlOfShop.instance.moneycount >= PlayerPrefs.GetInt("FulminantCost"))
        {
            ControlOfShop.instance.moneycount -= PlayerPrefs.GetInt("FulminantCost");
            PlayerPrefs.SetInt("Money", ControlOfShop.instance.moneycount);
            ControlOfShop.instance.UpdateMoneyText();

            int fulminantCost = PlayerPrefs.GetInt("FulminantCost") * 2;
            PlayerPrefs.SetInt("FulminantCost", fulminantCost);
            currencyNeedFulminant.text = GetCostText(fulminantCost, PlayerPrefs.GetInt("FulminantStars"));

            PlayerPrefs.SetInt("FulminantStars", currentStars + 1);
        }
        UpdateCurrencyTexts();
    }

    public void UpgradeFirethrower()
    {
        int currentStars = PlayerPrefs.GetInt("FirethrowerStars");
        if (currentStars < MAX_UPGRADE_LEVEL && ControlOfShop.instance.moneycount >= PlayerPrefs.GetInt("FirethrowerCost"))
        {
            ControlOfShop.instance.moneycount -= PlayerPrefs.GetInt("FirethrowerCost");
            PlayerPrefs.SetInt("Money", ControlOfShop.instance.moneycount);
            ControlOfShop.instance.UpdateMoneyText();

            int firethrowerCost = PlayerPrefs.GetInt("FirethrowerCost") * 2;
            PlayerPrefs.SetInt("FirethrowerCost", firethrowerCost);
            currencyNeedFirethrower.text = GetCostText(firethrowerCost, PlayerPrefs.GetInt("FirethrowerStars"));

            PlayerPrefs.SetInt("FirethrowerStars", currentStars + 1);
        }
        UpdateCurrencyTexts();
    }

    public void FulminantShip()
    {
        if (PlayerPrefs.GetInt("Language", 0) == 0)
        {
            shipText.text = "The latest development of mankind, helps in the fight against aliens extremely effectively";
        }
        else if (PlayerPrefs.GetInt("Language", 0) == 1)
        {
            shipText.text = "Новейшая разработка человечества, помогает в борьбе с инопланетянами крайне эффективно";
        }

        firstPassive.SetActive(false);
        secondPassive.SetActive(false);
        foreach (GameObject go in couldBeAdded)
        {
            go.SetActive(false);
        }

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }

        if (!InfoFrame.activeSelf)
        {
            StartCoroutine(ShowInfo());
        }

        imageOfShip.sprite = fulminantSprite;
        Vector2 size = imageOfShip.rectTransform.sizeDelta;
        size.y = 1.2f;
        imageOfShip.rectTransform.sizeDelta = size;
        textOfShip.text = "Fulminant";
        textOfShip.color = new Color(0.5f, 0f, 0.5f, 1f);

        if (PlayerPrefs.HasKey("FulminantStars") && PlayerPrefs.GetInt("FulminantStars") != -1)
        {
            if (PlayerPrefs.GetInt("ChosenShip") == 2)
            {
                chooseButton.SetActive(false);
            }
            else
            {
                chooseButton.SetActive(true);
            }

            foreach (GameObject go in couldBeAdded)
            {
                go.SetActive(true);
            }

            int starsToShow = Mathf.Min(PlayerPrefs.GetInt("FulminantStars"), MAX_UPGRADE_LEVEL);
            for (int i = 0; i <= starsToShow; i++)
            {
                stars[i].SetActive(true);
            }

            notBoughtText.SetActive(false);

            if (starsToShow == 0)
            {
                livesText.text = "3";
                livesCouldBeAdded.text = "+1";
                damageText.text = "5";
                damageCouldBeAdded.text = "+0";
                shootingSpeedText.text = "2s";
                shootingSpeedCouldBeAdded.text = "-0";
            }
            else if (starsToShow == 1)
            {
                livesText.text = "4";
                livesCouldBeAdded.text = "+1";
                damageText.text = "5";
                damageCouldBeAdded.text = "+1";
                shootingSpeedText.text = "2s";
                shootingSpeedCouldBeAdded.text = "-1s";
            }
            else if (starsToShow == 2)
            {
                livesText.text = "5";
                livesCouldBeAdded.text = "+1";
                damageText.text = "6";
                damageCouldBeAdded.text = "+0";
                shootingSpeedText.text = "1s";
                shootingSpeedCouldBeAdded.text = "-0.5";
            }
            else if (starsToShow == 3) // Максимальный уровень достигнут
            {
                secondPassive.SetActive(true);
                foreach (GameObject go in couldBeAdded)
                {
                    go.SetActive(false);
                }
                foreach (GameObject text in otherTexts)
                {
                    text.SetActive(false);
                }
                secondPassiveImage.sprite = secret;
                Vector3 parentPosition = secondPassive.transform.parent.position;
                secondPassive.transform.position = new Vector3(parentPosition.x, parentPosition.y, parentPosition.z);
                livesText.text = "6";
                damageText.text = "6";
                shootingSpeedText.text = "0.5s";
            }
            else if (starsToShow == 4) // Максимальный уровень достигнут
            {
                secondPassive.SetActive(true);
                Vector3 parentPosition = secondPassive.transform.parent.position;
                secondPassive.transform.position = new Vector3(parentPosition.x, parentPosition.y, parentPosition.z);
                foreach (GameObject go in couldBeAdded)
                {
                    go.SetActive(false);
                }
                foreach (GameObject text in otherTexts)
                {
                    text.SetActive(true);
                }

                if (PlayerPrefs.GetInt("Language", 0) == 0)
                {
                    secondPassiveName.text = "ComplexShot";
                    secondPassiveTextInfo.text = "Shot hits nearby enemies";
                }
                else if (PlayerPrefs.GetInt("Language", 0) == 1)
                {
                    secondPassiveName.text = "Выстрел дробью";
                    secondPassiveTextInfo.text = "Выстрел поражает ближайших врагов";
                }

                secondPassiveImage.sprite = bulletCloned;
                livesText.text = "6";
                damageText.text = "6";
                shootingSpeedText.text = "0.5s";
            }
        }
        else
        {
            chooseButton.SetActive(false);
            notBoughtText.SetActive(true);
            if (PlayerPrefs.GetInt("Language", 0) == 0)
            {
                notBoughtText.GetComponent<Text>().text = "Not bought yet";
            }
            else if (PlayerPrefs.GetInt("Language", 0) == 1)
            {
                notBoughtText.GetComponent<Text>().text = "Ещё не куплено";
            }

            livesText.text = "3";
            damageText.text = "5";
            shootingSpeedText.text = "2s";
        }
    }

    public void FirethrowerShip()
    {
        if (PlayerPrefs.GetInt("Language", 0) == 0)
        {
            shipText.text = "When GPT 3.0 was released in 2020, humanity created this flamethrower to destroy the cosmic threat";
        }
        else if (PlayerPrefs.GetInt("Language", 0) == 1)
        {
            shipText.text = "Когда в 2020 году был выпущен GPT 3.0, человечество создало этот корабль, чтобы уничтожить космическую угрозу.";
        }

        firstPassive.SetActive(true);
        secondPassive.SetActive(false);
        foreach (GameObject go in couldBeAdded)
        {
            go.SetActive(false);
        }

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }

        if (!InfoFrame.activeSelf)
        {
            StartCoroutine(ShowInfo());
        }

        imageOfShip.sprite = firethrowerSprite;
        Vector2 size = imageOfShip.rectTransform.sizeDelta;
        size.y = 1f;
        imageOfShip.rectTransform.sizeDelta = size;
        textOfShip.text = "Flamethrower";
        textOfShip.color = Color.red;

        if (PlayerPrefs.GetInt("Language", 0) == 0)
        {
            firstPassiveName.text = "Fire the power";
            firstPassiveTextInfo.text = "Fire passes through enemies";
        }
        else if (PlayerPrefs.GetInt("Language", 0) == 1)
        {
            firstPassiveName.text = "Огонь - сила";
            firstPassiveTextInfo.text = "Огонь проходит сквозь врагов";
        }

        firstPassiveImage.sprite = fireSprite;

        if (PlayerPrefs.HasKey("FirethrowerStars") && PlayerPrefs.GetInt("FirethrowerStars") != -1)
        {
            if (PlayerPrefs.GetInt("ChosenShip") == 4)
            {
                chooseButton.SetActive(false);
            }
            else
            {
                chooseButton.SetActive(true);
            }

            foreach (GameObject go in couldBeAdded)
            {
                go.SetActive(true);
            }

            int starsToShow = Mathf.Min(PlayerPrefs.GetInt("FirethrowerStars"), MAX_UPGRADE_LEVEL);
            for (int i = 0; i <= starsToShow; i++)
            {
                stars[i].SetActive(true);
            }

            notBoughtText.SetActive(false);

            if (starsToShow == 0)
            {
                livesText.text = "1";
                livesCouldBeAdded.text = "+2";
                damageText.text = "8";
                damageCouldBeAdded.text = "+0";
                shootingSpeedText.text = "3s";
                shootingSpeedCouldBeAdded.text = "-0";
            }
            else if (starsToShow == 1)
            {
                livesText.text = "3";
                livesCouldBeAdded.text = "+0";
                damageText.text = "8";
                damageCouldBeAdded.text = "+0";
                shootingSpeedText.text = "3s";
                shootingSpeedCouldBeAdded.text = "-1s";
            }
            else if (starsToShow == 2)
            {
                livesText.text = "3";
                livesCouldBeAdded.text = "+0";
                damageText.text = "8";
                damageCouldBeAdded.text = "+1";
                shootingSpeedText.text = "2s";
                shootingSpeedCouldBeAdded.text = "-0";
            }
            else if (starsToShow == 3) // Максимальный уровень достигнут
            {
                livesText.text = "3";
                livesCouldBeAdded.text = "+1";
                damageText.text = "9";
                damageCouldBeAdded.text = "+1";
                shootingSpeedText.text = "2s";
                shootingSpeedCouldBeAdded.text = "-0";
                secondPassive.SetActive(true);
                Vector3 parentPos = secondPassive.transform.parent.position;
                secondPassive.transform.position = new Vector3(parentPos.x, parentPos.y - 0.6f, parentPos.z);
                secondPassiveImage.sprite = secret;
                foreach (GameObject text in otherTexts)
                {
                    text.SetActive(false);
                }
            }
            else if (starsToShow == 4) // Максимальный уровень достигнут
            {
                secondPassive.SetActive(true);
                Vector3 parentPos = secondPassive.transform.parent.position;
                secondPassive.transform.position = new Vector3(parentPos.x, parentPos.y - 0.6f, parentPos.z);
                secondPassiveImage.sprite = addGun;

                if (PlayerPrefs.GetInt("Language", 0) == 0)
                {
                    secondPassiveName.text = "Fire support";
                    secondPassiveTextInfo.text = "Additional gun is activated";
                }
                else if (PlayerPrefs.GetInt("Language", 0) == 1)
                {
                    secondPassiveName.text = "Огневая поддержка";
                    secondPassiveTextInfo.text = "Дополнительная пушка активирована";
                }

                foreach (var go in couldBeAdded)
                {
                    go.SetActive(false);
                }
                livesText.text = "4";
                damageText.text = "10";
                shootingSpeedText.text = "2s";
            }
        }
        else
        {
            chooseButton.SetActive(false);
            notBoughtText.SetActive(true);

            if (PlayerPrefs.GetInt("Language", 0) == 0)
            {
                notBoughtText.GetComponent<Text>().text = "Not bought yet";
            }
            else if (PlayerPrefs.GetInt("Language", 0) == 1)
            {
                notBoughtText.GetComponent<Text>().text = "Ещё не куплено";
            }

            livesText.text = "1";
            damageText.text = "8";
            shootingSpeedText.text = "2s";
        }
    }

    public void ImmortalShip()
    {
        if (PlayerPrefs.GetInt("Language", 0) == 0)
        {
            shipText.text = "No one knows whose ship it is or where it came from, but there are rumors about its survivability";
        }
        else if (PlayerPrefs.GetInt("Language", 0) == 1)
        {
            shipText.text = "Никто не знает, чей это корабль и откуда он взялся, но ходят легенды о его живучести.";
        }

        firstPassive.SetActive(true);
        secondPassive.SetActive(false);
        foreach (GameObject go in couldBeAdded)
        {
            go.SetActive(false);
        }

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }

        if (!InfoFrame.activeSelf)
        {
            StartCoroutine(ShowInfo());
        }

        imageOfShip.sprite = immortalSprite;
        Vector2 size = imageOfShip.rectTransform.sizeDelta;
        size.y = 1.35f;
        imageOfShip.rectTransform.sizeDelta = size;
        textOfShip.text = "Immortal";
        textOfShip.color = Color.gray;

        if (PlayerPrefs.GetInt("Language", 0) == 0)
        {
            firstPassiveName.text = "Immortality";
            firstPassiveTextInfo.text = "Survives twice with no lives";
        }
        else if (PlayerPrefs.GetInt("Language", 0) == 1)
        {
            firstPassiveName.text = "Бессмертие";
            firstPassiveTextInfo.text = "Выживает дважды без единой жизни";
        }

        firstPassiveImage.sprite = passiveImmortal;

        if (PlayerPrefs.HasKey("ImmortalStars") && PlayerPrefs.GetInt("ImmortalStars") != -1)
        {
            firstPassiveImage.sprite = activeImmortal;
            if (PlayerPrefs.GetInt("ChosenShip") == 1)
            {
                chooseButton.SetActive(false);
            }
            else
            {
                chooseButton.SetActive(true);
            }

            foreach (GameObject go in couldBeAdded)
            {
                go.SetActive(true);
            }

            int starsToShow = Mathf.Min(PlayerPrefs.GetInt("ImmortalStars"), MAX_UPGRADE_LEVEL);
            for (int i = 0; i <= starsToShow; i++)
            {
                stars[i].SetActive(true);
            }

            notBoughtText.SetActive(false);

            if (starsToShow == 0)
            {
                livesText.text = "5";
                livesCouldBeAdded.text = "+0";
                damageText.text = "3";
                damageCouldBeAdded.text = "+0";
                shootingSpeedText.text = "0.75s";
                shootingSpeedCouldBeAdded.text = "-0.25s";
            }
            else if (starsToShow == 1)
            {
                livesText.text = "5";
                livesCouldBeAdded.text = "+1";
                damageText.text = "3";
                damageCouldBeAdded.text = "+1";
                shootingSpeedText.text = "0.5s";
                shootingSpeedCouldBeAdded.text = "-0";
            }
            else if (starsToShow == 2)
            {
                livesText.text = "6";
                livesCouldBeAdded.text = "+2";
                damageText.text = "4";
                damageCouldBeAdded.text = "+?";
                shootingSpeedText.text = "0.5s";
                shootingSpeedCouldBeAdded.text = "-0";
            }
            else if (starsToShow == 3) // Максимальный уровень достигнут
            {
                secondPassive.SetActive(true);
                foreach (GameObject go in couldBeAdded)
                {
                    go.SetActive(false);
                }
                foreach (GameObject text in otherTexts)
                {
                    text.SetActive(false);
                }
                secondPassiveImage.sprite = secret;
                Vector3 parentPosition = secondPassive.transform.parent.position;
                secondPassive.transform.position = new Vector3(parentPosition.x, parentPosition.y - 0.6f, parentPosition.z);
                livesText.text = "8";
                damageText.text = "4~6";
                shootingSpeedText.text = "0.5s";
            }
            else if (starsToShow == 4) // Максимальный уровень достигнут
            {
                secondPassive.SetActive(true);
                Vector3 parentPosition = secondPassive.transform.parent.position;
                secondPassive.transform.position = new Vector3(parentPosition.x, parentPosition.y - 0.6f, parentPosition.z);
                foreach (GameObject go in couldBeAdded)
                {
                    go.SetActive(false);
                }
                foreach (GameObject text in otherTexts)
                {
                    text.SetActive(true);
                }

                if (PlayerPrefs.GetInt("Language", 0) == 0)
                {
                    secondPassiveName.text = "BloodyHeal";
                    secondPassiveTextInfo.text = "+ 1 life for killing every 30 enemies";
                }
                else if (PlayerPrefs.GetInt("Language", 0) == 1)
                {
                    secondPassiveName.text = "Жажда крови";
                    secondPassiveTextInfo.text = "+ 1 жизнь за убийство каждых 30 врагов";
                }

                secondPassiveImage.sprite = bloodyHeart;
                livesText.text = "8";
                damageText.text = "4~6";
                shootingSpeedText.text = "0.5s";
            }
        }
        else
        {
            chooseButton.SetActive(false);
            notBoughtText.SetActive(true);

            if (PlayerPrefs.GetInt("Language", 0) == 0)
            {
                notBoughtText.GetComponent<Text>().text = "Not bought yet";
            }
            else if (PlayerPrefs.GetInt("Language", 0) == 1)
            {
                notBoughtText.GetComponent<Text>().text = "Ещё не куплено";
            }

            livesText.text = "5";
            damageText.text = "3";
            shootingSpeedText.text = "0.75s";
        }
    }

    public void ClassicShip()
    {
        if (PlayerPrefs.GetInt("Language", 0) == 0)
        {
            shipText.text = "Warship for short-term space travel";
        }
        else if (PlayerPrefs.GetInt("Language", 0) == 1)
        {
            shipText.text = "Военный корабль для краткосрочных космических путешествий";
        }

        firstPassive.SetActive(false);
        secondPassive.SetActive(false);
        foreach (GameObject go in couldBeAdded)
        {
            go.SetActive(true);
        }

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }

        if (!InfoFrame.activeSelf)
        {
            StartCoroutine(ShowInfo());
        }

        imageOfShip.sprite = classicSprite;
        Vector2 size = imageOfShip.rectTransform.sizeDelta;
        size.y = 1.2f;
        imageOfShip.rectTransform.sizeDelta = size;
        textOfShip.text = "Classic";
        textOfShip.color = Color.green;
        notBoughtText.SetActive(false);

        if (PlayerPrefs.HasKey("ClassicStars"))
        {
            if (PlayerPrefs.GetInt("ChosenShip") == 0)
            {
                chooseButton.SetActive(false);
            }
            else
            {
                chooseButton.SetActive(true);
            }

            int starsToShow = Mathf.Min(PlayerPrefs.GetInt("ClassicStars"), MAX_UPGRADE_LEVEL);
            for (int i = 0; i <= starsToShow; i++)
            {
                stars[i].SetActive(true);
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("ChosenShip") == 0)
            {
                chooseButton.SetActive(false);
            }
            else
            {
                chooseButton.SetActive(true);
            }

            PlayerPrefs.SetInt("ClassicStars", 0);
            stars[0].SetActive(true);
        }

        if (PlayerPrefs.GetInt("ClassicStars") == 0)
        {
            livesText.text = "3";
            livesCouldBeAdded.text = "+1";
            damageText.text = "1";
            damageCouldBeAdded.text = "+1";
            shootingSpeedText.text = "1s";
            shootingSpeedCouldBeAdded.text = "-0";
        }
        else if (PlayerPrefs.GetInt("ClassicStars") == 1)
        {
            livesText.text = "4";
            livesCouldBeAdded.text = "+0";
            damageText.text = "2";
            damageCouldBeAdded.text = "+0.5";
            shootingSpeedText.text = "1s";
            shootingSpeedCouldBeAdded.text = "-0.5";
        }
        else if (PlayerPrefs.GetInt("ClassicStars") == 2)
        {
            livesText.text = "4";
            livesCouldBeAdded.text = "+1";
            damageText.text = "2.5";
            damageCouldBeAdded.text = "+0.5";
            shootingSpeedText.text = "0.5s";
            shootingSpeedCouldBeAdded.text = "-0";
        }
        else if (PlayerPrefs.GetInt("ClassicStars") == 3) // Максимальный уровень достигнут
        {
            secondPassive.SetActive(true);
            foreach (GameObject text in otherTexts)
            {
                text.SetActive(false);
            }
            foreach (GameObject text in couldBeAdded)
            {
                text.SetActive(false);
            }
            secondPassiveImage.sprite = secret;
            Vector3 parentPosition = secondPassive.transform.parent.position;
            secondPassive.transform.position = new Vector3(parentPosition.x, parentPosition.y, parentPosition.z);
            livesText.text = "5";
            damageText.text = "3";
            shootingSpeedText.text = "0.5s";
        }
        else if (PlayerPrefs.GetInt("ClassicStars") == 4) // Максимальный уровень достигнут
        {
            secondPassive.SetActive(true);
            Vector3 parentPosition = secondPassive.transform.parent.position;
            secondPassive.transform.position = new Vector3(parentPosition.x, parentPosition.y, parentPosition.z);
            foreach (GameObject text in otherTexts)
            {
                text.SetActive(true);
            }
            secondPassiveImage.sprite = damageX2;

            if (PlayerPrefs.GetInt("Language", 0) == 0)
            {
                secondPassiveName.text = "DamageX2";
                secondPassiveTextInfo.text = "Temporarily increases damage by 2 times";
            }
            else if (PlayerPrefs.GetInt("Language", 0) == 1)
            {
                secondPassiveName.text = "УронX2";
                secondPassiveTextInfo.text = "Временно увеличивает урон в 2 раза";
            }

            foreach (GameObject text in couldBeAdded)
            {
                text.SetActive(false);
            }
            livesText.text = "5";
            damageText.text = "3";
            shootingSpeedText.text = "0.5s";
        }
    }

    public void GoldenWarriorShip()
    {
        if (PlayerPrefs.GetInt("Language", 0) == 0)
        {
            shipText.text = "The ship of a billionaire which became legendary for its ability to turn flesh into jewelry...";
        }
        else if (PlayerPrefs.GetInt("Language", 0) == 1)
        {
            shipText.text = "Корабль миллиардера, ставший легендарным благодаря своей способности превращать плоть в драгоценности...";
        }

        firstPassive.SetActive(false);
        secondPassive.SetActive(true);
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }

        if (!InfoFrame.activeSelf)
        {
            StartCoroutine(ShowInfo());
        }

        Vector2 size = imageOfShip.rectTransform.sizeDelta;
        size.y = 1.2f;
        imageOfShip.rectTransform.sizeDelta = size;
        imageOfShip.sprite = goldenWarriorSprite;
        textOfShip.text = "Golden Warrior";
        textOfShip.color = Color.yellow;
        notBoughtText.SetActive(true);

        Vector3 parentPosition = secondPassive.transform.parent.position;
        secondPassive.transform.position = new Vector3(parentPosition.x, parentPosition.y, parentPosition.z);
        secondPassiveImage.sprite = crystall;

        if (PlayerPrefs.GetInt("Language", 0) == 0)
        {
            notBoughtText.GetComponent<Text>().text = "Bought";
            secondPassiveName.text = "Gilding";
            secondPassiveTextInfo.text = "There is a chance of crystals falling out of enemies";
        }
        else if (PlayerPrefs.GetInt("Language", 0) == 1)
        {
            notBoughtText.GetComponent<Text>().text = "Куплен";
            secondPassiveName.text = "Позолота";
            secondPassiveTextInfo.text = "Есть вероятность выпадения кристаллов из врагов.";
        }

        livesText.text = "5";
        damageText.text = "5";
        shootingSpeedText.text = "1s";

        if (PlayerPrefs.GetInt("ChosenShip") == 3)
        {
            chooseButton.SetActive(false);
        }
        else
        {
            chooseButton.SetActive(true);
        }
    }

    public void Close()
    {
        StartCoroutine(HideInfo());
    }

    public void Change()
    {
        if (textOfShip.text == "Classic")
        {
            PlayerPrefs.SetInt("ChosenShip", 0);
            chooseButton.SetActive(false);
        }
        else if (textOfShip.text == "Immortal")
        {
            PlayerPrefs.SetInt("ChosenShip", 1);
            chooseButton.SetActive(false);
        }
        else if (textOfShip.text == "Fulminant")
        {
            PlayerPrefs.SetInt("ChosenShip", 2);
            chooseButton.SetActive(false);
        }
        else if (textOfShip.text == "Golden Warrior")
        {
            PlayerPrefs.SetInt("ChosenShip", 3);
            chooseButton.SetActive(false);
        }
        else if (textOfShip.text == "Flamethrower")
        {
            PlayerPrefs.SetInt("ChosenShip", 4);
            chooseButton.SetActive(false);
        }
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

    private string GetCostText(int amount, int starcount)
    {
        if (starcount == MAX_UPGRADE_LEVEL) // Максимальный уровень достигнут
        {
            if (PlayerPrefs.GetInt("Language", 0) == 1)
            {
                return "МАКС";
            }
            return "MAX";
        }
        else
        {
            return FormatCurrency(amount);
        }
    }

    private string FormatCurrency(int amount)
    {
        if (amount >= 1000000000)
        {
            return (amount / 1000000000).ToString() + "B";
        }
        else if (amount >= 1000000)
        {
            return (amount / 1000000).ToString() + "M";
        }
        else if (amount >= 1000)
        {
            return (amount / 1000).ToString() + "K";
        }
        else
        {
            return amount.ToString();
        }
    }

    public void UpdateCurrencyTexts()
    {
        currencyNeedClassic.text = GetCostText(PlayerPrefs.GetInt("ClassicCost"), PlayerPrefs.GetInt("ClassicStars"));
        currencyNeedImmortal.text = GetCostText(PlayerPrefs.GetInt("ImmortalCost"), PlayerPrefs.GetInt("ImmortalStars"));
        currencyNeedFulminant.text = GetCostText(PlayerPrefs.GetInt("FulminantCost"), PlayerPrefs.GetInt("FulminantStars"));
        currencyNeedFirethrower.text = GetCostText(PlayerPrefs.GetInt("FirethrowerCost"), PlayerPrefs.GetInt("FirethrowerStars"));
    }
}