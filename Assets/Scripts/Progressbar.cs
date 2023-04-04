using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Progressbar : MonoBehaviour {
  public float maxValue = 100f;

  private float value;
  public float Value {
    get => value;
    set {
      this.value = value;
      image.fillAmount = value / maxValue;
    }
  }

  private Image image;

  public void Awake() {
    image = GetComponent<Image>();
  }

  public void SetValue(float value) => Value = value;

  public float GetValue() => value;

  public void SetMaxValue(float value) => maxValue = value;
  
}