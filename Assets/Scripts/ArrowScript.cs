using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool isFlying = false;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Fly();
    }

    public void Launch(Vector3 forceVector)
    {
        isFlying = true;
        _rigidbody.useGravity = true;
        _rigidbody.AddForce(forceVector);
    }

    private void Fly()
    {
        if (isFlying)
        {
            Debug.DrawLine(transform.position, transform.position + _rigidbody.velocity);
            transform.LookAt(transform.position + _rigidbody.velocity);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (isFlying)
        {

            //if (collision.gameObject.CompareTag("Arrow")) return;

            isFlying = false;
            if (collision.gameObject.transform.parent != null)
            {
                transform.parent = collision.gameObject.transform.parent;
            }
            else
            {
                transform.parent = collision.gameObject.transform;
            }
            if (collision.gameObject.tag == "Pig")
            {
                collision.gameObject.GetComponent<PigScript>().PlayPigSound();
                transform.parent = collision.gameObject.transform;
                var observedPig = collision.gameObject.GetComponent<FieldOfView>().observedPig;
                if (observedPig != null)
                {
                    var lovePig = collision.gameObject.GetComponent<LovePigScript>();
                    lovePig.Love(observedPig.GetComponent<LovePigScript>());
                    lovePig.MutualLove();

                }
            }
            if (collision.gameObject.tag == "Button")
            {
                collision.gameObject.GetComponentInParent<ButtonScript>().SwitchButton();
            }

            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<BoxCollider>());
        }
    }
}
