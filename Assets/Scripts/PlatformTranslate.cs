using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTranslate : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float stopY1;
    [SerializeField] private float stopY2;
    [SerializeField] private float stopZ1;
    [SerializeField] private float stopZ2;

    public bool isActive = true;
    public bool isLooping = false;

    private string moveAxis;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (direction.y != 0)
        {
            moveAxis = "vertical";
        }
        else
        {
            moveAxis = "horizontal";
        }
        if (isActive)
        {
            //transform.Translate(direction * Time.deltaTime * speed);
            transform.position += direction * Time.deltaTime * speed;
            if (moveAxis == "vertical")
            {
                if (transform.position.y > stopY2)
                {
                    direction *= -1;
                    transform.position = new Vector3(transform.position.x, stopY2 - 0.01f, transform.position.z);
                    if (!isLooping) isActive = false;
                }
                else if (transform.position.y < stopY1)
                {
                    direction *= -1;
                    transform.position = new Vector3(transform.position.x, stopY1 + 0.01f, transform.position.z);
                    if (!isLooping) isActive = false;
                }
            }
            else if (moveAxis == "horizontal")
            {
                if (transform.position.z > stopZ2)
                {
                    direction *= -1;
                    transform.position = new Vector3(transform.position.x, transform.position.y, stopZ2 - 0.01f);
                    if (!isLooping) isActive = false;
                }
                else if (transform.position.z < stopZ1)
                {
                    direction *= -1;
                    transform.position = new Vector3(transform.position.x, transform.position.y, stopZ1 + 0.01f);
                    if (!isLooping) isActive = false;
                }
            }
        }
    }
}
