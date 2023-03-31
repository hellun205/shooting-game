using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {
  public KeyCode leftKey = KeyCode.LeftArrow;
  public KeyCode rightKey = KeyCode.RightArrow;
  public KeyCode jumpKey = KeyCode.UpArrow;
  public float speed = 0.9f;

  private bool isJumping;

  private Rigidbody2D rb;
  private void Awake() {
    rb = GetComponent<Rigidbody2D>();
  }

  private void Update() {
    if (Input.GetKey(leftKey)) {
      rb.AddForce(Vector2.left * speed);
    }

    if (Input.GetKey(rightKey)) {
      rb.AddForce(Vector2.right * speed);
    }

    if (!isJumping && Input.GetKey(jumpKey)) {
      rb.AddForce(Vector2.up * 350f);
      isJumping = true;
    }
  }

  private void OnCollisionEnter2D(Collision2D col) {
    if (isJumping && col.gameObject.CompareTag($"Floor")) {
      isJumping = false;
    }
  }
}