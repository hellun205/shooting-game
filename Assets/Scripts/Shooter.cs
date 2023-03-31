using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Shooter : MonoBehaviour {
  public KeyCode shootKey = KeyCode.Space;
  public KeyCode changeModeKey = KeyCode.B;
  public KeyCode reloadKey = KeyCode.R;
  public GameObject bullet;
  public Transform singleFirePosition;
  public Transform autoFirePosition;
  public Object singleFireSprite;
  public Object autoFireSprite;
  public GameObject weaponObject;
  public int maxSingleFireAmmo = 8;
  public int maxAutoFireAmmo = 24;
  public int reloadTime = 60;

  private ShootingMode mode;
  private int leftAmmoCount;
  private bool isReloading;
  private int frame;
  private int frameAuto = 0;
  private const int AutoFireFrame = 30;
  private int tempAmmo;

  private void Awake() {
    mode = ShootingMode.Single;
    leftAmmoCount = maxSingleFireAmmo;
    tempAmmo = maxAutoFireAmmo;
  }

  private void Update() {
    switch (mode) {
      case ShootingMode.Auto:
        if (Input.GetKey(shootKey)) {
          frameAuto++;
          if (frameAuto >= AutoFireFrame) {
            frameAuto = 0;
            Fire();
          }
        } else {
          frameAuto = 0;
        }

        break;

      case ShootingMode.Single:
        if (Input.GetKeyDown(shootKey)) Fire();
        break;
    }

    if (!isReloading && Input.GetKeyDown(changeModeKey)) {
      int tmp;
      mode = mode == ShootingMode.Auto ? ShootingMode.Single : ShootingMode.Auto;
      tmp = leftAmmoCount;
      leftAmmoCount = tempAmmo;
      tempAmmo = tmp;
      weaponObject.GetComponent<SpriteRenderer>().sprite =
        (Sprite)(mode == ShootingMode.Single ? singleFireSprite : autoFireSprite);

    }

    if (!isReloading && Input.GetKeyDown(reloadKey)) {
      isReloading = true;
    }

    if (isReloading) {
      if (leftAmmoCount < GetMaxAmmo()) {
        if (frame % reloadTime == 0) leftAmmoCount++;
      } else {
        isReloading = false;
        leftAmmoCount = GetMaxAmmo();
        frame = 0;
      }

      frame++;
    }

    RefreshInformation();
  }

  private void Fire() {
    if (!isReloading && leftAmmoCount > 0) {
      var pos = GetFirePosition();
      Instantiate(bullet, pos.position, pos.rotation);
      leftAmmoCount--;
    } else {
      isReloading = true;
    }
  }

  private Transform GetFirePosition() => mode == ShootingMode.Single ? singleFirePosition : autoFirePosition;

  private int GetMaxAmmo() => mode == ShootingMode.Single ? maxSingleFireAmmo : maxAutoFireAmmo;

  private void RefreshInformation() {
    Information.SetText("left_ammo", $"ammo: {leftAmmoCount}/{GetMaxAmmo()}{(isReloading ? " (reloading)" : "")}");
    Information.SetText("mode", $"mode: {(mode == ShootingMode.Single ? "single" : "auto")}");
    Progressbar.SetValue("left_ammo_progress", leftAmmoCount);
    Progressbar.SetMaxValue("left_ammo_progress", GetMaxAmmo());
  }
}