using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [Header("Components links")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private PlayerHealth playerHealth;

    public event Action OnGameOver;
    public event Action OnDamage;

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

    public void Init(Transform parentBulletObject)
    {
        playerMovement.Init(playerHealth);
        playerShooting.Init(parentBulletObject);
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
