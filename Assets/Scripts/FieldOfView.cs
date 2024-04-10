using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject observedPig = null;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePig = false;

    [SerializeField] private LineRenderer FOVLine1;
    [SerializeField] private LineRenderer FOVLine2;
    [SerializeField] private LineRenderer VeiwLine;
    [SerializeField] private GameObject Eyes;


    private void Start()
    {
        //playerRef = GameObject.FindGameObjectWithTag("Player");
        FOVLine1.gameObject.SetActive(false);
        FOVLine2.gameObject.SetActive(false);
        StartCoroutine(FOVRoutine());
    }

    private void Update()
    {
        //DrowFOV();

        //Debug.DrawLine(transform.position, transform.position + transform.forward);

        if (canSeePig)
        {
            //Debug.DrawLine(Eyes.transform.position, observedPig.transform.position);
            DrowViewLine();
        }
    }


    private IEnumerator FOVRoutine()
    {
        float delay = 0.05f;

        while (true)
        {
            yield return new WaitForSeconds(delay);
            FeildOfViewCheck();
            
        }
    }

    private void FeildOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(Eyes.transform.position, radius, targetMask);

        if (rangeChecks.Length > 0)
        {
            List<GameObject> viewedObj = new List<GameObject>();

            foreach (var obj in rangeChecks)
            {
                Transform target = obj.transform;
                Vector3 directionToTarget = (target.position - Eyes.transform.position).normalized;


                if (Vector3.Angle(Eyes.transform.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(Eyes.transform.position, target.position);

                    if (!Physics.Raycast(Eyes.transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        viewedObj.Add(obj.gameObject);
                    }
                }
            }

            if (viewedObj.Count > 0)
            {
                observedPig = viewedObj.OrderBy(obj => Vector3.Distance(obj.transform.position, Eyes.transform.position)).FirstOrDefault(obj => obj != gameObject);
                if (observedPig != null)
                {
                    canSeePig = true;
                    VeiwLine.gameObject.SetActive(true);

                }
                else
                {
                    canSeePig = false;
                    observedPig = null;
                    VeiwLine.gameObject.SetActive(false);
                }
            }
            else
            {
                canSeePig = false;
                observedPig = null;
                VeiwLine.gameObject.SetActive(false);
            }
        }
        else if (canSeePig)
        {
            canSeePig = false;
            observedPig = null;
            VeiwLine.gameObject.SetActive(false);
        }

    }

    private void DrowFOV()
    {
        FOVLine1.SetPosition(1, Eyes.transform.position + DirectionFromAngle(Eyes.transform.eulerAngles.y, -angle / 2) * radius);
        FOVLine1.SetPosition(0, Eyes.transform.position);
        FOVLine2.SetPosition(1, Eyes.transform.position + DirectionFromAngle(Eyes.transform.eulerAngles.y, angle / 2) * radius);
        FOVLine2.SetPosition(0, Eyes.transform.position);
    }

    private void DrowViewLine()
    {
        Debug.DrawLine(Eyes.transform.position, observedPig.transform.position);
        VeiwLine.SetPosition(1, observedPig.transform.position);
        VeiwLine.SetPosition(0, Eyes.transform.position);
    }


    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(0, Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    //private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    //{
    //    angleInDegrees += eulerY;

    //    return new Vector3(0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
    //}


}
