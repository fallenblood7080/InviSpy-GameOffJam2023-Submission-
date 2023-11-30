using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private GameObject transitionScreen;

    private void Start()
    {
        transitionScreen.SetActive(false);
    }
    public void LoadMainMenu()
    {
        StartCoroutine(LoadSceneAsyncWithTransition(0));
    }
    public void LoadGame()
    {
        StartCoroutine(LoadSceneAsyncWithTransition(1));
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator LoadSceneAsyncWithTransition(int index)
    {
        // Activate the transition screen
        transitionScreen.SetActive(true);

        // Start loading the scene asynchronously in the background
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);

        // Don't allow the scene to be activated until loading is complete
        asyncOperation.allowSceneActivation = false;

        // While the scene is not yet loaded, continue the loop
        while (!asyncOperation.isDone)
        {

            // If the loading has reached 90%, allow activation of the scene
            if (asyncOperation.progress >= 0.9f)
            {
                // Deactivate the transition screen

                // Allow the scene to be activated
                asyncOperation.allowSceneActivation = true;
            }

            yield return null; // Wait for the next frame
        }
    }
}
