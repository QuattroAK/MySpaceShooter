using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingController : MonoBehaviour
{
    private void Start()
    {
        PlayerData.Init();
        SceneManager.LoadScene(1);
    }
}