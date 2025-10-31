using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPromocode : MonoBehaviour
{
    public InputField PromoField;
    public GameObject Acepted;
    public GameObject Declined;
    public Text Promo, PromoPlaceholder;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Language", 0) == 0)
        {
            Promo.text = "Promocode";
            PromoPlaceholder.text = "Promocode...";
        }
        else if(PlayerPrefs.GetInt("Language", 0) == 1)
        {
            Promo.text = "Промокод";
            PromoPlaceholder.text = "Промокод...";
        }
    }
    public void PromoClicked()
    {
        if(PromoField.text == "IOO")
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 100);
            Acepted.SetActive(true);
            Invoke("Hide", 0.5f);
        }
        else if(PromoField.text == "IK")
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 1000);
            Acepted.SetActive(true);
            Invoke("Hide", 0.5f);
        }
        else if (PromoField.text == "IM")
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 1000000);
            Acepted.SetActive(true);
            Invoke("Hide", 0.5f);
        }
        else if (PromoField.text == "IB")
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 1000000000);
            Acepted.SetActive(true);
            Invoke("Hide", 0.5f);
        }
        else if (PromoField.text == "IOOC")
        {
            PlayerPrefs.SetInt("Crystall", PlayerPrefs.GetInt("Crystall") + 100);
            Acepted.SetActive(true);
            Invoke("Hide", 0.5f);
        }
        else
        {
            Declined.SetActive(true);
            Invoke("Hide", 0.5f);
        }
    }

    void Hide()
    {
        Acepted.SetActive(false);
        Declined.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
