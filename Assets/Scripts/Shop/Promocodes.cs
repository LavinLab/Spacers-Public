using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Add this for EventSystem

public class Promocodes : MonoBehaviour
{

    private int buttonCount = 0; // Count for button clicks
    private int backButtonCount = 0; // Count for Escape key presses

    private bool escSequenceComplete = false; // Flag to track button sequence completion
    public void PromoClicked()
    {
        if (backButtonCount == 3 && escSequenceComplete)
        {
            buttonCount += 1;
        }
    }
    void Clear()
    {
        buttonCount = 0;
        backButtonCount = 0;
        escSequenceComplete = false;
    }
    void Update()
    {
        if (escSequenceComplete)
        {
            Invoke("Clear", 1f);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            backButtonCount++;
            if(backButtonCount == 3)
            {
                escSequenceComplete = true;
            }
        }
        if (buttonCount == 3 && backButtonCount == 3)
        {
            buttonCount = 0;
            backButtonCount = 0;
            escSequenceComplete = false;
            SceneManager.LoadScene("Promocode");
        }
    }
}