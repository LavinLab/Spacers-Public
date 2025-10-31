using System.Collections;
using UnityEngine;

public class ShakeCameraEffect : MonoBehaviour
{
    public float duration = 0.2f;
    private Transform cameraTransform;
    private Vector3 originalPosition;
    public static ShakeCameraEffect instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cameraTransform = GetComponent<Transform>();
        originalPosition = cameraTransform.position;
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        float x, y;
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            x = Random.Range(-0.05f, 0.05f);
            y = Random.Range(-0.05f, 0.05f);
            cameraTransform.position = new Vector3(x, y, originalPosition.z);
            yield return new WaitForSeconds(0.025f);
        }

        cameraTransform.position = originalPosition;
    }
}