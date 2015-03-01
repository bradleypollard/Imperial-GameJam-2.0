using UnityEngine;
using System.Collections;

public class Track : MonoBehaviour
{
  public enum TrackType
  {
    Straight,
    Right,
    Left,
    SplitRight,
    SplitLeft
  }
  public enum TrackSplitMode
  {
    Straight,
    Turn
  }

  public TrackType type;
  public Sprite splitStraight;
  public Sprite splitTurn;

  private Vector2 endPoint;
  private float endRotation;
  private TrackSplitMode mode;

  // Use this for initialization
  void Start()
  {
    if (type == TrackType.SplitRight)
    {
      mode = TrackSplitMode.Straight;
      endPoint = new Vector2(0, 2);
      endRotation = 0;

      GenerateStraightEndPoint();
    }
    else if (type == TrackType.SplitLeft)
    {
      mode = TrackSplitMode.Straight;
      endPoint = new Vector2(0, 2);
      endRotation = 0;

      GenerateStraightEndPoint();
    }
    else if (type == TrackType.Right)
    {
      endPoint = new Vector2(2, 2);
      endRotation = -90;

      GenerateRightEndPoint();
    }
    else if (type == TrackType.Left)
    {
      endPoint = new Vector2(-1, 1);
      endRotation = 90;

      GenerateLeftEndPoint();
    }
    else if (type == TrackType.Straight)
    {
      endPoint = new Vector2(0, 1);
      endRotation = 0;

      GenerateStraightEndPoint();
    }
  }

  private void GenerateStraightEndPoint()
  {
    endPoint.x = endPoint.y * -Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
    endPoint.y = endPoint.y * Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
  }

  private void GenerateLeftEndPoint()
  {
    int youspinmerightround = (int)transform.rotation.eulerAngles.z;

    if (youspinmerightround == 270)
    {
      endPoint.x = -endPoint.x;
    }
    else if (youspinmerightround == 180)
    {
      endPoint.x = -endPoint.x;
      endPoint.y = -endPoint.y;
    }
    else if (youspinmerightround == 90)
    {
      endPoint.y = -endPoint.y;
    }
  }

  private void GenerateRightEndPoint()
  {
    int youspinmerightround = (int)transform.rotation.eulerAngles.z;

    if (youspinmerightround == 270)
    {
      endPoint.y = -endPoint.y;
    }
    else if (youspinmerightround == 180)
    {
      endPoint.x = -endPoint.x;
      endPoint.y = -endPoint.y;
    }
    else if (youspinmerightround == 90)
    {
      endPoint.x = -endPoint.x;
    }
  }

  // Update is called once per frame
  void Update()
  {

  }

  public Vector2 GetEndPoint()
  {
    return endPoint;
  }

  public float GetEndRotation()
  {
    return endRotation;
  }

  public TrackSplitMode GetMode()
  {
    return mode;
  }

  public void SwitchMode()
  {
    if (mode == TrackSplitMode.Straight)
    {
      mode = TrackSplitMode.Turn;
      GetComponent<SpriteRenderer>().sprite = splitStraight;

      if (type == TrackType.SplitRight)
      {
        endPoint = new Vector2(2, 2);
        endRotation = -90;

        GenerateRightEndPoint();
      }
      else if (type == TrackType.SplitLeft)
      {
        endPoint = new Vector2(-1, 1);
        endRotation = 90;

        GenerateLeftEndPoint();
      }
    }
    else
    {
      mode = TrackSplitMode.Straight;
      GetComponent<SpriteRenderer>().sprite = splitTurn;

      endPoint = new Vector2(0, 2);
      endRotation = 0;

      GenerateStraightEndPoint();
    }
  }

  public bool IsTurn()
  {
    return (type == Track.TrackType.Right || type == Track.TrackType.Left ||
      (type == Track.TrackType.SplitRight || type == Track.TrackType.SplitLeft) && GetMode() == Track.TrackSplitMode.Turn);
  }

  public bool IsStraight()
  {
    return (type == Track.TrackType.Straight || (type == Track.TrackType.SplitRight || type == Track.TrackType.SplitLeft)
      && GetMode() == Track.TrackSplitMode.Straight);
  }
}
