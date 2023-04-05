using UnityEngine;

public class EntitySpawner : MonoBehaviour {
  public Object target;
  public int respawnDelay = 500;

  private int frame;
  private void Update() {
    if (frame >= respawnDelay) {
      frame = 0;
      Instantiate(target, transform.position, transform.rotation);
    }

    if (!Utils.IsPause) frame++;
  }
}