using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneData _allScenes;

    /// <summary>
    /// Синхронная загрузка сцены по значению перечисления.
    /// </summary>
    /// <param name="sceneType">Тип сцены из перечисления</param>
    public void LoadScene(SceneData.SceneType sceneType)
    {
        string sceneName = _allScenes.GetScene(sceneType).name;
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Не найдена сцена для типа: " + sceneType);
        }
    }

    /// <summary>
    /// Асинхронная загрузка сцены по значению перечисления.
    /// </summary>
    /// <param name="sceneType">Тип сцены из перечисления</param>
    public void LoadSceneAsync(SceneData.SceneType sceneType)
    {
        string sceneName = _allScenes.GetScene(sceneType).name;
        if (!string.IsNullOrEmpty(sceneName))
        {
            StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
        }
        else
        {
            Debug.LogError("Не найдена сцена для типа: " + sceneType);
        }
    }

    /// <summary>
    /// Coroutine для асинхронной загрузки сцены с отслеживанием прогресса.
    /// </summary>
    /// <param name="sceneName">Имя сцены для загрузки</param>
    /// <returns></returns>
    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            // operation.progress принимает значения от 0 до 0.9, поэтому нормализуем
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log("Загрузка: " + (progress * 100f).ToString("F0") + "%");
            // Здесь можно обновлять UI, например, progressBar.fillAmount = progress;
            yield return null;
        }
    }
}