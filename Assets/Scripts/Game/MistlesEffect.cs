using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MistlesEffect : MonoBehaviour
{
    public static MistlesEffect instance;
    private bool gunsActive = false;
    private Coroutine gunsCoroutine;
    public GameObject gunPrefab;
    public float offset = 0.8f;
    public Text countdownText;
    public GameObject objectToMove;
    public float duration = 0;
    public GameObject rightGun;
    public GameObject leftGun;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mistles")
        {
            if (gunsActive)
            {
                StopCoroutine(gunsCoroutine);
                Destroy(rightGun);
                Destroy(leftGun);
                should = false;
            }
            duration = PlayerPrefs.HasKey("MistlesHave") ? PlayerPrefs.GetInt("MistlesHave") + 3 : 3;
            gunsCoroutine = StartCoroutine(SpawnGuns(duration, should));
            Destroy(collision.gameObject);
        }
    }

    public IEnumerator SpawnGuns(float duration, bool shouldDo)
    {
        gunsActive = true;
        float elapsedTime = 0f;
        Vector3 scaleNeeded = new Vector3(222f, 222f, 222f);
        Vector3 scaleGuns = new Vector3(1f, 1f, 1f);
        Vector3 originalScale = Vector3.zero;
        float scaleDuration = 0.15f;
        float scaleElapsedTime = 0f;

        Vector3 rightPosition = transform.position + new Vector3(offset, 0, 0);
        rightGun = Instantiate(gunPrefab, rightPosition, Quaternion.identity);
        rightGun.transform.parent = transform;
        rightGun.tag = "MistlesShoot";
        

        Vector3 leftPosition = transform.position - new Vector3(offset, 0, 0);
        leftGun = Instantiate(gunPrefab, leftPosition, Quaternion.identity);
        leftGun.transform.parent = transform;
        leftGun.tag = "MistlesShoot";

        while (scaleElapsedTime < scaleDuration && shouldDo)
        {
            scaleElapsedTime += Time.deltaTime;
            float t = scaleElapsedTime / scaleDuration;
            rightGun.transform.localScale = Vector3.Lerp(originalScale, scaleGuns, t);
            leftGun.transform.localScale = Vector3.Lerp(originalScale, scaleGuns, t);
            yield return null;
        }
        rightGun.transform.localScale = scaleGuns;
        leftGun.transform.localScale = scaleGuns;

        objectToMove.SetActive(true);
        scaleElapsedTime = 0f;
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
            // Update the countdown text
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
            objectToMove.transform.localScale = Vector3.Lerp(scaleNeeded, originalScale, t);
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
            if(rightGun != null && leftGun != null)
            {
                rightGun.transform.localScale = Vector3.Lerp(scaleGuns, originalScale, t);
                leftGun.transform.localScale = Vector3.Lerp(scaleGuns, originalScale, t);
            }
            yield return null;
        }
        if (rightGun != null && leftGun != null)
        {
            rightGun.transform.localScale = originalScale;
            leftGun.transform.localScale = originalScale;
        }
        gunsActive = false;
        Destroy(rightGun);
        Destroy(leftGun);
    }

    private void Update()
    {
        if (rightGun != null)
        {
            rightGun.transform.position = transform.position + new Vector3(offset, 0, 0);
        }
        if (leftGun != null)
        {
            leftGun.transform.position = transform.position - new Vector3(offset, 0, 0);
        }
    }
}