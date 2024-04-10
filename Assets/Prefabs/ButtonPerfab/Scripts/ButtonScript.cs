using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material activeMaterial;
    [SerializeField] GameObject _buttonModel;


    public bool isActive { private set; get; } = false;

    private MeshRenderer _buttonRenderer;


    public void SwitchButton()
    {
        if (isActive)
        {
            Deactivate();
        }
        else
        {
            Activate();
        }
    }

    private void Activate()
    {
        isActive = true;
        _buttonRenderer.material = activeMaterial;
    }

    private void Deactivate()
    {
        isActive = false;
        _buttonRenderer.material = defaultMaterial;
    }



    // Start is called before the first frame update
    void Start()
    {
        _buttonRenderer = _buttonModel.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
