using System;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

public class Shooter : MonoBehaviour {
  public KeyCode shootKey = KeyCode.Space;
  public KeyCode changeModeKey = KeyCode.B;
  public KeyCode reloadKey = KeyCode.R;
  public GameObject bullet;
  public Transform singleFirePosition;
  public Transform autoFirePosition;
  public Sprite singleFireSprite;
  public Sprite autoFireSprite;
  public GameObject weaponObject;
  public int maxSingleFireAmmo = 8;
  public int maxAutoFireAmmo = 24;
  public int reloadTime = 60;
  public int maxHp = 100;

  public Information leftAmmoInfo;
  public Information hpInfo;
  public Information modeInfo;
  public Progressbar leftAmmoPrg;
  public Progressbar hpPrg;
  public Information scoreInfo;

  public static int score;

  private ShootingMode mode;
  private int leftAmmoCount;
  private bool isReloading;
  private int frame;
  private int frameAuto = 0;
  private const int AutoFireFrame = 30;
  private int tempAmmo;
  private int hp;

  private SpriteRenderer weaponSr;

  private void Awake() {
    mode = ShootingMode.Single;
    leftAmmoCount = maxSingleFireAmmo;
    tempAmmo = maxAutoFireAmmo;
    hp = maxHp;
    weaponSr = weaponObject.GetComponent<SpriteRenderer>();
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
      
      default:
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
      weaponSr.sprite =
        (mode == ShootingMode.Single ? singleFireSprite : autoFireSprite);

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
    leftAmmoInfo.SetText($"ammo: {leftAmmoCount}/{GetMaxAmmo()}{(isReloading ? " (reloading)" : "")}");
    modeInfo.SetText($"mode: {(mode == ShootingMode.Single ? "single" : "auto")}");
    leftAmmoPrg.SetMaxValue(GetMaxAmmo());
    leftAmmoPrg.SetValue(leftAmmoCount);
    scoreInfo.SetText($"{score} point");

    hpInfo.SetText($"hp: {hp}/{maxHp}");
    hpPrg.SetMaxValue(maxHp);
    hpPrg.SetValue(hp);
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.CompareTag($"Enemy")) {
      hp--;
    }
  }
}