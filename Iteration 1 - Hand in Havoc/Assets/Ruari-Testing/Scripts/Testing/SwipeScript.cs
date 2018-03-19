using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeScript : MonoBehaviour
{
    public Vector2 swipeStart, swipeEnd;

    public double tapZone;

    public Camera sceneCamera; //Temp Camera Variable For Testing Purposes

	// Use this for initialization
	void Start ()
    {
        tapZone = Screen.width * 0.1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Stores Where The Player First Touched The Screen
            swipeStart = Input.mousePosition;
        }

        //When Player Lifts Up From The Screen
        if (Input.GetMouseButtonUp(0))
        {
            //Stores The Last Place That The Player's Finger Was Touching & Calls The SwipeDirection Method
            swipeEnd = Input.mousePosition;
            SwipeDirection();
        }
	}

    private void SwipeDirection()
    {
        float swipeMagnitudeX = (swipeEnd.x - swipeStart.x) * (swipeEnd.x - swipeStart.x);
        float swipeMagnitudeY = (swipeEnd.y - swipeStart.y) * (swipeEnd.y - swipeStart.y);
        double tapZoneMagnitude = (tapZone * tapZone);

        //Player Has Swiped Left or Right
        if (swipeMagnitudeX > swipeMagnitudeY && swipeMagnitudeX > tapZoneMagnitude)
        {
            if (swipeEnd.x > swipeStart.x) //Player Has Swiped Right
            {
                sceneCamera.backgroundColor = new Color(255, 0, 0);
            }
            else //Player Has Swiped Left
            {
                sceneCamera.backgroundColor = new Color(0, 0, 255);
            }
        }
        //Player Has Swiped Up or Down
        else if (swipeMagnitudeX < swipeMagnitudeY && swipeMagnitudeY > tapZoneMagnitude)
        {
            if (swipeEnd.y > swipeStart.y) //Player Has Swiped Up
            {
                sceneCamera.backgroundColor = new Color(0, 255, 0);
            }
            else //Player Has Swiped Down
            {
                sceneCamera.backgroundColor = new Color(255, 255, 0);
            }
        }
        //Player has Tapped the Screen
        else
        {
            sceneCamera.backgroundColor = new Color(255, 255, 255);
        }
    }
}
