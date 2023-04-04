using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Information : MonoBehaviour {
  private TextMeshProUGUI tmp;
  private string text;
  public string Text {
    get => text;
    set {
      text = value;
      tmp.SetText(value);
    }
  }
  
  private void Awake() {
    tmp = GetComponent<TextMeshProUGUI>();
  }

  public void SetText(string text) => Text = text;

  public string GetText() => Text;
  

}