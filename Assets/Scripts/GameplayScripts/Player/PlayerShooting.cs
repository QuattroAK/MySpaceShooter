using UnityEngine;

[RequireComponent(typeof(BulletsController))]
public class PlayerShooting : MonoBehaviour
{
    [Header("Components links")]
    [SerializeField] private BulletsController bulletsController;

    [Header("Shooting parametres")]
    [SerializeField] private float nextFire;
    [SerializeField] private float fireRate;
    [SerializeField] private float baseDamage;

    private bool stopFire;

    public void Init()
    {
        bulletsController.Init(baseDamage);
    }

    public void Refresh()
    {
        Fire();
        bulletsController.RefreshBullets();
    }

    private void Fire()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire && !stopFire)
        {
            nextFire = Time.time + fireRate;
            SoundController.Instance.PlayAudio(TypeAudio.PlayerGunShot);
            bulletsController.SpawnBullet();

            PlayerData.PlayerStatisticData.CountPlayerShoots++;
        }
    }

    public void DisableEffects()
    {
        stopFire = true;
    }
}