using UnityEngine;

public class EnemyShooting : MonoBehaviour
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
        Attack();
        bulletsController.RefreshBullets();
    }

    private void Attack()
    {
        if(Time.time > nextFire && !stopFire)
        {
            nextFire = Time.time + fireRate;
            SoundController.Instance.PlayAudio(TypeAudio.EnemyGunShot);
            bulletsController.SpawnBullet();
        }
    }
}
