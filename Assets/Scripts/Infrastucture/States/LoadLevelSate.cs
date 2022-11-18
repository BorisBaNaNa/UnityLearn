using UnityEngine;

internal class LoadLevelSate : IState
{
    private IStateSwitcher _stateSwitcher;
    private FactoryPlayer _factoryPlayer;

    public LoadLevelSate(IStateSwitcher stateSwitcher, FactoryPlayer factoryPlayer)
    {
        _stateSwitcher = stateSwitcher;
        _factoryPlayer = factoryPlayer;
    }

    public void Enter()
    {
        CreatePlayer();
    }

    public void Exit()
    {
    }

    private void CreatePlayer()
    {
        Vector3 spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
        _factoryPlayer.BuildPlayer(spawnPoint);
    }
}
