﻿using System;

public class PlayerHealth : ObjectHealth
{
    public event Action OnDamage;
    public event Action OnGameOver;

    private PlayerShooting playerShooting;

    public void Init(PlayerShooting playerShooting, Action OnDamage, Action OnGameOver)
    {
        base.InitObjectHealth();
        this.playerShooting = playerShooting;
        this.OnGameOver += OnGameOver;
        this.OnDamage += OnDamage;
    }

    protected override void Death()
    {
        base.Death();
        playerShooting.DisableEffects();
        OnGameOver?.Invoke();
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        OnDamage?.Invoke();
    }
}