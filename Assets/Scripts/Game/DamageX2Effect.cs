using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageX2Effect : MonoBehaviour
{
    public static DamageX2Effect instance;
    private bool DamageX2Active = false;
    private Coroutine DamageX2Coroutine;
    public float duration;
    public Text countdownText; // Assign the text component in the Inspector 
    public Image DamageX2Image;
    public Button DamageX2Button;
    public Sprite DamageX2UnactiveSprite;
    public Sprite DamageX2ActiveSprite;
    public bool activeCoroutine;
    public int remainingTime;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        DamageX2Button.onClick.AddListener(DamageX2);
        DamageX2Image.sprite = DamageX2ActiveSprite;
    }
    public void DamageX2()
    {
        if (!DamageX2Active)
        {
            duration = 9;
            DamageX2Coroutine = StartCoroutine(DamageX2(duration));
        }
    }

    public IEnumerator DamageX2(float duration)
    {
        activeCoroutine = true;
        DamageX2Active = true;
        DamageX2Button.interactable = false;
        
        EnemyDie.damage *= 2;
        Enemy2Die.damage *= 2;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            remainingTime = Mathf.CeilToInt(duration - elapsedTime);
            countdownText.text = remainingTime + "s";

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        DamageX2Image.sprite = DamageX2UnactiveSprite;
        EnemyDie.damage /= 2;
        Enemy2Die.damage /= 2;
        countdownText.text = ""; // Clear the text
        StartCoroutine(CountdownCooldown(15));
    }

    public IEnumerator CountdownCooldown(int cooldownDuration)
    {
        activeCoroutine = false;
        float elapsedTime = 0f;
        while (elapsedTime < cooldownDuration)
        {
            remainingTime = Mathf.CeilToInt(cooldownDuration - elapsedTime);
            countdownText.text = remainingTime + "s";

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        countdownText.text = ""; // Clear the text
        DamageX2Image.sprite = DamageX2ActiveSprite;
        DamageX2Active = false;
        DamageX2Button.interactable = true;
    }
}
