using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TranslateFromButton : MonoBehaviour
{

    [SerializeField] ButtonScript _buttonScript;
    [SerializeField] PlatformTranslate _platformTranslate;

    public bool isDisablelable = false;

    private bool first = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if ( _buttonScript.isActive) 
        {
            if (isDisablelable && !first)
            {
                return;
            }

            _platformTranslate.isActive = !_platformTranslate.isActive;
            first = false;
        }
    }
}
