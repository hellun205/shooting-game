using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Information : MonoBehaviour {
  public static List<Information> InfoList = new List<Information>();
  public string uniqueName;
  

  private TextMeshProUGUI tmp;
  private void Awake() {
    tmp = GetComponent<TextMeshProUGUI>();
    InfoList.Add(this);
  }

  public static void SetText(string name, string text) {
    foreach (var info in InfoList.FindAll(x => x.uniqueName == name)) {
      info.tmp.SetText(text);
    }
  }
}