using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{

  public Track[,] map = new Track[30, 30];
  public Camera c;

  public GameObject straightTrack;
  public GameObject leftTrack;
  public GameObject rightTrack;
  public GameObject stationStart;
  public GameObject stationStop;
  public GameObject splitRight;
  public GameObject splitLeft;
  public GameObject driftRight;
  public GameObject driftLeft;

  public GameObject wheels;
  public GameObject train;

  private Vector2 startDrag;
  bool isDragging;
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
    isDragging = false;
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
    else if (Input.GetButtonDown("STARTSTATION"))
    {
      currentlySelected = stationStart;
      GeneratePreview(le_x, le_y);
    }
    else if (Input.GetButtonDown("STOPSTATION"))
    {
      currentlySelected = stationStop;
      GeneratePreview(le_x, le_y);
    }
    else if (Input.GetButtonDown("SPLITRIGHT"))
    {
      currentlySelected = splitRight;
      GeneratePreview(le_x, le_y);
    }
    else if (Input.GetButtonDown("SPLITLEFT"))
    {
      currentlySelected = splitLeft;
      GeneratePreview(le_x, le_y);
    }
    else if (Input.GetButtonDown("DRIFTRIGHT"))
    {
      currentlySelected = driftRight;
      GeneratePreview(le_x, le_y);
    }
    else if (Input.GetButtonDown("DRIFTLEFT"))
    {
      currentlySelected = driftLeft;
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
    
    if (Input.GetButton("DRAG"))
    {
      c.transform.position += new Vector3(-Input.GetAxisRaw("Mouse X") * Time.deltaTime * 2, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * 2, 0);
      c.transform.position = new Vector3(Mathf.Clamp(c.transform.position.x, 11, 19), Mathf.Clamp(c.transform.position.y, 7, 23), -10);
    }

    if (previewtrack)
    {
      Vector3 hello = new Vector3(le_x, le_y, 0);
      previewtrack.transform.position = hello;
    }

    if (Input.GetButton("PLACE"))
    {
      if (currentlySelected != null)
      {
        if (!map[le_x, le_y])
        {
          GameObject newtrack = Instantiate(currentlySelected, new Vector3(le_x, le_y, 0), Quaternion.Euler(new Vector3(0, 0, turnbabyturn))) as GameObject;
          map[le_x, le_y] = newtrack.GetComponent<Track>();
        }
      }
    }

    if (Input.GetButtonDown("PLACE"))
    {
      if (le_x >= 0 && le_y >= 0)
      {
        Track underMouse = map[le_x, le_y];
        if (underMouse != null && (underMouse.type == Track.TrackType.SplitLeft || underMouse.type == Track.TrackType.SplitRight))
        {
          underMouse.SwitchSplitMode();
        }
        else if (underMouse != null && (underMouse.type == Track.TrackType.DriftLeft || underMouse.type == Track.TrackType.DriftRight))
        {
          underMouse.SwitchDriftMode();
        }
        else if (underMouse != null && underMouse.type == Track.TrackType.StationStart)
        {
          int dx = (int)Mathf.Round(-Mathf.Sin(Mathf.Deg2Rad * underMouse.transform.rotation.eulerAngles.z));
          int dy = (int)Mathf.Round(Mathf.Cos(Mathf.Deg2Rad * underMouse.transform.rotation.eulerAngles.z));

          GameObject back = (GameObject)Instantiate(wheels, new Vector3(underMouse.transform.position.x + dx, underMouse.transform.position.y + dy, 0), underMouse.transform.rotation);
          GameObject front = (GameObject)Instantiate(wheels, new Vector3(underMouse.transform.position.x + 3 * dx, underMouse.transform.position.y + 3 * dy, 0), underMouse.transform.rotation);

          TrainWheels fw = front.GetComponent<TrainWheels>();
          TrainWheels bw = back.GetComponent<TrainWheels>();
          fw.gl = this;
          bw.gl = this;

          GameObject t = (GameObject)Instantiate(train, new Vector3(underMouse.transform.position.x + 2 * dx, underMouse.transform.position.y + 2 * dy, 0), underMouse.transform.rotation);
          Train tt = t.GetComponent<Train>();
          tt.front = fw;
          tt.back = bw;

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
