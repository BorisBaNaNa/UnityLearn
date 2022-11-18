using UnityEngine;

internal class BoostraperState : IState
{
    IStateSwitcher _stateSwitcher;

    public BoostraperState(IStateSwitcher stateSwitcher)
    {
        _stateSwitcher = stateSwitcher;
    }

    public void Enter()
    {
        Initialize();

        _stateSwitcher.StateSwitch<LoadLevelSate>();
    }

    public void Exit()
    {
    }

    private static void Initialize()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
