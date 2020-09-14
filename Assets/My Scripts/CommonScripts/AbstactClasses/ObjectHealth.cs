using UnityEngine;

public abstract class ObjectHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;

    private bool isDead;
    private float currentHealth;

    #region Properties

    public float StartingHealth
    {
        get
        {
            return startingHealth;
        }
    }

    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
    }

    public bool IsAlive
    {
        get
        {
            return currentHealth > 0;
        }
    }

    #endregion

    public void Init()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        //SoundController.Instance.PlayAudio(TypeAudio.);

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;
        //playerShooting.DisableEffects();
        //SoundController.Instance.PlayAudio(TypeAudio.PlayerDeath);
        //OnGameOver?.Invoke();
    }
}