using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class InputArrowTranslate : MonoBehaviour
{
    [SerializeField] public bool canShoot = true;

    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private GameObject _bow;
    [SerializeField] private int _arrowCount = 3;
    [SerializeField] private LineRenderer trajectoryLineRenderer;
    [SerializeField] private int numberOfPoints = 10;
    [SerializeField] private float timeStep = 0.3f;
    [SerializeField] private LineRenderer bowStringUpper;
    [SerializeField] private LineRenderer bowStringLower;
    [SerializeField] private GameObject bowModel;
    [SerializeField] private TMP_Text arrowCountText;


    private GameObject _arrow = null;
    private GameObject arrowStartPoint;
    private Vector3 _arrowStartPos;
    private Vector3 _inputStartPos;
    private Vector3 _offsetVector;
    private bool _isTranslate = false;
    void Start()
    {
        bowStringUpper.gameObject.SetActive(true);
        bowStringLower.gameObject.SetActive(true);
        arrowCountText.text = (_arrowCount + 1).ToString();
    }

    void Update()
    {

        if (_arrow == null && _arrowCount > 0)
        {
            _arrow = Instantiate(_arrowPrefab, _bow.transform.position, Quaternion.identity);
            arrowStartPoint = _arrow.GetComponentsInChildren<Transform>()[1].gameObject;
            _arrowCount--;
            arrowCountText.text = (_arrowCount + 1).ToString();
            _arrow.GetComponent<BoxCollider>().enabled = false;
            ResetBowPosition();
            //GetComponent<LineRenderer>().enabled = false;
            trajectoryLineRenderer.enabled = false;
            StartCoroutine(MakeArrowCollide());
        }

        if (!canShoot) return;


        if (Input.GetMouseButtonDown(0) && _arrow != null)
        {
            _arrowStartPos = _arrow.transform.position;
            _inputStartPos = Input.mousePosition;
            _isTranslate = true;
            trajectoryLineRenderer.enabled = true;

        }

        if (_isTranslate && Input.GetMouseButton(0))
        {
            var currenPos = Input.mousePosition;

            var tempVector = currenPos - _inputStartPos;

            _offsetVector = new Vector3(0, tempVector.y, tempVector.x);

            _arrow.transform.position = _arrowStartPos + _offsetVector / 1000;
            _arrow.transform.LookAt(_arrowStartPos +  -_offsetVector);
            SetBowPosition(-_offsetVector);



            //----------------------------------------------- new

            // –ассчитываем траекторию полета стрелы
            Vector3[] positions = CalculateTrajectory();

            // ќбновл€ем LineRenderer с новыми позици€ми
            trajectoryLineRenderer.positionCount = positions.Length;
            trajectoryLineRenderer.SetPositions(positions);

            //------------------------------------------------|

            //GetComponent<LineRenderer>().SetPosition(0, _arrow.transform.position);
            //GetComponent<LineRenderer>().SetPosition(1, _arrowStartPos - _offsetVector / 500);


        }

        if (_isTranslate && Input.GetMouseButtonUp(0))
        {
            GetComponent<LineRenderer>().enabled = false;
            _arrow.GetComponent<ArrowScript>().Launch(-_offsetVector);
            _isTranslate = false;
            _arrow = null;

        }

        if (Input.GetMouseButtonDown(1))
        {
            _arrow.transform.position = _arrowStartPos;
            _isTranslate = false;
            _offsetVector = Vector3.zero;
            trajectoryLineRenderer.enabled = false;
        }

    }

    private IEnumerator MakeArrowCollide()
    {

        yield return new WaitForSeconds(0.3f);
        _arrow.GetComponent<BoxCollider>().enabled = true;
    }

    private Vector3[] CalculateTrajectory()
    {
        Vector3[] positions = new Vector3[numberOfPoints];

        float arrowMass = _arrow.GetComponent<Rigidbody>().mass;
        // ѕолучаем начальную позицию стрелы
        Vector3 startPosition = _arrow.transform.position;

        // ѕолучаем силу, с которой нат€нута стрела
        Vector3 force = -_offsetVector;

        // –ассчитываем траекторию полета стрелы
        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = i * timeStep;
            Vector3 position = startPosition + (force * t / arrowMass + Physics.gravity * t * t / 2f) / 2500;
            positions[i] = position;
        }

        return positions;
    }

    private void SetBowPosition(Vector3 target)
    {
        bowModel.transform.LookAt(target);
        bowStringLower.SetPosition(0, bowStringLower.transform.position);
        bowStringLower.SetPosition(1, arrowStartPoint.transform.position);
        bowStringUpper.SetPosition(0, bowStringUpper.transform.position);
        bowStringUpper.SetPosition(1, arrowStartPoint.transform.position);
    }

    private void ResetBowPosition()
    {
        bowModel.transform.rotation = Quaternion.identity;
        bowStringLower.SetPosition(0, bowStringLower.transform.position);
        bowStringLower.SetPosition(1, arrowStartPoint.transform.position);
        bowStringUpper.SetPosition(0, bowStringUpper.transform.position);
        bowStringUpper.SetPosition(1, arrowStartPoint.transform.position);
    }


}
