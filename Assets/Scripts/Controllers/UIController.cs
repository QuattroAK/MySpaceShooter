using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image gameOver;
    [SerializeField] private Image pauseMenu;

    private PlayerController playerController;

    public void Init(PlayerController playerController)
    {
        this.playerController = playerController;

        playerController.OnDamage += HitEffects;
        playerController.OnGameOver += GameOver;
    }

    private void HitEffects()
    {
        healthBar.fillAmount = playerController.CurrentHealth / playerController.StartingHealth;
    }

    private void GameOver()
    {
        if (!playerController.IsAlive)
        {
            DOVirtual.DelayedCall(2f, ShowGameOver);
        }
    }

    private void ShowGameOver()
    {
        gameOver.gameObject.SetActive(true);
    }

    public void UpdateScore(int scoreValue)
    {
        text.text = $"Score: {scoreValue}";
    }

    public void Pausee()
    {
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
    }
}
