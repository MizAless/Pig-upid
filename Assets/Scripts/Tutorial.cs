using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class Tutorial : MonoBehaviour
{

    [SerializeField] private List<VideoClip> clipList;
    [SerializeField] private List<string> RuTextList;
    [SerializeField] private List<string> EnTextList;
    [SerializeField] private InputArrowTranslate inputSystem;
    [SerializeField] private TMP_Text tutorText;

    private int currentClipIndex;
    private VideoPlayer videoPlayer;
    private List<string> textList;

    void Start()
    {

        videoPlayer = GetComponent<VideoPlayer>();

        if (Language.Instance.currentLanguage == "ru")
        {
            textList = RuTextList;
        }
        else
        {
            textList = EnTextList;
        }

        if (clipList.Count > 0)
        {
            inputSystem.canShoot = false;
            currentClipIndex = 0;
            PlayClip();
            tutorText.text = textList[currentClipIndex];
        }
        else
        {
            gameObject.SetActive(false);
            inputSystem.canShoot = true;
        }
    }

    void Update()
    {

    }

    public void NextClip()
    {
        if (clipList.Count > 0)
        {
            currentClipIndex++;
            if (clipList.Count > currentClipIndex)
            {
                PlayClip();
                tutorText.text = textList[currentClipIndex];
            }
            else
            {
                //videoPlayer.clip = null;
                gameObject.SetActive(false);
                inputSystem.canShoot = true;
            }
        }
    }

    public void PlayClip()
    {
        string clipPath = System.IO.Path.Combine(Application.streamingAssetsPath, clipList[currentClipIndex].name);
        videoPlayer.url = clipPath + ".mp4";
        videoPlayer.Play();
    }
}
