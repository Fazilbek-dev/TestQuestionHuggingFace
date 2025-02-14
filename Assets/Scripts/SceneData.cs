using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SceneData;

[CreateAssetMenu(fileName = "SceneData", menuName = "ScriptableObjects/SceneData")]
public class SceneData : ScriptableObject
{
    public enum SceneType
    {
        MainMenu,
        Chat
        // Добавляйте другие сцены по необходимости
    }

    [System.Serializable]
    public struct SceneMapping
    {
        public SceneType sceneType;
        public SceneAsset scene; // Имя сцены, как указано в Build Settings
    }

    public SceneMapping[] scenes;

    public SceneAsset GetScene(SceneType sceneType)
    {
        foreach (var mapping in scenes)
        {
            if (mapping.sceneType == sceneType)
            {
                return mapping.scene;
            }
        }
        return scenes[0].scene;
    }
}