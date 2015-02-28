using UnityEngine;
using System.Collections;

public class TrainWheels : MonoBehaviour
{

  private Track currentTrack;

  private Vector2 start;
  private Vector2 end;
  private Vector3 startRot;
  private Vector3 endRot;

  private float startTime;
  private float journeyLength;
  private int x;
  private int y;

  public GameLogic gl;
  public float speed = 1f;

  // Use this for initialization
  void Start()
  {
    x = (int)transform.position.x;
    y = (int)transform.position.y;
  }

  // Update is called once per frame
  void Update()
  {

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



    if (currentTrack.type == Track.TrackType.Right || currentTrack.type == Track.TrackType.Left)
    {
      int trackRot = (int)currentTrack.transform.rotation.eulerAngles.z;

      float magicrandabout = 2 * Mathf.PI * 1.5f / 4;

      float rotCovered = (Time.time - startTime) * speed / magicrandabout * currentTrack.EndRotation;

      if (trackRot == 90 || trackRot == 270)
      {
        transform.RotateAround(new Vector3(start.x, end.y, -1), new Vector3(0, 0, 1), Time.deltaTime * speed / magicrandabout * currentTrack.EndRotation);
      }
      else
      {
        transform.RotateAround(new Vector3(end.x, start.y, -1), new Vector3(0, 0, 1), Time.deltaTime * speed / magicrandabout * currentTrack.EndRotation);
      }

      if (Mathf.Abs(rotCovered) >= Mathf.Abs(currentTrack.EndRotation))
      {
        x += (int)currentTrack.EndPoint.x;
        y += (int)currentTrack.EndPoint.y;

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Round(transform.rotation.eulerAngles.z / 90) * 90);
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), -1);

        currentTrack = null;
      }
    }
    else if (currentTrack.type == Track.TrackType.Straight)
    {
      float distCovered = (Time.time - startTime) * speed;

      float fracJourney = distCovered / journeyLength;

      Vector2 pos = Vector2.Lerp(start, end, fracJourney);

      transform.position = new Vector3(pos.x, pos.y, -1);

      if (fracJourney >= 1)
      {

        x += (int)currentTrack.EndPoint.x;
        y += (int)currentTrack.EndPoint.y;

        currentTrack = null;
      }
    }
    //transform.rotation = Quaternion.Euler(Vector3.Lerp( startRot, endRot, fracJourney ));




  }
}
