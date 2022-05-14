using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JF : MonoBehaviour {
  public event Action OnClear;
  public GameObject Particle;

  private void OnTriggerEnter(Collider other) {
    if (other.tag == "Mentee") {
      Instantiate(Particle, transform.position, Quaternion.identity);
      Destroy(gameObject);
      OnClear();
    }
  }
}
