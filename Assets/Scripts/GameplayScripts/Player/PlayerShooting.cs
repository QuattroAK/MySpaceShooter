﻿using UnityEngine;

[RequireComponent(typeof(BulletsController))]
public class PlayerShooting : MonoBehaviour
{
    [Header("Components links")]
    [SerializeField] private BulletsController bulletsController;

    [Header("Shooting parametres")]
    [SerializeField] private float nextFire;
    [SerializeField] private float fireRate;
    [SerializeField] private float baseDamage;

    private Transform parentBulletObject;
    private bool stopFire;

    public void Init(Transform parentBulletObject)
    {
        this.parentBulletObject = parentBulletObject;
        bulletsController.Init(baseDamage);
    }

    public void Refresh()
    {
        bulletsController.RefreshBullets();
    }

    public void Fire()
    {
        if (Time.time > nextFire && !stopFire)
        {
            nextFire = Time.time + fireRate;
            SoundController.Instance.PlayAudio(TypeAudio.PlayerGunShot);
            bulletsController.SpawnBullet(parentBulletObject);

            PlayerData.PlayerStatisticData.CountPlayerShoots++;
        }
    }

    public void DisableEffects()
    {
        stopFire = true;
    }
}