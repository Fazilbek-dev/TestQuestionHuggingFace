using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// ������������ ��������� ����. ���������� ���� ��� �����, ������� ������ ��������� ����� ���������.
/// </summary>
public enum SceneType
{
    MainMenu,
    Chat
    // ���������� ������ ����� �� �������������
}

/// <summary>
/// ����� ��� �������� ���� ��� ������������� ������ ��� ������� � ����.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// ��������� ��� ������� ������������ ����� ��������� ������������ � ������ ����� � Build Settings.
    /// </summary>
    [System.Serializable]
    public struct SceneMapping
    {
        public SceneType sceneType;
        public string sceneName; // ��� �����, ��� ������� � Build Settings
    }

    /// <summary>
    /// ������ ������������, ������� ������������� � ����������.
    /// </summary>
    [SerializeField] private SceneMapping[] scenes;

    /// <summary>
    /// ���������� �������� ����� �� �������� ������������.
    /// </summary>
    /// <param name="sceneType">��� ����� �� ������������</param>
    public void LoadScene(SceneType sceneType)
    {
        string sceneName = GetSceneName(sceneType);
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("�� ������� ����� ��� ����: " + sceneType);
        }
    }

    /// <summary>
    /// ����������� �������� ����� �� �������� ������������.
    /// </summary>
    /// <param name="sceneType">��� ����� �� ������������</param>
    public void LoadSceneAsync(SceneType sceneType)
    {
        string sceneName = GetSceneName(sceneType);
        if (!string.IsNullOrEmpty(sceneName))
        {
            StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
        }
        else
        {
            Debug.LogError("�� ������� ����� ��� ����: " + sceneType);
        }
    }

    /// <summary>
    /// Coroutine ��� ����������� �������� ����� � ������������� ���������.
    /// </summary>
    /// <param name="sceneName">��� ����� ��� ��������</param>
    /// <returns></returns>
    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            // operation.progress ��������� �������� �� 0 �� 0.9, ������� �����������
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log("��������: " + (progress * 100f).ToString("F0") + "%");
            // ����� ����� ��������� UI, ��������, progressBar.fillAmount = progress;
            yield return null;
        }
    }

    /// <summary>
    /// ����� ����� ����� �� �������� ������������.
    /// </summary>
    /// <param name="sceneType">��� �����</param>
    /// <returns>��� �����, ���� �������, ����� ������ ������</returns>
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