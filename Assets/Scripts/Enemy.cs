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
    rb.AddForce(Vector2.left * 0.8f);
    if (hp <= 0) {
      Destroy(gameObject);
    }
    
  }

  public void Hit() {
    hp--;
  }
  

  private void OnCollisionEnter2D(Collision2D col) {
    rb.AddForce(Vector2.right * 5f);
  }
  
}