using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private List<Button> levelButtons;
    void Start()
    {
        int complitedLevelsCount = PlayerPrefs.GetInt("ComplitedLevelsCount");
        for (int i = 0; i < levelButtons.Count; i++)
        {
            int levelIndex = i + 1;

            if (Language.Instance.currentLanguage == "ru")
            {
                levelButtons[i].GetComponentInChildren<TMP_Text>().text = "Уровень " + levelIndex;
            }
            else
            {
                levelButtons[i].GetComponentInChildren<TMP_Text>().text = "Level " + levelIndex;

            }

            int sceneIndex = levelIndex;

            levelButtons[i].onClick.AddListener(() => LoadLevel(sceneIndex));

            if (i  <= complitedLevelsCount)
            {
                levelButtons[i].interactable = true;
            }
        }
    }

    void LoadLevel(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public int GetLevelCount()
    {
        return levelButtons.Count;
    }
}
