using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MyGame
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject[] hazards;
        [SerializeField] private Vector3 spawnValues;
        [SerializeField] private int hazardCount;
        [SerializeField] private float startWait;
        [SerializeField] private float spawnWait;
        [SerializeField] private float waveWait;
        [SerializeField] private Text scoreText;
        [SerializeField] private Text restartText;
        [SerializeField] private Text gameOverText;

        private int score;
        private bool gameOver;
        private bool restart;

        void Start()
        {
            gameOver = false;
            restart = false;
            restartText.text = "";
            gameOverText.text = "";
            score = 0;
            UpdateScore();
            StartCoroutine(SpawnWaves());
        }

        private void Update()
        {
            if (restart)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }

        private IEnumerator SpawnWaves()
        {
            yield return new WaitForSeconds(startWait);
            while (true)
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);
                    yield return new WaitForSeconds(spawnWait);
                }
                yield return new WaitForSeconds(waveWait);

                if (gameOver)
                {
                    restartText.text = "Press 'R' for Restart";
                    restart = true;
                    break;
                }
            }
        }

        public void AddScore(int newScoreValue)
        {
            score += newScoreValue;
            UpdateScore();
        }

        private void UpdateScore()
        {
            scoreText.text = $"Score: {score}";
        }

        public void GameOver()
        {
            gameOverText.text = "Game Over";
            gameOver = true;
        }
    }
}
