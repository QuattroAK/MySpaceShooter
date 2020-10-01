using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private EnemyShooting enemyShooting;

    public void Init(PlayerController playerController, Action<int> OnEnemyDie)
    {
        enemyMovement.Init(playerController.transform);
        enemyHealth.Init();
        enemyShooting.Init();
    }

    public void Refresh()
    {
        enemyMovement.Refresh();
        enemyHealth.Refresh();
        enemyShooting.Refresh();
    }
}
