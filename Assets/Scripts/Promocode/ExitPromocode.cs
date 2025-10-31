using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Add this for EventSystem

public class ExitPromocode : MonoBehaviour
{
    private int backButtonCount = 0; // Count for Escape key presses
    void Clear()
    {
        backButtonCount = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            backButtonCount++;
            if(backButtonCount == 1)
            {
                Invoke("Clear", 3f);
            }
            if(backButtonCount == 5) 
            {
                SceneManager.LoadScene("Shop");
            }
        }
    }
}