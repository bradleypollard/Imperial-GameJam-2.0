using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

    public Track[,] map = new Track[10,10];
	public Camera c;
	public GameObject straightTrack;
	public GameObject leftTrack;
	public GameObject rightTrack;
	private GameObject currentlySelected;
	
	// Use this for initialization
	void Start () {
	    foreach (GameObject g in GameObject.FindGameObjectsWithTag("Track"))
        {
            map[(int)g.transform.position.x, (int)g.transform.position.y] = g.GetComponent<Track>();
        }

        currentlySelected = leftTrack;
	}

	// Update is called once per frame
	void Update () {
		Vector3 clickpos = c.ScreenToWorldPoint(Input.mousePosition);
		int le_x = (int)Mathf.Floor (clickpos.x);
		int le_y = (int)Mathf.Floor (clickpos.y);
		if(Input.GetButtonDown("GOLEFT")){
			currentlySelected = leftTrack;
		}
		else if(Input.GetButtonDown("GOSTRAIGHT")){
			currentlySelected = straightTrack;
		}
		else if(Input.GetButtonDown("GORIGHT")){
			currentlySelected = rightTrack;
		}

		//TODO: ROTAET

		if(Input.GetButtonDown("PLACE")){
			GameObject newtrack = Instantiate(currentlySelected, new Vector3(le_x, le_y, 0), Quaternion.identity) as GameObject;
			map[le_x, le_y] = newtrack.GetComponent<Track>();
		}
	}
}
