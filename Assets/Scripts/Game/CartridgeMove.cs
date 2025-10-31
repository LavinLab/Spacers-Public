using UnityEngine;

public class CartridgeMove : MonoBehaviour
{
    public static CartridgeMove Instance;
    public float speed = 10;
    public Vector3 direction = Vector3.up;
    
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    

    

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
