using UnityEngine.SceneManagement;
using UnityEngine;

public class ExitMainMenu : MonoBehaviour
{
    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
