using UnityEngine;
using System.Collections;

public class Track : MonoBehaviour
{


  public enum TrackType
  {
    Straight,
    Right,
    Left
  }
  public TrackType type;

  public Vector2 EndPoint;
  public float EndRotation;

  // Use this for initialization
  void Start()
  {
    if (type == TrackType.Right)
    {
      int youspinmerightround = (int)transform.rotation.eulerAngles.z;

      if (youspinmerightround == 270)
      {
        EndPoint.y = -EndPoint.y;
      }
      else if (youspinmerightround == 180)
      {
        EndPoint.x = -EndPoint.x;
        EndPoint.y = -EndPoint.y;
      }
      else if (youspinmerightround == 90)
      {
        EndPoint.x = -EndPoint.x;
      }
    }
    else if (type == TrackType.Left)
    {
      int youspinmerightround = (int)transform.rotation.eulerAngles.z;

      if (youspinmerightround == 270)
      {
        EndPoint.x = -EndPoint.x;
      }
      else if (youspinmerightround == 180)
      {
        EndPoint.x = -EndPoint.x;
        EndPoint.y = -EndPoint.y;
      }
      else if (youspinmerightround == 90)
      {
        EndPoint.y = -EndPoint.y;
      }
    }
    else if (type == TrackType.Straight)
    {
      EndPoint.x = -Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
      EndPoint.y = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
    }
  }

  // Update is called once per frame
  void Update()
  {

  }
}
