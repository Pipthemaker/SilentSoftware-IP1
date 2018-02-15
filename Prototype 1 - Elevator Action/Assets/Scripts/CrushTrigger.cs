using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushTrigger : MonoBehaviour
{
    private bool crushing;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (crushing)
        {
            if (transform.parent.gameObject.tag == "Player")
            {
                transform.parent.GetComponent<PlayerMovement>().dead = true;
            }
            if (transform.parent.gameObject.tag == "Enemy")
            {
                GameObject.Find("LevelManager").GetComponent<LevelManager>().currentEnemies--;
                transform.parent.GetComponent<EnemyMovement>().parentFloor.GetComponent<FloorData>().hasEnemy = false;
                Destroy(transform.parent.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            crushing = true;
        }
    }
}
