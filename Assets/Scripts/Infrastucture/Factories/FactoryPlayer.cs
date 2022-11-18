using UnityEngine;

public class FactoryPlayer : IService
{
    GameObject _playerPrefab;

    public FactoryPlayer(GameObject playerPrefab)
    {
        _playerPrefab = playerPrefab;
    }

    internal void BuildPlayer(Vector3 at)
        => GameObject.Instantiate(_playerPrefab, at, Quaternion.identity);
}
