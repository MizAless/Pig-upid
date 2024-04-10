using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PigScript : MonoBehaviour
{
    [SerializeField] private List<AudioClip> pigSounds;

    private AudioSource _pigSource;

    void Start()
    {
        _pigSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }

    public void PlayPigSound()
    {
        _pigSource.clip = pigSounds[Random.Range(0, pigSounds.Count)];
        _pigSource.Play();
    }


}
