using UnityEngine;
using System.Collections;

public class ParticleHack : MonoBehaviour
{

  // Use this for initialization
  void Start()
  {
    GetComponent<ParticleSystemRenderer>().sortingLayerName = "Particles";
  }

  // Update is called once per frame
  void Update()
  {
    if (!GetComponent<ParticleSystem>().IsAlive())
    {
      Destroy(gameObject);
    }
  }
}
