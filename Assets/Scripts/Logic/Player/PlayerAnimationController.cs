using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerAnimationController : MonoBehaviour
{
    public float IdleTimeActovate;

    [SerializeField]
    private PlayerMoovement _playerMoovement;

    private Animator _animator;
    private float _time = 0;
    private InputService _input;
    private float _deltaX;

    private void Awake()
    {
        _input = new();
        _animator = GetComponent<Animator>();
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
        UpdateInputVals();
        CalculateAdnUpdateTime();
        UpdateVelocity();
    }

    private void UpdateInputVals()
    {
        _animator.SetBool("isFiring", _input.Player.Shoot.IsPressed());
        _deltaX = _input.Player.MouseDelta.ReadValue<Vector2>().x;
        _animator.SetFloat("mouseDeltaX", _deltaX);
    }

    private void UpdateVelocity()
    {
        _animator.SetFloat("Velocity", _playerMoovement.InputVector.magnitude);
        _animator.SetFloat("xVelocity", _playerMoovement.InputVector.x);
        _animator.SetFloat("zVelocity", _playerMoovement.InputVector.z);
        _animator.SetFloat("yVelocity", _playerMoovement.VerticalVelocity.y);
    }

    private void CalculateAdnUpdateTime()
    {
        if (_playerMoovement.InputVector.magnitude == 0f && _deltaX == 0f)
            _time += Time.deltaTime;
        else _time = 0;

        if (_time > IdleTimeActovate)
        {
            _animator.SetTrigger("idleTime");
            _time = -11;
        }
    }
}
