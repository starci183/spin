using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class SpinnerSwipeController : Singleton<SpinnerSwipeController>
{
    [Range(0f, 5000f)]
    [SerializeField]
    private float _spinSpeed = 2000f;

    [Range(0f, 5f)]
    [SerializeField]
    private float _friction = 1f;

    [SerializeField]
    private Transform _wheelTransform;

    [SerializeField]
    private Transform _centerTransform;

    [SerializeField]
    private Transform _chooseTransform;

    [SerializeField]
    private MeshFilter _wheelMeshFilter;

    [SerializeField]
    private ResultScriptableObject _resultScriptableObject;

    [SerializeField]
    private Transform _resultModalPrefab;

    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    
    private void Start()
    {
        Debug.Log(_chooseTransform.GetComponent<MeshRenderer>().bounds.center);
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        _centerTransform.transform.localPosition = _wheelMeshFilter.mesh.bounds.center;
        _cinemachineVirtualCamera.transform.position = 
            new Vector3(
                _centerTransform.position.x,
                _centerTransform.position.y,
                -10
                );
    }


    public float CurrentSpinSpeed = 0f;
    public bool IsUp = true;

    private void Update()
    {
        if (CurrentSpinSpeed > 0)
        {
            _wheelTransform.RotateAround(
                _centerTransform.position,
                IsUp ? Vector3.right : Vector3.left, 
                CurrentSpinSpeed * Time.deltaTime
            );

            CurrentSpinSpeed -= _friction;
        }

        if (CurrentSpinSpeed > 0 && CurrentSpinSpeed - _friction <= 0)
        {
            ShowResult();
        }
    }

    public void Spin(Vector2 direction)
    {
        var y = direction.y;
        IsUp = y >= 0;
        CurrentSpinSpeed = Mathf.Abs(y * _spinSpeed);
    }

    private void ShowResult()
    {
        Physics.Raycast(new Ray()
        {
            origin = _chooseTransform.GetComponent<MeshRenderer>().bounds.center,
            direction = -_chooseTransform.up,
        }, out RaycastHit hit);

        _resultScriptableObject.Result = hit.collider.GetComponentInChildren<TMP_Text>().text;
        BootstrapModalController.Instance.CreateModal(_resultModalPrefab);
    }
}
