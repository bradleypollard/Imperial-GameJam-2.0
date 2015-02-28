using UnityEngine;
using System.Collections;

public class Track : MonoBehaviour {

    public Vector2 EndPoint;
    public float EndRotation;

	// Use this for initialization
	void Start () {
        if (EndPoint.x == 1.5 && EndPoint.y == 1.5)
        {
            if (transform.rotation.eulerAngles.z == 270)
            {
                EndPoint.y = -EndPoint.y;
            }
            else if (transform.rotation.eulerAngles.z == 180)
            {
                EndPoint = -EndPoint;
            }
            else if (transform.rotation.eulerAngles.z == 90)
            {
                EndPoint.x = -EndPoint.x;
            }
        }
        else if (EndPoint.x == 0 && EndPoint.y == 1)
        {
            EndPoint.x = -Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
            EndPoint.y = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
        }
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
