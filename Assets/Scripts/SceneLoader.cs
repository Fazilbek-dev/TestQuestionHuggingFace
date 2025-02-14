using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Перечисление доступных сцен. Добавляйте сюда все сцены, которые хотите загружать через загрузчик.
/// </summary>
public enum SceneType
{
    MainMenu,
    Chat
    // Добавляйте другие сцены по необходимости
}

/// <summary>
/// Класс для загрузки сцен без использования строки или индекса в коде.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Структура для задания соответствия между значением перечисления и именем сцены в Build Settings.
    /// </summary>
    [System.Serializable]
    public struct SceneMapping
    {
        public SceneType sceneType;
        public string sceneName; // Имя сцены, как указано в Build Settings
    }

    /// <summary>
    /// Массив соответствий, который настраивается в инспекторе.
    /// </summary>
    [SerializeField] private SceneMapping[] scenes;

    /// <summary>
    /// Синхронная загрузка сцены по значению перечисления.
    /// </summary>
    /// <param name="sceneType">Тип сцены из перечисления</param>
    public void LoadScene(SceneType sceneType)
    {
        string sceneName = GetSceneName(sceneType);
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
    public void LoadSceneAsync(SceneType sceneType)
    {
        string sceneName = GetSceneName(sceneType);
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

    /// <summary>
    /// Поиск имени сцены по значению перечисления.
    /// </summary>
    /// <param name="sceneType">Тип сцены</param>
    /// <returns>Имя сцены, если найдено, иначе пустая строка</returns>
    private string GetSceneName(SceneType sceneType)
    {
        foreach (var mapping in scenes)
        {
            if (mapping.sceneType == sceneType)
            {
                return mapping.sceneName;
            }
        }
        return string.Empty;
    }
}