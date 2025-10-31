using UnityEngine;
using UnityEngine.UI;

public class BG : MonoBehaviour
{
    [SerializeField] public RawImage img;
    [SerializeField] public float x, y;
    public static BG instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(x, y) * Time.deltaTime, img.uvRect.size);
    }
}
