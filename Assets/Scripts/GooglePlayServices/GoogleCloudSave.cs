using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using System;
using UnityEngine;

public class GoogleCloudSave : MonoBehaviour
{
    bool isSaving = false;
    [SerializeField] GameObject ac1, ac2, dec1, dec2;

    public void SaveLoadData(bool saving)
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            isSaving = saving;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution("SpacersSavedGames", DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, SaveGameOpen);
        }
    }

    private void SaveGameOpen(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (isSaving)
            {
                byte[] myData = GetByteArray();

                SavedGameMetadataUpdate updateForMetadata = new SavedGameMetadataUpdate.Builder()
                   .WithUpdatedDescription("I have updated my game at: " + DateTime.Now.ToString())
                   .Build();

                ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(meta, updateForMetadata, myData, SaveCallback);
            }
            else
            {
                ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(meta, LoadCallback);
            }
        }
    }

    private void LoadCallback(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Success load");
            ac2.SetActive(true);
            Invoke("Diactivate", 1f);

            if (data != null && data.Length > 0)
            {
                string loadedData = System.Text.Encoding.ASCII.GetString(data);
                LoadSavedString(loadedData);
            }
            else
            {
                Debug.Log("No saved data found");
            }
        }
        else
        {
            Debug.Log("Failed load");
            dec2.SetActive(true);
            Invoke("Diactivate", 1f);
        }
    }

    private void LoadSavedString(string cloudData)
    {
        string[] cloudStringArray = cloudData.Split('|');

        PlayerPrefs.SetInt("ChosenShip", Convert.ToInt32(cloudStringArray[0]));
        PlayerPrefs.SetInt("ClassicStars", Convert.ToInt32(cloudStringArray[1]));
        PlayerPrefs.SetInt("ImmortalStars", Convert.ToInt32(cloudStringArray[2]));
        PlayerPrefs.SetInt("FulminantStars", Convert.ToInt32(cloudStringArray[3]));
        PlayerPrefs.SetInt("Money", Convert.ToInt32(cloudStringArray[4]));
        PlayerPrefs.SetInt("Crystall", Convert.ToInt32(cloudStringArray[5]));
        PlayerPrefs.SetInt("HeartHave", Convert.ToInt32(cloudStringArray[6]));
        PlayerPrefs.SetInt("ChanseHave", Convert.ToInt32(cloudStringArray[7]));
        PlayerPrefs.SetInt("MistlesHave", Convert.ToInt32(cloudStringArray[8]));
        PlayerPrefs.SetInt("SpeedUpHave", Convert.ToInt32(cloudStringArray[9]));
        PlayerPrefs.SetInt("ShieldHave", Convert.ToInt32(cloudStringArray[10]));
        PlayerPrefs.SetInt("MaxScore", Convert.ToInt32(cloudStringArray[11]));
        PlayerPrefs.SetInt("DayCount", Convert.ToInt32(cloudStringArray[12]));
        PlayerPrefs.SetInt("IsThanks", Convert.ToInt32(cloudStringArray[13]));
        PlayerPrefs.SetInt("RewardAwailable", Convert.ToInt32(cloudStringArray[14]));
        PlayerPrefs.SetInt("IsNotification", Convert.ToInt32(cloudStringArray[15]));
        PlayerPrefs.SetString("RewardTime", cloudStringArray[16]);
        PlayerPrefs.SetInt("ClassicCost", Convert.ToInt32(cloudStringArray[17]));
        PlayerPrefs.SetInt("ImmortalCost", Convert.ToInt32(cloudStringArray[18]));
        PlayerPrefs.SetInt("FulminantCost", Convert.ToInt32(cloudStringArray[19]));
        PlayerPrefs.SetInt("ShieldCost", Convert.ToInt32(cloudStringArray[20]));
        PlayerPrefs.SetInt("MistlesCost", Convert.ToInt32(cloudStringArray[21]));
        PlayerPrefs.SetInt("SpeedUpCost", Convert.ToInt32(cloudStringArray[22]));
        PlayerPrefs.SetInt("HeartCost", Convert.ToInt32(cloudStringArray[23]));
        PlayerPrefs.SetInt("ChanseCost", Convert.ToInt32(cloudStringArray[24]));
        PlayerPrefs.SetInt("FirethrowerCost", Convert.ToInt32(cloudStringArray[25]));
        PlayerPrefs.SetInt("FirethrowerStars", Convert.ToInt32(cloudStringArray[26]));
        PlayerPrefs.SetInt("Language", Convert.ToInt32(cloudStringArray[27]));
        if(PlayerPrefs.GetInt("Language") == 0)
        {
            ControlOfSettings.instance.EnglishButton();
        }
        else if(PlayerPrefs.GetInt("Language") == 1)
        {
            ControlOfSettings.instance.RussianButton();
        }
    }

    private void SaveCallback(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Success save");
            ac1.SetActive(true);
            Invoke("Diactivate", 1f);
        }
        else
        {
            Debug.Log("Failed save");
            dec1.SetActive(true);
            Invoke("Diactivate", 1f);
        }
    }

    byte[] GetByteArray()
    {
        string strData = $"{PlayerPrefs.GetInt("ChosenShip", 0)}|{PlayerPrefs.GetInt("ClassicStars", 0)}|{PlayerPrefs.GetInt("ImmortalStars", -1)}|{PlayerPrefs.GetInt("FulminantStars", -1)}|{PlayerPrefs.GetInt("Money", 0)}|{PlayerPrefs.GetInt("Crystall", 0)}|{PlayerPrefs.GetInt("HeartHave", 0)}|{PlayerPrefs.GetInt("ChanseHave", 0)}|{PlayerPrefs.GetInt("MistlesHave", 0)}|{PlayerPrefs.GetInt("SpeedUpHave", 0)}|{PlayerPrefs.GetInt("ShieldHave", 0)}|{PlayerPrefs.GetInt("MaxScore", 0)}|{PlayerPrefs.GetInt("DayCount", 5)}|{PlayerPrefs.GetInt("IsThanks", 0)}|{PlayerPrefs.GetInt("RewardAwailable", 1)}|{PlayerPrefs.GetInt("IsNotification", 1)}|{PlayerPrefs.GetString("RewardTime", "")}|{PlayerPrefs.GetInt("ClassicCost", 100)}|{PlayerPrefs.GetInt("ImmortalCost", 50)}|{PlayerPrefs.GetInt("FulminantCost", 75)}|{PlayerPrefs.GetInt("ShieldCost", 10)}|{PlayerPrefs.GetInt("MistlesCost", 10)}|{PlayerPrefs.GetInt("SpeedUpCost", 10)}|{PlayerPrefs.GetInt("HeartCost", 20)}|{PlayerPrefs.GetInt("ChanseCost", 40)}|{PlayerPrefs.GetInt("FirethrowerCost", 100)}|{PlayerPrefs.GetInt("FirethrowerStars", -1)}|{PlayerPrefs.GetInt("Language", 0)}";
        return System.Text.Encoding.ASCII.GetBytes(strData);
    }

    void Diactivate()
    {
        ac1.SetActive(false);
        ac2.SetActive(false);
        dec1.SetActive(false);
        dec2.SetActive(false);
    }
}