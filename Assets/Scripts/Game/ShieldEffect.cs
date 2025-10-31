using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShieldEffect : MonoBehaviour
{
    public static ShieldEffect instance;
    private bool shieldActive = false;
    private Coroutine shieldCoroutine;
    public GameObject shieldPrefab;
    public Text countdownText;
    public GameObject objectToMove;
    public float duration;
    bool should = true;

    public GameObject actualShield;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shield")
        {
            if (shieldActive)
            {
                StopCoroutine(shieldCoroutine);
                Destroy(actualShield);
                should = false;
            }
            duration = PlayerPrefs.HasKey("ShieldHave") ? PlayerPrefs.GetInt("ShieldHave") + 5 : 5;
            shieldCoroutine = StartCoroutine(Shield(duration, should));
            Destroy(collision.gameObject);
        }
    }

    public IEnumerator Shield(float duration, bool shouldDo)
    {
        shieldActive = true;
        Vector3 spawnPosition = transform.position;
        Vector3 targetScale = new Vector3(222f, 222f, 222f);
        Vector3 actualShieldScale = new Vector3(1f, 1f, 1f);
        Vector3 originalScale = Vector3.zero;
        float scaleDuration = 0.15f;
        float scaleElapsedTime = 0f;
        actualShield = Instantiate(shieldPrefab, spawnPosition, Quaternion.identity, transform);
        while(scaleElapsedTime < scaleDuration && shouldDo)
        {
            scaleElapsedTime += Time.deltaTime;
            float t = scaleElapsedTime / scaleDuration;
            actualShield.transform.localScale = Vector3.Lerp(originalScale, actualShieldScale, t);
            yield return null;
        }
        actualShield.transform.localScale = actualShieldScale;
        float elapsedTime = 0f;
        objectToMove.SetActive(true);

        
        scaleElapsedTime = 0f;
        while (scaleElapsedTime < scaleDuration && shouldDo)
        {
            scaleElapsedTime += Time.deltaTime;
            float t = scaleElapsedTime / scaleDuration;
            objectToMove.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }
        objectToMove.transform.localScale = targetScale;

        while (elapsedTime < duration)
        {
            int remainingTime = Mathf.CeilToInt(duration - elapsedTime);
            countdownText.text = remainingTime + "s";

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        scaleElapsedTime = 0f;
        while (scaleElapsedTime < scaleDuration)
        {
            scaleElapsedTime += Time.deltaTime;
            float t = scaleElapsedTime / scaleDuration;
            objectToMove.transform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            yield return null;
        }

        objectToMove.transform.localScale = originalScale;
        should = true;
        objectToMove.SetActive(false);
        scaleElapsedTime = 0f;
        while (scaleElapsedTime < scaleDuration)
        {
            scaleElapsedTime += Time.deltaTime;
            float t = scaleElapsedTime / scaleDuration;
            if(actualShield != null)
            {
                actualShield.transform.localScale = Vector3.Lerp(actualShieldScale, originalScale, t);
            }
            yield return null;
        }
        if (actualShield != null)
        {
            actualShield.transform.localScale = originalScale;
        }
        Destroy(actualShield);
        shieldActive = false;
    }

    private void Update()
    {
        if (actualShield != null)
        {
            actualShield.transform.position = transform.position;
        }
    }
}