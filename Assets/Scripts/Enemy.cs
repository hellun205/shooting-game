using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour {
  public int maxHp = 5;
  private int hp;
  private Rigidbody2D rb;
  
  

  private void Awake() {
    rb = GetComponent<Rigidbody2D>();
    hp = maxHp;
  }

  private void Update() {
    rb.AddForce(Vector2.left * 0.75f);

    if (hp <= 0 || transform.position.y < -50) {
      Destroy(gameObject);
      Shooter.score += hp <= 0 ? 1 : -1;
    }
  }

  public void Hit() {
    hp--;
  }


  private void OnCollisionEnter2D(Collision2D col) {
    if (col.gameObject.CompareTag("Player")) {
      rb.AddForce(Vector2.right * 500f);
      Hit();
    }
  }
}