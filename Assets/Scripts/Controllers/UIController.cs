using UnityEngine;

public class UIController : MonoBehaviour
{
    private PlayerController playerController;

    public void Init(PlayerController playerController)
    {
        this.playerController = playerController;

        playerController.OnDamage += HitEffects;
        playerController.OnGameOver += GameOver;
    }

    private void HitEffects()
    {

    }

    private void GameOver()
    {

    }
}
