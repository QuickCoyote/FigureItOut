using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelComplete : MonoBehaviour
{
    int LevelAmount = 50;
    private int currentLevel;

    private void Start()
    {
        CheckLevel();
    }

    void CheckLevel()
    {
        for (int i = 1; i <= LevelAmount; i++)
        {
            if (SceneManager.GetActiveScene().name == "Level" + i)
            {
                currentLevel = i;
                SaveMyGame();
            }
        }
    }

    void SaveMyGame()
    {
        int NextLevel = currentLevel + 1;
        if (NextLevel <= LevelAmount)
        {
            PlayerPrefs.SetInt("Level" + NextLevel.ToString(), 1);
        }

        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
