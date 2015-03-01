using UnityEngine;
using System.Collections;

public class ParticleHack : MonoBehaviour
{

  // Use this for initialization
  void Start()
  {
    GetComponent<ParticleRenderer>().sortingLayerName = "Test";
  }

  // Update is called once per frame
  void Update()
  {

  }
}
