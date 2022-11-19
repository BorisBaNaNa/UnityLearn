using UnityEngine;

public class FactoryProjectile : IService
{
    public Projectile _projectilePrefab;

    public FactoryProjectile(Projectile projectilePrefab)
    {
        _projectilePrefab = projectilePrefab;
    }
    public Projectile BuildProjectile(Vector3 instPos, Vector3 direction)
    {
        Projectile projectile = Object.Instantiate(_projectilePrefab, instPos, Quaternion.identity);
        projectile.DirectionTarget = direction;
        return projectile;
    }
}
