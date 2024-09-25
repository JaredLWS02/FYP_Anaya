using UnityEngine;

public class State_Anaya_Climbing : BaseState
{
    public override string Name => "Climbing";

    Anaya anaya;

    public State_Anaya_Climbing(StateMachine_Anaya sm)
    {
        anaya = sm.anaya;
    }

    protected override void OnEnter()
    {
        Debug.Log($"{anaya.gameObject.name} State: {Name}");

        ToggleAllow(true);
    }

    protected override void OnUpdate(float deltaTime)
    {
        anaya.AllowMoveX = false;
        anaya.AllowMoveY = true;
        anaya.AllowJump = true;
    }

    protected override void OnExit()
    {
        ToggleAllow(false);
    }

    void ToggleAllow(bool toggle)
    {
        anaya.AllowDash = toggle;
        anaya.AllowSwitch = toggle;
    }
}
