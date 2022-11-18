using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private Transform _gunPoint;
    [SerializeField]
    private LayerMask _ignoedLayerMask;
    [SerializeField]
    private float _timeReload = 1f;
    [SerializeField]
    private float _bulletSpread;

    private InputService _input;
    private float _timeBeforShoot = 0f;
    private FactoryProjectile _factoryProjectile;

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
    }

    private void Shoot()
    {
        if (_timeBeforShoot > 0)
        {
            _timeBeforShoot -= Time.deltaTime;
            return;
        }

        if (!_input.Player.Shoot.IsPressed()) return;
        _timeBeforShoot = _timeReload;

        Ray cameraRay = Camera.main.ScreenPointToRay(_input.Player.MousePos.ReadValue<Vector2>());
        if (Physics.Raycast(cameraRay, out RaycastHit hit, 1000, ~_ignoedLayerMask))
            _factoryProjectile.BuildProjectile(_gunPoint.position, GetBulletSpread(hit.point - _gunPoint.position));
    }

    private Vector3 GetBulletSpread(Vector3 direction)
    {
        float GetRandomSpread() => Random.Range(-_bulletSpread, _bulletSpread);
        direction += new Vector3(GetRandomSpread(), GetRandomSpread(), GetRandomSpread());

        return direction.normalized;
    }
}
