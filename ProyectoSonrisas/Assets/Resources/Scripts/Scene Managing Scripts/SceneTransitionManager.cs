using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public FadeScreenManager fadeScreenManager;
    public void GoToScene(int sceneIndex)
    {
        //StartCoroutine(GoToSceneRoutine(sceneIndex));
        SceneManager.LoadScene(sceneIndex);
    }
    /*
    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        fadeScreenManager.FadeOut();
        yield return new WaitForSeconds(fadeScreenManager.fadeOutDuration);

        SceneManager.LoadScene(sceneIndex);
    }*/


    public void GoToSceneAsync(int sceneIndex)
    {
        //StartCoroutine(GoToSceneAsyncRoutine(sceneIndex));
        SceneManager.LoadScene(sceneIndex);
    }

    /*
    IEnumerator GoToSceneAsyncRoutine(int sceneIndex)
    {
        fadeScreenManager.FadeOut();
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while (timer <=fadeScreenManager.fadeOutDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        operation.allowSceneActivation = true;
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GoToSceneAsync(3);
        }
    }
}
