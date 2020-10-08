using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private SoundController soundController;
    [SerializeField] private UIController uIController;
    [SerializeField] private EnemiesManager enemiesManager;

    [Header("Game links")]
    [SerializeField] private Transform playerParentBulletObject;

    private float saveTimer;
    private float saveTime = 3f;

    private void Start()
    {
        playerController.Init(playerParentBulletObject);
        soundController.Init();
        uIController.Init(playerController);
        enemiesManager.Init(playerController);
    }

    private void FixedUpdate()
    {
        playerController.Refresh();
        enemiesManager.Refresh();
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