using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEffects : MonoBehaviour
{
    [SerializeField] private string[] effects = { "Aim", "SpeedUp", "Mistles", "Shield", "Coin" };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string effect in effects)
        {
            if (collision.gameObject.tag == effect)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
