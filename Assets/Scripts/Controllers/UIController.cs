using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("UI components")]
    [SerializeField] private Text text;
    [SerializeField] private Image healthBar;

    [Header("Controllers")]
    private PlayerController playerController;

    public void Init(PlayerController playerController)
    {
        this.playerController = playerController;
        playerController.OnDamage += HitEffects;
    }

    private void HitEffects()
    {
        healthBar.fillAmount = playerController.CurrentHealth / playerController.StartingHealth;
    }

    public void UpdateScore(int scoreValue)
    {
        text.text = $"Score: {scoreValue}";
    }
}
