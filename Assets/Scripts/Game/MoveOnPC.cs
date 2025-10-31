using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPC : MonoBehaviour
{
    void Start()
    {

    }

    private Vector3 lastTouchPosition;
    private bool isMoving = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;
            lastTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }

        if (isMoving && Input.GetMouseButton(0))
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 delta = touchPosition - lastTouchPosition;
            transform.position += delta;
            lastTouchPosition = touchPosition;
        }
    }
}
