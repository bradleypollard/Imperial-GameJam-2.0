using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

    public Track[,] map = new Track[10,10];

	// Use this for initialization
	void Start () {
	    foreach (GameObject g in GameObject.FindGameObjectsWithTag("Track"))
        {
            map[(int)g.transform.position.x, (int)g.transform.position.y] = g.GetComponent<Track>();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
