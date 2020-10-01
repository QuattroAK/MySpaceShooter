using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class LoadingController : MonoBehaviour
{
    [Header("Загружаемая сцена")]
    [SerializeField] private int sceneNumber;

    [Header("Остальные объекты")]
    [SerializeField] Image imageLoading;
    [SerializeField] Text textLoadPercent;

    private void Start()
    {
        PlayerData.Init();
        StartCoroutine(AsyncLoad());
    }

    private IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNumber);

        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            imageLoading.fillAmount = progress;
            textLoadPercent.text = string.Format("{0:0}%", progress * 100);
            if (Input.anyKeyDown)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}