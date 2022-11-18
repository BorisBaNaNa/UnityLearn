using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 DirectionTarget { get; set; }

    [SerializeField]
    private float Speed;
    [SerializeField]
    private GameObject _particePrefab;

    private bool _isCollade = false;

    private void Update()
    {
        MoveToTarget();
    }

    private void OnCollisionEnter(Collision collision)
    {
        CollapseBullet();
        ContactPoint contact = collision.contacts[0];
        Instantiate(_particePrefab, contact.point, Quaternion.LookRotation(contact.normal));
    }

    private void CollapseBullet()
    {
        _isCollade = true;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject, 1);

    }

    private void MoveToTarget()
    {
        if (_isCollade) return;

        transform.position += DirectionTarget * Speed * Time.deltaTime;
    }
}
