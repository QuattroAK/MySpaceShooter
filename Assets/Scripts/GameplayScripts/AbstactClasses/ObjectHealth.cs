using UnityEngine;

public abstract class ObjectHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private TypeAudio takeDamageAudio;
    [SerializeField] private TypeAudio deathAudio;

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

    protected virtual void InitObjectHealth()
    {
        currentHealth = startingHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        if (isDead) return;
        currentHealth -= amount;
        SoundController.Instance.PlayAudio(takeDamageAudio);

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        isDead = true;
        SoundController.Instance.PlayAudio(deathAudio);
        gameObject.SetActive(false);

    }
}