using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigTranslate : MonoBehaviour
{

    [SerializeField] private float stopPosL;
    [SerializeField] private float stopPosR;
    [SerializeField] private Transform stopPointL;
    [SerializeField] private Transform stopPointR;
    [SerializeField] private float speed;
    [SerializeField] private float rotateDuration;
    [SerializeField] private bool LocalCoordinates;
    [SerializeField] private bool useSceneTransformStopPoits;

    private bool isWalking = true;
    private bool isRotating = false;
    private float startRotateTime;
    private float startRotateAngle;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (isWalking)
        {
            transform.position += transform.forward * speed * Time.deltaTime;

            if (LocalCoordinates)
            {

                Vector3 localPosition = transform.parent.InverseTransformPoint(transform.position);


                if (localPosition.z > stopPosR || localPosition.z < stopPosL)
                {
                    BeginRotating();
                }
            }
            else if (useSceneTransformStopPoits)
            {
                if (transform.position.z > stopPointR.position.z || transform.position.z < stopPointL.position.z)
                {
                    BeginRotating();
                }
            }
            else
            {
                if (transform.position.z > stopPosR || transform.position.z < stopPosL)
                {
                    BeginRotating();
                }
            }


        }

        if (isRotating)
        {
            float rotateProgress = (Time.time - startRotateTime) / rotateDuration;
            float targetRotation = 180f;

            if (rotateProgress >= 1f)
            {
                // Завершаем вращение
                isRotating = false;
                isWalking = true;
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, startRotateAngle + targetRotation, transform.rotation.eulerAngles.z);
                transform.position += transform.forward * 0.05f;
            }
            else
            {
                // Вращаем объект плавно
                float currentRotation = Mathf.Lerp(180f, 180f, rotateProgress);
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + currentRotation * Time.deltaTime, transform.rotation.eulerAngles.z);
            }
        }
    }

    public void Stop()
    {
        isWalking = false;
        isRotating = false;
    }

    private void BeginRotating()
    {
        isWalking = false;
        isRotating = true;
        startRotateTime = Time.time;
        startRotateAngle = transform.rotation.eulerAngles.y;
    }

}
