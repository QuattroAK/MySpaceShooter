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

    public void Init()
    {
        bulletsController.Init();
    }

    public void Refresh()
    {
        Fire();
        bulletsController.RefreshBullets();
    }

    private void Fire()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            SoundController.Instance.PlayAudio(TypeAudio.GunShot);
            bulletsController.SpawnBullet();
        }
    }
}