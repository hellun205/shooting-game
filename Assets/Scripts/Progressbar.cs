using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Progressbar : MonoBehaviour {
  public static List<Progressbar> PbList = new List<Progressbar>();
  public string uniqueName;
  public float maxValue = 100f;

  private Image image;

  public void Awake() {
    image = GetComponent<Image>();
    PbList.Add(this);
  }
  public void SetValue(float value) {
    image.fillAmount = value / maxValue;
  }

  public float GetValue() => image.fillAmount;

  public void SetMaxValue(float value) => maxValue = value;
  
  public static void SetValue(string name, float value) {
    foreach (var info in PbList.FindAll(x => x.uniqueName == name)) {
      info.SetValue(value);
    }
  }
  
  public static void SetMaxValue(string name, float value) {
    foreach (var info in PbList.FindAll(x => x.uniqueName == name)) {
      info.SetMaxValue(value);
    }
  }
}