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
        float dx = front.transform.position.x - back.transform.position.x;
        float dy = front.transform.position.y - back.transform.position.y;

        float ang = 0;
        ang = Mathf.Atan2(dy, dx);
        transform.rotation = Quaternion.Euler(0, 0, -Mathf.Rad2Deg * (2.5f * Mathf.PI - ang));
	}
}
