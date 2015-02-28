using UnityEngine;
using System.Collections;

public class Train : MonoBehaviour {

    private Track currentTrack;

    private Vector2 start;
    private Vector2 end;
    private Vector3 startRot;
    private Vector3 endRot;

    private float startTime;
    private float journeyLength;
    
    public GameLogic gl;
    public int x;
    public int y;
    public float speed = 1f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	    if (!currentTrack)
        {
            currentTrack = gl.map[x, y];

            start = (Vector2)transform.position;
            end = (Vector2)transform.position + currentTrack.EndPoint;

            startRot = transform.rotation.eulerAngles;
            endRot = transform.rotation.eulerAngles + new Vector3(0, 0, currentTrack.EndRotation);

            journeyLength = currentTrack.EndPoint.magnitude;
            startTime = Time.time;
        }

        float distCovered = (Time.time - startTime) * speed;

        float fracJourney = distCovered / journeyLength;

        Vector2 pos = Vector2.Lerp(start, end, fracJourney);

        transform.position = new Vector3(pos.x, pos.y, -1);
        transform.rotation = Quaternion.Euler(Vector3.Lerp( startRot, endRot, fracJourney ));

        if (fracJourney >= 1)
        {

            x += (int)Mathf.Ceil(currentTrack.EndPoint.x);
            y += (int)Mathf.Ceil(currentTrack.EndPoint.y);

            currentTrack = null;
        }
	}
}
