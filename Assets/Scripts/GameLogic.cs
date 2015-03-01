using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{

  public Track[,] map = new Track[20, 20];
  public Camera c;
  public GameObject straightTrack;
  public GameObject leftTrack;
  public GameObject rightTrack;
  public GameObject stationPiece;

  private GameObject currentlySelected;
  private int turnbabyturn;
  private GameObject previewtrack;

  // Use this for initialization
  void Start()
  {
    foreach (GameObject g in GameObject.FindGameObjectsWithTag("Track"))
    {
      map[(int)g.transform.position.x, (int)g.transform.position.y] = g.GetComponent<Track>();
    }
  }

  // Update is called once per frame
  void Update()
  {
    Vector3 clickpos = c.ScreenToWorldPoint(Input.mousePosition);
    int le_x = (int)Mathf.Floor(clickpos.x);
    int le_y = (int)Mathf.Floor(clickpos.y);

    if (Input.GetButtonDown("GOLEFT"))
    {
      currentlySelected = leftTrack;
      GeneratePreview(le_x, le_y);
    }
    else if (Input.GetButtonDown("GOSTRAIGHT"))
    {
      currentlySelected = straightTrack;
      GeneratePreview(le_x, le_y);
    }
    else if (Input.GetButtonDown("GORIGHT"))
    {
      currentlySelected = rightTrack;
      GeneratePreview(le_x, le_y);
    }
    else if (Input.GetButtonDown("STATION"))
    {
      currentlySelected = stationPiece;
      GeneratePreview(le_x, le_y);
    }
    else if (Input.GetButtonDown("STOPGHOST"))
    {
      if (previewtrack)
      {
        Destroy(previewtrack);
        previewtrack = null;
        currentlySelected = null;
      }
    }

    if (Input.GetButtonDown("ROTAETLEFT"))
    {
      turnbabyturn += 90;
      GeneratePreview(le_x, le_y);
    }
    else if (Input.GetButtonDown("ROTAETRIGHT"))
    {
      turnbabyturn -= 90;
      GeneratePreview(le_x, le_y);
    }

    if (previewtrack)
    {
      Vector3 hello = new Vector3(le_x, le_y, 0);
      previewtrack.transform.position = hello;
    }

    if (Input.GetButtonDown("PLACE"))
    {
      if (currentlySelected != null)
      {
        GameObject newtrack = Instantiate(currentlySelected, new Vector3(le_x, le_y, 0), Quaternion.Euler(new Vector3(0, 0, turnbabyturn))) as GameObject;
        map[le_x, le_y] = newtrack.GetComponent<Track>();
      }
      else
      {
        Track underMouse = map[le_x, le_y];
        if (underMouse != null && (underMouse.type == Track.TrackType.SplitLeft || underMouse.type == Track.TrackType.SplitRight))
        {
          underMouse.SwitchMode();
        }
        else if (underMouse != null && underMouse.type == Track.TrackType.Station)
        {
          //TODO: DROPTRAIN
        }
      }

    }
  }

  private void GeneratePreview(int le_x, int le_y)
  {
    if (previewtrack)
    {
      Destroy(previewtrack);
    }
    previewtrack = Instantiate(currentlySelected, new Vector3(le_x, le_y, 0), Quaternion.Euler(new Vector3(0, 0, turnbabyturn))) as GameObject;
    SpriteRenderer sr = previewtrack.GetComponent<SpriteRenderer>();
    sr.color = new Color(1, 1, 1, 0.2f);
  }
}
