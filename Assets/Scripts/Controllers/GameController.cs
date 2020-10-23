using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private SoundController soundController;
    [SerializeField] private EnemiesManager enemiesManager;
    [SerializeField] private UIController uIController;

    [Header("Game links")]
    [SerializeField] private Transform playerParentBulletObject;
    [SerializeField] private Image gameVictory;
    [SerializeField] private Image gameOver;
    [SerializeField] private Text rewards;

    private float saveTime = 3f;
    private float saveTimer;
    private int score;

    private void Start()
    {
        playerController.Init(playerParentBulletObject);
        soundController.Init();
        enemiesManager.Init(playerController);
        uIController.Init(playerController);
        enemiesManager.OnEnemyDie += UpdateScore;
        playerController.OnGameOver += GameOver;
        enemiesManager.Victory += GameVictory;
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

    private void GameVictory()
    {
        DOVirtual.DelayedCall(2f, ShowGameVictory);
    }

    private void ShowGameVictory()
    {
        gameVictory.gameObject.SetActive(true);
        Time.timeScale = 0;
        rewards.text = $"{score}";
        SoundController.Instance.PlayAudio(TypeAudio.Victory);
    }

    private void UpdateScore(int score)
    {
        this.score += score;
        uIController.UpdateScore(this.score);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void SaveGame()
    {
        PlayerData.SaveSaves();
        Debug.Log($"Game saveed");
    }

    public void Exit()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
}