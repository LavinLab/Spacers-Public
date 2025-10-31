using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this for UI interaction

public class SpeedUpEffect : MonoBehaviour
{
    public static SpeedUpEffect instance;
    private bool isSpeedUpActive = false;
    private Coroutine speedUpCoroutine;
    public float duration;

    public GameObject objectToMove; // Assign the object to move in the Inspector
    public Text countdownText; // Assign the text component in the Inspector 
    public float startInterval = 0;
    bool should = true;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SpeedUp")
        {
            if (isSpeedUpActive)
            {
                StopCoroutine(speedUpCoroutine);
                ShootCartridge.shootInterval = startInterval;
                should = false;
            }
            duration = PlayerPrefs.HasKey("SpeedUpHave") ? PlayerPrefs.GetInt("SpeedUpHave") + 10 : 10;
            speedUpCoroutine = StartCoroutine(Speedup(duration, should));
            Destroy(collision.gameObject);
        }
    }

    public IEnumerator Speedup(float duration, bool shouldDo)
    {
        isSpeedUpActive = true;
        CartridgeMove.Instance.speed += 2;
        startInterval = ShootCartridge.shootInterval;
        ShootCartridge.shootInterval /= 2f;
        
        float elapsedTime = 0f;

        Vector3 scaleNeeded = new Vector3(222f, 222f, 222f);
        Vector3 originalScale = Vector3.zero;
        float scaleDuration = 0.15f;
        float scaleElapsedTime = 0f;

        objectToMove.SetActive(true);

        while (scaleElapsedTime < scaleDuration && shouldDo)
        {
            scaleElapsedTime += Time.deltaTime;
            float t = scaleElapsedTime / scaleDuration;
            objectToMove.transform.localScale = Vector3.Lerp(originalScale, scaleNeeded, t);
            yield return null;
        }
        objectToMove.transform.localScale = scaleNeeded;

        while (elapsedTime < duration)
        {
            // Move the object using Lerp
            // Update the countdown text
            int remainingTime = Mathf.CeilToInt(duration - elapsedTime);
            countdownText.text = remainingTime + "s";

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Reset values and object position
        CartridgeMove.Instance.speed = 10f;
        ShootCartridge.shootInterval = startInterval;
        while (scaleElapsedTime < scaleDuration)
        {
            scaleElapsedTime += Time.deltaTime;
            float t = scaleElapsedTime / scaleDuration;
            objectToMove.transform.localScale = Vector3.Lerp(scaleNeeded, originalScale, t);
            yield return null;
        }
        objectToMove.transform.localScale = originalScale;
        should = true;
        objectToMove.SetActive(false);
        countdownText.text = "0s"; // Clear the text
        isSpeedUpActive = false;
    }
}