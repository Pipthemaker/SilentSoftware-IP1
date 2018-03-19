using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTest : MonoBehaviour
{
    public bool timer = false;

    public float time = 0;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (timer)
        {
            time += Time.deltaTime;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "StartLine")
        {
            timer = true;
        }
        if (other.tag == "EndLine")
        {
            timer = false;
            Debug.Log(time);
        }
    }
}
