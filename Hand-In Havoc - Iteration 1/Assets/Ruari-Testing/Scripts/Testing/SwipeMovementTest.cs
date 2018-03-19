using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeMovementTest : MonoBehaviour
{
    public Vector2 swipeStart, swipeEnd;

    public int movementDirection;
    public int movementSpeed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + (movementSpeed * movementDirection * Time.deltaTime), transform.position.y, 0);

        if (Input.GetMouseButtonDown(0))
        {
            swipeStart = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            swipeEnd = Input.mousePosition;
            SwipeDirection();
        }
    }

    private void SwipeDirection()
    {
        float swipeMagnitudeX = (swipeEnd.x - swipeStart.x) * (swipeEnd.x - swipeStart.x);
        float swipeMagnitudeY = (swipeEnd.y - swipeStart.y) * (swipeEnd.y - swipeStart.y);

        //Player Has Swiped Left or Right
        if (swipeMagnitudeX > swipeMagnitudeY)
        {
            if (swipeEnd.x > swipeStart.x) //Player Has Swiped Right
            {
                movementDirection = 1;
            }
            else //Player Has Swiped Left
            {
                movementDirection = -1;
            }
        }
        //Player Has Swiped Up or Down
        else if (swipeMagnitudeX < swipeMagnitudeY)
        {
            if (swipeEnd.y > swipeStart.y) //Player Has Swiped Up
            {
                
            }
            else //Player Has Swiped Down
            {

            }
        }
        //Player has Tapped the Screen
        else
        {

        }
    }
}
