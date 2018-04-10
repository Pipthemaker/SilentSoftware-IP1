using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public List<GameObject> sprites;

	// Use this for initialization
	void Start ()
    {
        foreach (GameObject image in sprites)
        {
            image.SetActive(false);
        }

        int i = Random.Range(0, sprites.Count);

        sprites[i].SetActive(true);
;    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
