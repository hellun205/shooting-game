using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {
  private Rigidbody2D rb;

  public float speed = 5f;
  public int despawnTime = 3000; 
  private bool isFiring = true;
  private int frame;

  private void Awake() {
    rb = GetComponent<Rigidbody2D>();
  }

  private void Update() {
    if (isFiring) {
      rb.transform.Translate(Vector3.right * (Time.deltaTime * speed));
    }

    if (frame >= despawnTime) {
      Destroy(gameObject);
    }

    frame++;
  }

  private void OnTriggerEnter2D(Collider2D col) {

    var enemy = col.gameObject.GetComponent<Enemy>();
    if (enemy != null) {
      enemy.Hit();
    }
    if (!col.gameObject.CompareTag($"Bullet")) Destroy(gameObject);
  }
}