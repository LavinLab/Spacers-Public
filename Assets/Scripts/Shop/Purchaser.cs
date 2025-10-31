using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class Purchaser : MonoBehaviour
{
    public static Purchaser instance;
    public GameObject thanksText;
    public GameObject supportButton;
    public GameObject goldenWarrior;
    public Text goldenThanks;
    private void Awake()
    {
        instance = this;
    }
    public void OnPurchaseCompleted(Product product)
    {
        switch (product.definition.id) {
            case "com.lavinlab.spacers.support":
                GiveBonuses();
                break;
            default:
                GiveBonuses();
                break;
        }

    }

    public void ShowThanks()
    {
        if (!PlayerPrefs.HasKey("IsThanks") || PlayerPrefs.GetInt("IsThanks") == 0)
        {
            PlayerPrefs.SetInt("IsThanks", 1);
        }
        if (thanksText != null && supportButton != null && goldenWarrior != null)
        {
            goldenWarrior.SetActive(true);
            thanksText.SetActive(true);
            supportButton.SetActive(false);
            if(PlayerPrefs.GetInt("Language", 0) == 0)
            {
                thanksText.GetComponent<Text>().text = "Thanks";
                goldenThanks.text = "Thanks";
            }
            else if(PlayerPrefs.GetInt("Language", 0) == 1)
            {
                thanksText.GetComponent<Text>().text = "Спасибо";
                goldenThanks.text = "Спасибо";
            }
        }
        else
        {
            Debug.Log("ThanksText or SupportButton is null.");
        }
    }
    public void GiveBonuses()
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 200);
        PlayerPrefs.SetInt("Crystall", PlayerPrefs.GetInt("Crystall") + 20);
        ControlOfShips.instance.UpdateCurrencyTexts();
        ControlOfShop.instance.LoadUpgradeData();

        // Update UI
        ControlOfShop.instance.UpdateMoneyText();
        ControlOfShop.instance.UpdateUpgradeTexts();
        ControlOfShips.instance.crystallText.text = PlayerPrefs.GetInt("Crystall").ToString();
        ShowThanks();
    }

}
