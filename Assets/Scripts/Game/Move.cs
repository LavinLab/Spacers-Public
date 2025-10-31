using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Vector3 lastTouchPosition;
    private bool isMoving = false;
    private Camera mainCamera;
    public static bool isOk = true;

    void Start()
    {
        mainCamera = Camera.main; // Get the main camera
    }

    void Update()
    {
        if (Input.touchCount > 0 && isOk)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isMoving = true;
                    lastTouchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                    break;

                case TouchPhase.Ended:
                    isMoving = false;
                    break;

                case TouchPhase.Moved:
                    if (isMoving)
                    {
                        Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                        Vector3 delta = touchPosition - lastTouchPosition;

                        // Calculate new position with bounds check
                        Vector3 newPosition = transform.position + delta;
                        Vector3 viewportPos = mainCamera.WorldToViewportPoint(newPosition);
                        viewportPos.x = Mathf.Clamp(viewportPos.x, 0.0f, 1.0f); // Clamp x within 0 to 1
                        viewportPos.y = Mathf.Clamp(viewportPos.y, 0.0f, 1.0f); // Clamp y within 0 to 1
                        newPosition = mainCamera.ViewportToWorldPoint(viewportPos);

                        transform.position = newPosition;
                        lastTouchPosition = touchPosition;
                    }
                    break;
            }
        }
        else if (Input.touchCount > 0 && !isOk)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isMoving = true;
                    lastTouchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                    break;

                case TouchPhase.Ended:
                    isMoving = false;
                    break;

                case TouchPhase.Moved:
                    if (isMoving)
                    {
                        Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                        Vector3 delta = touchPosition - lastTouchPosition;

                        // Calculate new position with bounds check
                        Vector3 newPosition = transform.position - delta;
                        Vector3 viewportPos = mainCamera.WorldToViewportPoint(newPosition);
                        viewportPos.x = Mathf.Clamp(viewportPos.x, 0.0f, 1.0f); // Clamp x within 0 to 1
                        viewportPos.y = Mathf.Clamp(viewportPos.y, 0.0f, 1.0f); // Clamp y within 0 to 1
                        newPosition = mainCamera.ViewportToWorldPoint(viewportPos);

                        transform.position = newPosition;
                        lastTouchPosition = touchPosition;
                    }
                    break;
            }
        }
    }
}