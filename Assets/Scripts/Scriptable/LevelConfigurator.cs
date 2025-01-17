using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData/LevelConfigurator", order = 1)]
public class LevelConfigurator : ScriptableObject
{
    public LevelInfo[] levelInfo;
}

[Serializable]
public class LevelInfo
{
    public int levelNumber;
    public int rows;
    public int columns;
}
