using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneData _allScenes;

    /// <summary>
    /// ���������� �������� ����� �� �������� ������������.
    /// </summary>
    /// <param name="sceneType">��� ����� �� ������������</param>
    public void LoadScene(SceneData.SceneType sceneType)
    {
        string sceneName = _allScenes.GetScene(sceneType).name;
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
    public void LoadSceneAsync(SceneData.SceneType sceneType)
    {
        string sceneName = _allScenes.GetScene(sceneType).name;
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
}