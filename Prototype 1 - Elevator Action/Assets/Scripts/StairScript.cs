using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairScript : MonoBehaviour
{
    public Transform target;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void StairsMove(GameObject other)
    {
        other.transform.position = target.transform.position;
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyMovement>().currentWaitTime = other.GetComponent<EnemyMovement>().maxWaitTime;
            other.GetComponent<EnemyMovement>().mode = 0;
        }
    }
}
