using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

    public Track[,] map = new Track[10,10];
	public Camera c;
	public GameObject track;
	
	// Use this for initialization
	void Start () {
	    foreach (GameObject g in GameObject.FindGameObjectsWithTag("Track"))
        {
            map[(int)g.transform.position.x, (int)g.transform.position.y] = g.GetComponent<Track>();
        }
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")){
			Vector3 clickpos = c.ScreenToWorldPoint(Input.mousePosition);
			Debug.Log("x is: " + clickpos.x + " and y is:" + clickpos.y);
			GameObject newtrack = Instantiate(track, new Vector3(Mathf.Floor(clickpos.x), Mathf.Floor(clickpos.y), 0), Quaternion.identity) as GameObject;
			//TODO: PUT DOWN SOME TRACK
		}
	}
}
