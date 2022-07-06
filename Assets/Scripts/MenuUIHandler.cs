using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;
using System.IO;

public class MenuUIHandler : MonoBehaviour
{
    [System.Serializable]
    class SaveData
    {
        public int saveScore;
        public string savePlayer;
    }

    public static MenuUIHandler Instance;
    [SerializeField] TMP_InputField InputBox;
    [SerializeField] TMP_Text highScoreDisplay;
    public GameObject Menu;
    public string localPlayerName;
    public string storedWinner = "Nobody";
    public int storedHighScore = 0;

    private void Start()//Load highscores
    {
        Load();
        highScoreDisplay.text = "Best: " + storedWinner + ": " + storedHighScore;
    }

    private void Awake()//Keeps script alive
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()// Close menu, load game scene
    {
        Menu.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void Exit()// Save and Exit
    {
        Save();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SetPlayerName()// Sets player name in this script
    {
        if (InputBox.text != null)
            localPlayerName = InputBox.text;
        else
            localPlayerName = "Nobody";
    }

    public void StoreHighScore(int score)//Tracks current high score and the player that achieved it, updates main menu score text
    {
        storedWinner = localPlayerName;
        storedHighScore = score;
        highScoreDisplay.text = "Best: " + storedWinner + ": " + storedHighScore;
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.saveScore = storedHighScore;
        data.savePlayer = storedWinner;
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "savefile.json", json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            storedHighScore = data.saveScore;
            storedWinner = data.savePlayer;
        }
    }
}
