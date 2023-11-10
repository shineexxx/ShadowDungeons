using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneIndexes", menuName = "GeneralData/SceneIndexes", order = 100)]
public class BuildSceneIndexes : ScriptableObject
{
    [SerializeField] private List<SceneData> _sceneIndexes;
    public List<SceneData> SceneIndexes => _sceneIndexes;
}

[Serializable]
public struct SceneData
{
    public string SceneName;
    public int SceneIndex;
}

