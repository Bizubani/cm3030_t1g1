using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class loadingScreenToLevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;
    private float progress;

    void Start()
    {
        loadingScreen.SetActive(false);
    }

    public void LoadGameWorld(int sceneIndex)
    {
        loadingScreen.SetActive(true);

        WaitForLoadingBar(10f);
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        yield return new WaitForSeconds(10f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log(operation.progress);

            slider.value = progress;
            progressText.text = progress * 100 + "%";

            yield return null;
        }
    }

    IEnumerator WaitForLoadingBar(float seconds)
    {
         float currentTime = seconds;
     
         while (currentTime > 0)
         {
              currentTime -= Time.deltaTime;

              progress += Random.Range(0,2)/10;
              slider.value = progress;
              progressText.text = progress * 100 + "%";

              yield return null;
         }
     
         Debug.Log(seconds + " seconds have passed! And I'm done waiting!");
    }

}
