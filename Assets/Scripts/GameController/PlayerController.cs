using UnityEngine;
using System.IO;
public class PlayerController : Singleton<PlayerController>
{
    public int currentLevel = 0;
    public int score = 0;
    private const string dataFileName = "playerdata.json";
    private UserData currentUserdData;
    private void OnEnable()
    {
        currentUserdData = new UserData(0, 0);
        LoadData();
        ActionController.Instance.onLevelComplete += OnLevelComplete;
    }
    private void OnDisable()
    {
        if (ActionController.Instance == null) return;
        ActionController.Instance.onLevelComplete -= OnLevelComplete;
    }
    private void OnLevelComplete()
    {
        currentLevel++;
        SaveData(currentLevel, ScoreConfigController.Instance.GetCurrentScore());
    }
    public void SaveData(int level, int score)
    {
        UserData userData = new UserData(level, score);
        string json = JsonUtility.ToJson(userData, true);
        string filePath = Path.Combine(Application.persistentDataPath, dataFileName);
        File.WriteAllText(filePath, json);
    }
    public void LoadData()
    {   
        string filePath = Path.Combine(Application.persistentDataPath, dataFileName);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            currentUserdData= JsonUtility.FromJson<UserData>(json);
            //currentLevel = currentUserdData.level;
            currentLevel = 0;
            score = currentUserdData.score;
        }
        /*else
        {
            SaveData(currentLevel,ScoreConfigManager.Instance.GetCurrentScore());
        }*/
    }
}
[System.Serializable]
public class UserData
{
    public int level;
    public int score;

    public UserData(int level, int score)
    {
        this.level = level;
        this.score = score;
    }
}