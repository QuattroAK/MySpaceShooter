using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public event Action OnDamage;
    public event Action OnGameOver;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private PlayerHealth playerHealth;

    #region Properties

    public Transform Transform
    {
        get
        {
            return gameObject.transform;
        }
    }

    public bool IsAlive
    {
        get
        {
            return playerHealth.IsAlive;
        }
    }

    public float CurrentHealth
    {
        get
        {
            return playerHealth.CurrentHealth;
        }
    }

    public float StartingHealth
    {
        get
        {
            return playerHealth.StartingHealth;
        }
    }

    #endregion

    public void Init()
    {
        playerMovement.Init();
        playerShooting.Init();
        playerHealth.Init(playerShooting, OnDamageHandler, OnGameOverHandler);
    }

    public void Refresh()
    {
        playerMovement.Refresh();
        playerShooting.Refresh();
    }

    private void OnGameOverHandler()
    {
        OnGameOver?.Invoke();
    }

    private void OnDamageHandler()
    {
        OnDamage?.Invoke();
    }

    public void TakeDamage(int amount)
    {
        playerHealth.TakeDamage(amount);
    }
}
