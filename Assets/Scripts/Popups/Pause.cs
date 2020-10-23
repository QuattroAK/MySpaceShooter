using UnityEngine;

public class Pause : MonoBehaviour
{
    public void PauseButton()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }
}
