using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private LoadScreen loadScreenPrefab;

    private Canvas canvas;
    private LoadScreen loadScreen;

    protected virtual void Awake ()
    {
        canvas = GetComponentInChildren<Canvas>();
    }

    public void LoadScene(string _level)
    {
        loadScreen = Instantiate(loadScreenPrefab);
        loadScreen.transform.SetParent(canvas.transform, false);
        StartCoroutine(HandleSceneLoad(_level));
    }

    public void LoadScene(LevelData _levelData)
    {
        loadScreen = Instantiate(loadScreenPrefab);
        loadScreen.transform.SetParent(canvas.transform, false);
        loadScreen.SetEssentials(_levelData);
        StartCoroutine(HandleSceneLoad(_levelData.levelName));
    }

    public void ToggleOptionsMenu()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }

    private IEnumerator HandleSceneLoad(string _sceneName)
    {
        loadScreen.SetProgress(Random.Range(0.05f, 0.2f));
        yield return new WaitForSeconds(0.25f);

        AsyncOperation _loadOperation = SceneManager.LoadSceneAsync(_sceneName);

        while (_loadOperation.progress < 1f)
        {
            loadScreen.SetProgress(_loadOperation.progress);
            yield return 0;
        }

        Destroy(loadScreen);
    }
}
