using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace Controllers
{
    public class LoadingSceneController : MonoBehaviour
    {
        [Header("UI components")]
        [SerializeField] private Image loadingFillImage = null;
        [SerializeField] private Text textLoadPercent;

        [Header("Loading settings")]
        [SerializeField] private float fakeLoadingTimeInSeconds = 1;

        private float fillLoadingStep = 0;
        private float updateTime = 0.01f;
        private bool fakeLoadingFinished = false;

        private void Start()
        {
            PlayerData.Init();
            fillLoadingStep = updateTime / fakeLoadingTimeInSeconds;
            StartCoroutine(StartFakeLoading());
        }

        private IEnumerator StartFakeLoading()
        {
            while (!fakeLoadingFinished)
            {
                loadingFillImage.fillAmount += fillLoadingStep;
                textLoadPercent.text = string.Format("{0:0}%", loadingFillImage.fillAmount * 100);

                if (loadingFillImage.fillAmount == 1f)
                {
                    fakeLoadingFinished = true;
                    StartCoroutine(LoadAsyncScene());
                }

                yield return new WaitForSeconds(updateTime);
            }
        }

        private IEnumerator LoadAsyncScene()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}