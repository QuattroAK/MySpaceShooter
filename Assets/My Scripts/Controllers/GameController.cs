using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private SoundController soundController;

    private float saveTimer;
    private float saveTime = 3f;

    private void Start()
    {
        playerController.Init();
        soundController.Init();
    }

    private void FixedUpdate()
    {
        playerController.Refresh();
    }

    private void Update()
    {
        CheckSave();
    }

    private void CheckSave()
    {
        saveTimer += Time.deltaTime;
        if (saveTimer >= saveTime)
        {
            PlayerData.SaveSaves();
            saveTimer = 0;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerData.SaveSaves();
    }
}