using UnityEngine;

public class PlayerHealth : ObjectHealth
{
    public void Init(/* какие-то аргументы */)
    {
        base.InitObjectHealth();
        /* какие-то аргументы */
    }

    protected override void Death()
    {
        base.Death();
        //playerShooting.DisableEffects();
        //OnGameOver?.Invoke();
    }
}