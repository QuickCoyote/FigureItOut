using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLockManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {

        public string LevelText;
        public int Unlock;
        public bool isInteractible;

        public Button.ButtonClickedEvent OnClick;
    }

    public Button[] levelButtons;
    public GameObject LEVELButton;
    public Transform Spacer;
    public List<Level> LevelList;

    private void Start()
    {
        
    }

    void FillOutList()
    {
        LevelButton prevButton = null;
        for (int i = 0; i <= LevelList.Count; i++)
        {
            GameObject newbutton = Instantiate(LEVELButton) as GameObject;
            LevelButton button = newbutton.GetComponent<LevelButton>();


            button.LevelText.text = LevelList[i].LevelText;
            Debug.Log(button.LevelText.text);

            if (i > 0)
            {

                if (PlayerPrefs.GetInt("Level" + prevButton.LevelText.text + "timetrial") == 1)
                {
                    LevelList[i - 1].Unlock = 1;
                    LevelList[i - 1].isInteractible = true;
                }

                if (PlayerPrefs.GetInt("Level" + prevButton.LevelText.text + "arena") == 1)
                {
                    LevelList[i - 1].Unlock = 1;
                    LevelList[i - 1].isInteractible = true;
                }

            }

            if (i < LevelList.Count)
            {

                if (PlayerPrefs.GetInt("Level" + button.LevelText.text) == 1)
                {
                    LevelList[i].Unlock = 1;
                    LevelList[i].isInteractible = true;
                }
            }
            button.unlocked = LevelList[i].Unlock;
            button.GetComponent<Button>().interactable = LevelList[i].isInteractible;
            button.GetComponent<Button>().onClick.AddListener(() => LoadLevel("Level" + button.LevelText.text));
            //button.GetComponent<Button>().onClick.AddListener(() => StarCor("Level" + button.LevelText.text));



            newbutton.transform.SetParent(Spacer);

            prevButton = button;
        }
        SAVE();
    }

    void SAVE()
    {
        {
            GameObject[] allbuttons = GameObject.FindGameObjectsWithTag("LevelButton");
            foreach (GameObject buttons in allbuttons)
            {
                LevelButton button = buttons.GetComponent<LevelButton>();
                PlayerPrefs.SetInt("Level" + button.LevelText.text, button.unlocked);
            }
        }
    }

    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    void LoadLevel(string value)
    {
        SceneManager.LoadScene(value);
        //LoadingScene.SetActive(true);
    }
}
