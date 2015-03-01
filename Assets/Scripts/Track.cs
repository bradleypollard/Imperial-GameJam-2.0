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
    SplitLeft,
    DriftRight,
    DriftLeft,
    StationStart,
    StationStop
  }
  public enum TrackMode
  {
    Straight,
    Turn
  }

  public TrackType type;
  public Sprite spriteStraight;
  public Sprite spriteTurn;

  private Vector2 endPoint;
  private float endRotation;
  private TrackMode mode;

  // Use this for initialization
  void Start()
  {
    if (type == TrackType.SplitRight)
    {
      mode = TrackMode.Straight;
      endPoint = new Vector2(0, 2);
      endRotation = 0;

      GenerateStraightEndPoint();
    }
    else if (type == TrackType.SplitLeft)
    {
      mode = TrackMode.Straight;
      endPoint = new Vector2(0, 2);
      endRotation = 0;

      GenerateStraightEndPoint();
    }
    else if (type == TrackType.DriftRight)
    {
      mode = TrackMode.Straight;
      endPoint = new Vector2(0, 2);
      endRotation = 0;

      GenerateStraightEndPoint();
    }
    else if (type == TrackType.DriftLeft)
    {
      mode = TrackMode.Straight;
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

  private void GenerateDriftRightEndPoint()
  {
    int youspinmerightround = (int)Mathf.Round(transform.rotation.eulerAngles.z);

    if (youspinmerightround == 270)
    {
      float temp = endPoint.x;
      endPoint.x = endPoint.y;
      endPoint.y = -temp;
    }
    else if (youspinmerightround == 180)
    {
      endPoint.x = -endPoint.x;
      endPoint.y = -endPoint.y;
    }
    else if (youspinmerightround == 90)
    {
      float temp = endPoint.x;
      endPoint.x = -endPoint.y;
      endPoint.y = temp;
    }
  }

  private void GenerateDriftLeftEndPoint()
  {
    int youspinmerightround = (int)Mathf.Round(transform.rotation.eulerAngles.z);

    if (youspinmerightround == 270)
    {
      float temp = endPoint.x;
      endPoint.x = endPoint.y;
      endPoint.y = -temp;
    }
    else if (youspinmerightround == 180)
    {
      endPoint.x = -endPoint.x;
      endPoint.y = -endPoint.y;
    }
    else if (youspinmerightround == 90)
    {
      float temp = endPoint.x;
      endPoint.x = -endPoint.y;
      endPoint.y = temp;
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

  public TrackMode GetMode()
  {
    return mode;
  }

  public void SwitchSplitMode()
  {
    if (mode == TrackMode.Straight)
    {
      mode = TrackMode.Turn;
      GetComponent<SpriteRenderer>().sprite = spriteTurn;

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
      mode = TrackMode.Straight;
      GetComponent<SpriteRenderer>().sprite = spriteStraight;

      endPoint = new Vector2(0, 2);
      endRotation = 0;

      GenerateStraightEndPoint();
    }
  }

  public void SwitchDriftMode()
  {
    if (mode == TrackMode.Straight)
    {
      mode = TrackMode.Turn;
      GetComponent<SpriteRenderer>().sprite = spriteTurn;

      if (type == TrackType.DriftRight)
      {
        endPoint = new Vector2(1, 2);
        endRotation = 0;

        GenerateDriftRightEndPoint();
      }
      else if (type == TrackType.DriftLeft)
      {
        endPoint = new Vector2(-1, 2);
        endRotation = 0;

        GenerateDriftLeftEndPoint();
      }
    }
    else
    {
      mode = TrackMode.Straight;
      GetComponent<SpriteRenderer>().sprite = spriteStraight;

      endPoint = new Vector2(0, 2);
      endRotation = 0;

      GenerateStraightEndPoint();
    }
  }

  public bool IsTurn()
  {
    return (type == Track.TrackType.Right || type == Track.TrackType.Left ||
      (type == Track.TrackType.SplitRight || type == Track.TrackType.SplitLeft) && GetMode() == Track.TrackMode.Turn);
  }

  public bool IsStraight()
  {
    return (type == Track.TrackType.Straight || (type == Track.TrackType.SplitRight || type == Track.TrackType.SplitLeft)
      && GetMode() == Track.TrackMode.Straight || type == Track.TrackType.DriftRight || type == Track.TrackType.DriftLeft);
  }

  public bool IsStation()
  {
    return (type == Track.TrackType.StationStop);
  }
}
