using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealHeart : MonoBehaviour
{
    public static HealHeart instance;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public GameObject newHeart;
    public GameObject[] hearts;

    private void Awake()
    {
        instance = this;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HealHeart")
        {
            Destroy(collision.gameObject);

            // Calculate max hearts and current lives
            int maxHearts = Core.GetLives();

            float currentLives = PlayerDie.lives;

            // Increase lives if possible
            if (currentLives < maxHearts)
            {
                PlayerDie.lives = Mathf.Min(currentLives + 1f, maxHearts);
            }

            // Update heart display
            UpdateHeartsDisplay();
        }
    }
    
    public void UpdateHeartsDisplay()
    {
        if(!PlayerDie.instance.livesObject.activeSelf) {
            // Get all heart objects
            hearts = GameObject.FindGameObjectsWithTag("Heart");

            // Calculate full and half hearts needed
            int fullHearts = Mathf.FloorToInt(PlayerDie.lives);
            bool hasHalfHeart = PlayerDie.lives - fullHearts > 0;

            // Update existing hearts
            for (int i = 0; i < hearts.Length; i++)
            {
                Image heartImage = hearts[i].GetComponent<Image>();
                if (i < fullHearts)
                {
                    heartImage.sprite = fullHeart;
                }
                else if (i == fullHearts && hasHalfHeart)
                {
                    heartImage.sprite = halfHeart;
                }
                else
                {
                    // Hide extra hearts
                    heartImage.enabled = false;
                }
            }

            // Create new hearts if needed
            if (hearts.Length < fullHearts + (hasHalfHeart ? 1 : 0))
            {
                CreateNewHearts(hearts.Length, fullHearts, hasHalfHeart);
            }
        }
        else
        {
            PlayerDie.instance.livesText.text = PlayerDie.lives.ToString();
        }
    }

    private void CreateNewHearts(int existingHearts, int fullHeartsNeeded, bool hasHalfHeart)
    {
        GameObject canvas = GameObject.Find("Canvas");
        RectTransform lastHeartRectTransform = existingHearts > 0
            ? hearts[existingHearts - 1].GetComponent<RectTransform>()
            : null;

        // Create full hearts
        for (int i = existingHearts; i < fullHeartsNeeded; i++)
        {
            CreateHeart(canvas, lastHeartRectTransform, fullHeart);
            lastHeartRectTransform = newHeart.GetComponent<RectTransform>();
        }

        // Create half heart if needed
        if (hasHalfHeart)
        {
            CreateHeart(canvas, lastHeartRectTransform, halfHeart);
        }
    }

    private void CreateHeart(GameObject canvas, RectTransform lastHeartRectTransform, Sprite heartSprite)
    {
        GameObject newHeart = Instantiate(PlayerDie.instance.heart);
        newHeart.transform.SetParent(canvas.transform, false);

        RectTransform heartRectTransform = newHeart.GetComponent<RectTransform>();
        if (lastHeartRectTransform != null)
        {
            heartRectTransform.anchoredPosition = new Vector2(lastHeartRectTransform.anchoredPosition.x + 70, heartRectTransform.anchoredPosition.y);
        }

        newHeart.GetComponent<Image>().sprite = heartSprite;
    }
}