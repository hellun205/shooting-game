using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utils {
  public static bool IsPause { get; private set; }

  public static void Pause() {
    IsPause = true;
    Time.timeScale = 0;
  }

  public static void UnPause() {
    IsPause = false;
    Time.timeScale = 1;
  }

  public static void TogglePause() {
    if (IsPause) UnPause();
    else Pause();
  }
  
}