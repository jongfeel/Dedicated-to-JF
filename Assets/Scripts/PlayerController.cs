using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

  public event Action GameOver;

  public GameObject Particle;
  public float ReleaseTime = 0.05f;
  public float MaxDrag = 0.9f;
  public AudioClip ReleaseSound;

  private Rigidbody _rb;
  private GameObject _jointPoint;
  private Vector2 _jPos, _mousePos, _dir;
  private AudioSource audioSource;
  private bool _isPressed = false, _isJointed = false;
  private float _dist;

  public void OnMouseDown() {
    _isPressed = true;
    _rb.isKinematic = true;
  }

  public void OnMouseUp() {
    _isPressed = false;
    _rb.isKinematic = false;
    Instantiate(Particle, _jointPoint.transform.position, Quaternion.FromToRotation(_jointPoint.transform.forward, new Vector3(_dir.x, _dir.y, 0)));

    audioSource.clip = ReleaseSound;
    audioSource.Play();

    StartCoroutine(Release());
  }

  public void PlayerClear() {
    Destroy(gameObject);
  }

  private void Start() {
    _rb = GetComponent<Rigidbody>();
    JF mentor = FindObjectOfType<JF>();
    mentor.OnClear += PlayerClear;
    audioSource = GetComponent<AudioSource>();
  }

  private void Update() {
    const float posZ = 15f;
    if (_isPressed == true && _isJointed == true) {
      _mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, posZ));
      _dist = Vector2.Distance(_jPos, _mousePos);
      _dir = (_mousePos - _jPos).normalized;

      if (_dist > MaxDrag)
        _rb.position = new Vector3(_jPos.x + _dir.x * MaxDrag, _jPos.y + _dir.y * MaxDrag, posZ);
      else
        _rb.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, posZ));
    }

    float minX = -8f, maxX = 8f, minY = -12f;
    if (_rb.position.x < minX || _rb.position.x > maxX || _rb.position.y < minY)
      GameOver();
  }

  private void OnTriggerEnter(Collider other) {
    if (other.tag == "Joint" || other.tag == "StartPoint") {
      _isJointed = true;
      _jointPoint = other.gameObject;
      transform.position = _jointPoint.transform.position;
      _jPos = new Vector2(_jointPoint.transform.position.x, _jointPoint.transform.position.y);
    }
  }

  private void OnTriggerExit(Collider other) {
    if (other.tag == "Joint" || other.tag == "StartPoint")
      _isJointed = false;
  }

  private IEnumerator Release() {
    yield return new WaitForSeconds(ReleaseTime);
    Destroy(_jointPoint);
  }
}
