using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private SoundController soundController;


    private void Start()
    {
        playerController.Init();
        soundController.Init();
    }

    private void FixedUpdate()
    {
        playerController.Refresh();
    }
}
