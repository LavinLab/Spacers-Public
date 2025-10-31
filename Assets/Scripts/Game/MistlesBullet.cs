using UnityEngine;

public class MistlesBullet : MonoBehaviour
{
    public static float speed = 10f;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
