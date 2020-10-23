using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    [Header("Components links")]
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private EnemyShooting enemyShooting;

    public void Init(PlayerController playerController, Action<int, int> OnEnemyDie, Transform parentBullet, Transform targetPatrol, Transform targetAsteroid)
    {
        enemyMovement.Init(playerController.transform, targetPatrol, targetAsteroid, enemyShooting);
        enemyHealth.Init(enemyShooting, OnEnemyDie);
        enemyShooting.Init(parentBullet, enemyHealth);
    }

    public void Refresh()
    {
        enemyMovement.Refresh();
        enemyShooting.Refresh();
    }
}
