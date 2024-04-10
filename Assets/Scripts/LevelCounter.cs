using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCounter : MonoBehaviour
{
    [SerializeField] MainMenuScript menuScript;

    private int levelCount;

    public static LevelCounter Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitLevelCount();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitLevelCount()
    {
        levelCount = menuScript.GetLevelCount();
    }

    public int GetLevelCount()
    {
        return levelCount;
    }
}
