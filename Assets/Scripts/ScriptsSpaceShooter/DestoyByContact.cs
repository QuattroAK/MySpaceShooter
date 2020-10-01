using UnityEngine;

namespace MyGame
{
    public class DestoyByContact : MonoBehaviour
    {
        [SerializeField] private GameObject explosin;
        [SerializeField] private GameObject playerExplosin;
        [SerializeField] private int scoreValue;
        
        private GameController gameController;

        private void Start()
        {
            GameObject gameControllerObject = GameObject.FindWithTag("GameController");
            if(gameControllerObject != null)
            {
                gameController = gameControllerObject.GetComponent<GameController>();
                
            }
            if(gameController == null)
            {
                Debug.Log("Cannot find 'GameController script");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
            {
                return;
            }

            if (explosin != null)
            {
                Instantiate(explosin, transform.position, transform.rotation);
            }

            if (other.CompareTag("Player"))
            {
                Instantiate(playerExplosin, other.transform.position, other.transform.rotation);
                gameController.GameOver();
            }

            gameController.AddScore(scoreValue);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
