using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InternationalText : MonoBehaviour
{

    [SerializeField] string _ru;
    [SerializeField] string _en;

    void Start()
    {
        if (Language.Instance.currentLanguage == "ru")
            //if (true)
        {
            GetComponent<TMP_Text>().text = _ru;
        }
        else
        {
            GetComponent<TMP_Text>().text = _en;
        }
    }

}
