using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

internal class SpringArm : MonoBehaviour
{
    [SerializeField]
    private GameObject _camera;
    [SerializeField]
    private float _minY;
    [SerializeField]
    private float _maxY;
    [SerializeField]
    private float _armLength;
    [SerializeField]
    private float _xCameraOffset;
    [SerializeField]
    private float _yCameraOffset;
    [SerializeField]
    private float _overlapCheckRadius;
    [SerializeField]
    private LayerMask _ignoredLayerMask;

    private float _xRotation;
    private Vector3 _localPosition;
    private float _startArmLength;

    public void Initialize()
    {
        _xRotation = transform.eulerAngles.x;
        if (_camera == null)
        {
            Debug.Log("Using main camera!");
            _camera = Camera.main.gameObject;
        }
        _camera.transform.parent = transform;
        _localPosition = new Vector3(_xCameraOffset, _yCameraOffset, -_armLength);
        _startArmLength = _armLength;
    }

    private void Update()
    {
        UpdateOffset();
        ZoomOnCollision();
    }

    private void ZoomOnCollision()
    {
        Vector3 dir = _camera.transform.position - transform.position;
        if (Physics.Raycast(transform.position, dir, out RaycastHit hit, _startArmLength, ~_ignoredLayerMask))
            _armLength = hit.distance - 0.1f;
        else
            _armLength = _startArmLength;
    }

    private void UpdateOffset()
    {
        if (_localPosition.x != _xCameraOffset) _localPosition.x = _xCameraOffset;
        if (_localPosition.y != _yCameraOffset) _localPosition.y = _yCameraOffset;
        if (_localPosition.z != -_armLength) _localPosition.z = -_armLength;

        if (_localPosition != _camera.transform.localPosition)
            _camera.transform.localPosition = _localPosition;
    }

    public void Rotate(float xAngle, float yAngle, float zAngle)
    {
        _xRotation -= xAngle;
        _xRotation = Mathf.Clamp(_xRotation, _minY, _maxY);
        transform.rotation = Quaternion.Euler(_xRotation, transform.eulerAngles.y, 0f);

        if (xAngle != 0 && zAngle != 0)
            transform.Rotate(0f, yAngle, zAngle);
    }
}