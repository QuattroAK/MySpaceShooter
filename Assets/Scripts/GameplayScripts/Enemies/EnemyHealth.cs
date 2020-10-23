using UnityEngine;
using System;

public class EnemyHealth : ObjectHealth
{
    [Header("Points parameters")]
    [SerializeField] private int scoreValue;

    private EnemyShooting enemyShooting;
    private int countEnemy = 1;

    private Action<int, int> OnEnemyDie;

    public void Init(EnemyShooting enemyShooting, Action<int, int> OnEnemyDie)
    {
        this.OnEnemyDie += OnEnemyDie;
        this.enemyShooting = enemyShooting;
        base.InitObjectHealth();
    }

    protected override void Death()
    {
        base.Death();
        enemyShooting.DisableEffects();
        OnEnemyDie?.Invoke(scoreValue, countEnemy);
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
    }
}
