using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private PlayerHealth playerHealth;

    public void Init()
    {
        playerShooting.Init();
        playerHealth.Init();
    }

    public void Refresh()
    {
        playerMovement.Refresh();
        playerShooting.Refresh();
    }
}
