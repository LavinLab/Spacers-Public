using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Unity.Notifications.Android;
using Unity.VisualScripting;

public class AdManager : MonoBehaviour
{
    //public static AdManager instance;
    //private string _adUnitId = "R-M-13517838-1";
    //private RewardedAdLoader rewardedAdLoader;
    //private RewardedAd rewardedAd;
    //public Button adButton;
    //public Text adText;
    //public GameObject coin;
    //public GameObject adTextObject;
    //public GameObject countAd;

    

    //private DateTime rewardTime;
    //private bool rewardAvailable = true;
    //private void Awake()
    //{
    //    instance = this;
    //    rewardedAdLoader = new RewardedAdLoader();
    //    rewardedAdLoader.OnAdLoaded += HandleAdLoaded;
    //}
    //void Start()
    //{
        
    //    // Retrieve stored values
    //    string storedRewardTime = PlayerPrefs.GetString("RewardTime", "");
    //    int isRewardAvailable = PlayerPrefs.GetInt("RewardAvailable", 1);

    //    if (!string.IsNullOrEmpty(storedRewardTime))
    //    {
    //        rewardTime = DateTime.Parse(storedRewardTime);
    //        rewardAvailable = isRewardAvailable == 1;

    //        if (!rewardAvailable)
    //        {
    //            StartCoroutine(CountdownRoutine());
    //        }
    //        else
    //        {
    //            adButton.interactable = true;
    //            adText.text = "+20";
    //        }
    //    }
    //}
    //public void HandleAdLoaded(object sender, RewardedAdLoadedEventArgs args)
    //{
    //    rewardedAd = args.RewardedAd;
    //    ShowAd();
    //}

    //public void LoadAd()
    //{
    //    MobileAds.SetAgeRestrictedUser(true);

    //    if (rewardedAd != null)
    //    {
    //        rewardedAd.Destroy();
    //    }

    //    rewardedAdLoader.LoadAd(CreateAdRequest(_adUnitId));
    //}

    //private AdRequestConfiguration CreateAdRequest(string adUnitId)
    //{
    //    return new AdRequestConfiguration.Builder(adUnitId).Build();
    //}

    //public void ShowAd()
    //{
    //    rewardedAd.OnRewarded += HandleRewarded;

    //    rewardedAd.Show();
    //}
    //public void HandleRewarded(object sender, Reward args)
    //{
    //    PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 20);
    //    ControlOfShips.instance.UpdateCurrencyTexts();
    //    ControlOfShop.instance.LoadUpgradeData();

    //    // Update UI
    //    ControlOfShop.instance.UpdateMoneyText();
    //    ControlOfShop.instance.UpdateUpgradeTexts();
    //    // Set reward time and start countdown
    //    if (ControlOfShop.instance.dayCount > 1)
    //    {
    //        ControlOfShop.instance.dayCount--;
    //        ControlOfShop.instance.dayCountText.text = ControlOfShop.instance.dayCount + "/5";
    //        PlayerPrefs.SetInt("DayCount", ControlOfShop.instance.dayCount);
    //    }
    //    else
    //    {
    //        rewardTime = DateTime.Now.AddDays(1);
    //        PlayerPrefs.SetString("RewardTime", rewardTime.ToString());
    //        PlayerPrefs.SetInt("RewardAvailable", 0); // Set to not available
    //        adButton.interactable = false;
    //        StartCoroutine(CountdownRoutine());
    //    }
    //}

    //public IEnumerator CountdownRoutine()
    //{
    //    if(adTextObject != null)
    //    {
    //        RectTransform adTextRectTransform = adTextObject.GetComponent<RectTransform>();
    //        Vector2 currentPosition = adTextRectTransform.anchoredPosition;
    //        currentPosition.x = coin.GetComponent<RectTransform>().anchoredPosition.x;
    //        adTextRectTransform.anchoredPosition = currentPosition;
    //    }
    //    if(coin != null)
    //    {
    //        coin.SetActive(false);
    //    }
    //    if(adButton != null)
    //    {
    //        adButton.interactable = false;
    //    }
    //    rewardAvailable = false;
    //    countAd.SetActive(false);
    //    while (DateTime.Now < rewardTime)
    //    {
    //        TimeSpan timeRemaining = rewardTime - DateTime.Now;
    //        if(adText.gameObject.activeSelf)
    //        {
    //            adText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeRemaining.Hours, timeRemaining.Minutes, timeRemaining.Seconds);
    //        }
    //        yield return new WaitForSeconds(1f);
    //    }
    //    // Reward available again
    //    rewardAvailable = true;
    //    countAd.SetActive(true);
    //    adButton.interactable = true;
    //    adText.text = "+20";
    //    PlayerPrefs.SetInt("RewardAvailable", 1);
    //    ControlOfShop.instance.dayCount = 5;
    //    ControlOfShop.instance.dayCountText.text = ControlOfShop.instance.dayCount + "/5";
    //    PlayerPrefs.SetInt("DayCount", ControlOfShop.instance.dayCount);
    //    // Set to available
    //    PlayerPrefs.SetInt("IsNotification", 1);
    //    if(adTextObject != null)
    //    {
    //        adTextObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-677.9993f, -2807.222f, 0);
    //    }
    //    if(coin != null)
    //    {
    //        coin.SetActive(true);
    //    }
    //}

    //// Optional: Handle app background/foreground
    //void OnApplicationPause(bool pauseStatus)
    //{
    //    if (pauseStatus)
    //    {
    //        // App going to background, stop coroutine
    //        StopCoroutine(CountdownRoutine());
    //    }
    //    else
    //    {
    //        // App coming back to foreground, restart if needed
    //        if (!rewardAvailable)
    //        {
    //            StartCoroutine(CountdownRoutine());
    //        }
    //    }
    //}
}