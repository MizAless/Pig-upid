using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowAdv();


    public int currentLevel = 1; // Текущий уровень игры
    private const string MusicTimeKey = "MusicTime";
    private const string PrevMusicName = "PreviousMusicName";
    private AudioSource musicSource;

    private void Start()
    {
        


        musicSource = GetComponent<AudioSource>();

        

        if (PlayerPrefs.GetString(PrevMusicName) == musicSource.name)
        {
            SetMusicTime();
        }
        else
        {
            PlayerPrefs.SetFloat(MusicTimeKey, 0);
        }

        ShowAdv();
        musicSource.Pause();

    }

    public void PlayMusic()
    {
        musicSource.Play();
        SetMusicTime();

    }




    //public IEnumerator LoadNextLevelWithDelay()
    //{
    //    if (PlayerPrefs.GetString(PrevMusicName) == musicSource.name)
    //    {
    //        PlayerPrefs.SetFloat(MusicTimeKey, GetMusicTime());
    //    }
    //    else
    //    {
    //        PlayerPrefs.SetFloat(MusicTimeKey, 0);
    //    }
    //    PlayerPrefs.SetInt("ComplitedLevelsCount", currentLevel);
    //    yield return new WaitForSeconds(1);
    //    currentLevel++;
    //    if (currentLevel <= SceneManager.sceneCountInBuildSettings)
    //    {
    //        SceneManager.LoadScene(currentLevel);
    //    }
    //    else
    //    {
    //        // Если все уровни завершены, можно выполнить другие действия, например, отобразить экран завершения игры или вернуться на первый уровень
    //    }
    //}

    public void LoadNextLevel()
    {
        PlayerPrefs.SetString(PrevMusicName, musicSource.name);
        PlayerPrefs.SetInt("ComplitedLevelsCount", currentLevel);
        PlayerPrefs.SetFloat(MusicTimeKey, GetMusicTime());
        currentLevel++;

        int sceneCount = LevelCounter.Instance.GetLevelCount();

        if (currentLevel <= sceneCount)
        {
            //ShowAdv();
            SceneManager.LoadScene(currentLevel);
        }
        else
        {
            SceneManager.LoadScene(sceneCount + 1);
        }
    }


    public float GetMusicTime()
    {
        if (musicSource != null)
        {
            return musicSource.time;
        }
        else
        {
            Debug.LogWarning("Music audio source is not assigned!");
            return 0f;
        }
    }

    public void SetMusicTime()
    {
        musicSource.time = PlayerPrefs.GetFloat(MusicTimeKey);
    }

    public void RestarLevel()
    {
        PlayerPrefs.SetFloat(MusicTimeKey, GetMusicTime());
        SceneManager.LoadScene(currentLevel);
    }

    public void LoadMenu()
    {
        PlayerPrefs.SetFloat(MusicTimeKey, 0);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}