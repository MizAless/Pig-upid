using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LovePigScript : MonoBehaviour
{
    [SerializeField] public GameObject loveImg;
    [SerializeField] public GameObject checkLoveImg;
    [SerializeField] private LineRenderer LoveLine;

    public LovePigScript otherLovePig = null;
    public bool inLove;
    public bool needLove;
    public LovePigScript needLovePig = null;


    private PigScript _pigScript;
    private GameObject _camera;


    // Start is called before the first frame update
    void Start()
    {
        if (needLove)
        {
            checkLoveImg.SetActive(true);
        }
        _pigScript = GetComponent<PigScript>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        loveImg.transform.LookAt(_camera.transform.position);
        checkLoveImg.transform.LookAt(_camera.transform.position);

        if (LoveLine.enabled)
        {
            SetLoveLinePosition();
        }

    }

    private void SetLoveLinePosition()
    {
        LoveLine.SetPosition(0, transform.position);
        LoveLine.SetPosition(1, otherLovePig.transform.position);
    }

    public void Love(LovePigScript pig)
    {
        loveImg.SetActive(true);
        checkLoveImg.SetActive(false);
        inLove = true;
        otherLovePig = pig;
        LoveLine.enabled = true;
    }

    public void Dislove()
    {
        loveImg.SetActive(false);
        inLove = false;
        otherLovePig = null;
        LoveLine.enabled = false;

    }

    public void MutualLove()
    {
        if (!inLove)
            return;

        if (inLove && otherLovePig.inLove && otherLovePig.otherLovePig == this)
        {
            var otherPigImg = otherLovePig.loveImg;
            Vector3 newImgPos = (otherPigImg.transform.position + transform.position) / 2;
            loveImg.transform.parent = null;
            otherPigImg.transform.parent = null;
            StartCoroutine(TranslateLoveImgs(loveImg.transform, otherPigImg.transform, newImgPos));

        }
    }

    public IEnumerator TranslateLoveImgs(Transform img1, Transform img2, Vector3 targetPos)
    {
        float duration = 0.5f;
        Vector3 startPosition1 = img1.position;
        Vector3 startPosition2 = img2.position;


        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            img1.position = Vector3.Lerp(startPosition1, targetPos, t);
            img2.position = Vector3.Lerp(startPosition2, targetPos, t);

            yield return null;
        }
    }





}
