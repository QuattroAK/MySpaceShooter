using UnityEngine;

public class EnemyShooting : MonoBehaviour
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

    // TODO Скорее всего придется переделать логику стрельбы, чтобы было более реалистично. Например стрелять в игрока тогда, когда враг направлен на него лицом
    public void Attack()
    {
        if (Time.time > nextFire && !stopFire)
        {
            nextFire = Time.time + fireRate;
            SoundController.Instance.PlayAudio(TypeAudio.EnemyGunShot);
            bulletsController.SpawnBullet(parentBulletObject);
        }
    }
    public void DisableEffects()
    {
        stopFire = true;
    }
}
