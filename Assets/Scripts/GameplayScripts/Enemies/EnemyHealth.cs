using UnityEngine;
using System;

public class EnemyHealth : ObjectHealth
{
    [SerializeField] private int scoreValue;

    private EnemyShooting enemyShooting;
    private Action<int> OnEnemyDie;

   public void Init(EnemyShooting enemyShooting, Action<int> OnEnemyDie)
   {
        this.OnEnemyDie += OnEnemyDie;
        this.enemyShooting = enemyShooting;
        base.InitObjectHealth();
   }

    public void Refresh()
    {

    }

    protected override void Death()
    {
        base.Death();
        enemyShooting.DisableEffects();
        OnEnemyDie?.Invoke(scoreValue);
        
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
    }
}
