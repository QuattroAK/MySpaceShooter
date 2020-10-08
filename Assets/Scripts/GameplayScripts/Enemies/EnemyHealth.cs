using UnityEngine;

public class EnemyHealth : ObjectHealth
{
    private EnemyShooting enemyShooting;

    public void Init(EnemyShooting enemyShooting)
    {
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
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        
    }
}
