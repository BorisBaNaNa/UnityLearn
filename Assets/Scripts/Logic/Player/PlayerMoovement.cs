using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoovement : MonoBehaviour
{
    public bool IsJumping => _verticalVelocity.y > 0f;
    public bool IsFalling => _verticalVelocity.y < 0f;
    public bool IsFlaying => !_characterController.isGrounded;
    public Vector3 VerticalVelocity => _verticalVelocity;
    public Vector3 InputVector => _inputVector;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _jumpStrength;
    [SerializeField]
    private Vector2 _mouseSecitivity;
    [SerializeField]
    private SpringArm _cameraBoom;
    [SerializeField]
    private float _drag;

    private CharacterController _characterController;
    private InputService _input;
    private Vector3 _verticalVelocity;
    private Vector3 _inputVector;

    private void Awake()
    {
        InitializeButtons();
        Initialize();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    void Update()
    {
        Vector2 mouseDelta = _input.Player.MouseDelta.ReadValue<Vector2>();
        RotationCamera(mouseDelta);

        if (IsFlaying) ApplyDrag();
        else _inputVector = Vector3.Lerp(_inputVector, _input.Player.Move.ReadValue<Vector3>().normalized, 0.1f);
        MovePlayer(_inputVector * _speed);

        ApplyGravity();
        MovePlayer(_verticalVelocity);
    }

    private void Initialize()
    {
        _cameraBoom.Initialize();
        _characterController = GetComponent<CharacterController>();
        _verticalVelocity = Physics.gravity;
    }

    private void InitializeButtons()
    {
        _input = new();
        _input.Player.Jump.performed += context => Jump();
    }

    private void ApplyDrag()
    {
        if (_inputVector.x != 0)
            _inputVector.x -= Mathf.Min(Math.Abs(_inputVector.x), _drag * Time.deltaTime) * Mathf.Sign(_inputVector.x);
        if (_inputVector.z != 0)
            _inputVector.z -= Mathf.Min(Math.Abs(_inputVector.z), _drag * Time.deltaTime) * Mathf.Sign(_inputVector.z);
    }

    private void ApplyGravity()
    {
        if (!IsJumping && !IsFlaying)
            _verticalVelocity.y = 0f;
        else
            _verticalVelocity += Physics.gravity * 2 * Time.deltaTime;
    }

    private void RotationCamera(Vector2 delta)
    {
        delta *= _mouseSecitivity;

        _cameraBoom.Rotate(delta.y, 0f, 0f);
        transform.Rotate(0f, delta.x, 0f);
    }

    private void Jump()
    {
        if (IsFlaying) return;

        _verticalVelocity.y = Mathf.Sqrt(_jumpStrength * -Physics.gravity.y);
    }

    private void MovePlayer(Vector3 movement)
    {
        if (movement == Vector3.zero) return;

        _characterController.Move(transform.rotation * movement * Time.deltaTime);
    }
}
