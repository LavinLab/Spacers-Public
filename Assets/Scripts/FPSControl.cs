using UnityEngine;

public class FPSControl : MonoBehaviour
{

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SetTargetFPS();
    }

    void SetTargetFPS()
    {
        // Устанавливаем целевую частоту кадров равной частоте обновления дисплея
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;

        // Для отладки выводим установленное значение
        Debug.Log("Установлен целевой FPS: " + Application.targetFrameRate);
    }
}