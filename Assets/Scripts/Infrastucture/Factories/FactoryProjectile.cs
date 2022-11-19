using UnityEngine;

public class FactoryProjectile : IService
{
    public GameObject _projectilePrefab;

    public FactoryProjectile(GameObject projectilePrefab)
    {
        _projectilePrefab = projectilePrefab;
    }
    public void BuildProjectile(Vector3 instPos, Vector3 direction)
    {
        var projectile = GameObject.Instantiate(_projectilePrefab, instPos, Quaternion.identity).GetComponent<Projectile>();
        projectile.DirectionTarget = direction;
    }
}
