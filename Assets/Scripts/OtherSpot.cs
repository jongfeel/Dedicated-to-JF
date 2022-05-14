using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OtherSpot : MonoBehaviour
{
  public bool isJointed = false;

  private SpringJoint spj;

  void Start() {
    spj = GetComponent<SpringJoint>();
  }

  void Update() {
    if (isJointed == true) {
      GetComponent<SphereCollider>().enabled = false;
    }
  }

  private void OnTriggerEnter(Collider other) {
    if (other.tag == "Mentee") {
      Rigidbody cb = other.GetComponent<Rigidbody>();
      spj.connectedBody = cb;
      isJointed = true;
    }
  }
}
