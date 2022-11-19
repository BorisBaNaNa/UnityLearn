using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private Transform _gunPoint;
    [SerializeField]
    private LayerMask _ignoedLayerMask;
    [SerializeField]
    private float _timeReload = 1f;
    [SerializeField]
    private float _randomAngelMax;
    [SerializeField]
    private AnimationCurve _curveDistanceRange;

    private InputService _input;
    private float _timeBeforShoot = 0f;
    private FactoryProjectile _factoryProjectile;
    private bool _gunIsLoaded = true;

    private void Awake()
    {
        _input = new();

        _factoryProjectile = AllServices.Instance.GetService<FactoryProjectile>();
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
        Shoot();
        Cooldown();
    }

    private void Shoot()
    {
        if (!_gunIsLoaded) return;

        if (!_input.Player.Shoot.IsPressed()) return;
        _timeBeforShoot = _timeReload;

        Ray cameraRay = Camera.main.ScreenPointToRay(_input.Player.MousePos.ReadValue<Vector2>());
        if (Physics.Raycast(cameraRay, out RaycastHit hit, 1000, ~_ignoedLayerMask))
            _factoryProjectile.BuildProjectile(_gunPoint.position, GetBulletSpread(hit));
    }

    private void Cooldown()
    {
        _gunIsLoaded = _timeBeforShoot <= 0;
        if (!_gunIsLoaded)
            _timeBeforShoot -= Time.deltaTime;
    }

    private Vector3 GetBulletSpread(RaycastHit hit)
    {
        Vector3 randomHitPoit = hit.point + Random.insideUnitSphere * _curveDistanceRange.Evaluate(hit.distance);
        return (randomHitPoit - _gunPoint.position).normalized;
    }
}
