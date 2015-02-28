using UnityEngine;
using System.Collections;

public class Train : MonoBehaviour {

    public TrainWheels front;
    public TrainWheels back;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = (front.transform.position + back.transform.position) / 2;

        // calc angle

        float diffx = front.transform.position.x - back.transform.position.x;
	}
}
