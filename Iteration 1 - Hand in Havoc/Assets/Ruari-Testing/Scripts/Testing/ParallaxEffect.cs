using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private Transform mainCamera;

	// Use this for initialization
	void Start ()
    {
        mainCamera = GameObject.Find("Camera Mount").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3((mainCamera.position.x / 2), gameObject.transform.position.y, 10);
	}
}
