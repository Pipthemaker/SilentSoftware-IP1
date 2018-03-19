using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public Vector2 swipeStart, swipeEnd;
    private double tapZone;
    
    private GameObject player;
    private PlayerController playerController;

    // Use this for initialization
    void Start()
    {
        tapZone = Screen.width * 0.1;

        player = GameObject.Find("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
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
                playerController.SwipeInput("Swipe Right");
            }
            else //Player Has Swiped Left
            {
                playerController.SwipeInput("Swipe Left");
            }
        }
        //Player Has Swiped Up or Down
        else if (swipeMagnitudeX < swipeMagnitudeY && swipeMagnitudeY > tapZoneMagnitude)
        {
            if (swipeEnd.y > swipeStart.y) //Player Has Swiped Up
            {
                playerController.SwipeInput("Swipe Up");
            }
            else //Player Has Swiped Down
            {
                playerController.SwipeInput("Swipe Down");
            }
        }
        //Player has Tapped the Screen
        else
        {
            playerController.SwipeInput("Tap");
        }
    }
}
