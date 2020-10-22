using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Components links")]
    [SerializeField] private BulletsController bulletsController;

    [Header("Shooting parametres")]
    [SerializeField] private float nextFire;
    [SerializeField] private float fireRate;
    [SerializeField] private float baseDamage;

    private EnemyHealth enemyHealth;
    private Transform parentBulletObject;
    private bool stopFire;

    public void Init(Transform parentBulletObject, EnemyHealth enemyHealth)
    {
        this.parentBulletObject = parentBulletObject;
        bulletsController.Init(baseDamage);
        this.enemyHealth = enemyHealth;
    }

    public void Refresh()
    {
        bulletsController.RefreshBullets();
    }

    // TODO Скорее всего придется переделать логику стрельбы, чтобы было более реалистично. Например стрелять в игрока тогда, когда враг направлен на него лицом
    public void Attack()
    {
        if (Time.time > nextFire && !stopFire && enemyHealth.CurrentHealth > 0)
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
