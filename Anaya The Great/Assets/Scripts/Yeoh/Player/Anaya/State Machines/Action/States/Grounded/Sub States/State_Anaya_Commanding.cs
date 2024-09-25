using UnityEngine;

public class State_Anaya_Commanding : BaseState
{
    public override string Name => "Commanding";

    Anaya anaya;

    public State_Anaya_Commanding(StateMachine_Anaya sm)
    {
        anaya = sm.anaya;
    }

    protected override void OnEnter()
    {
        Debug.Log($"{anaya.gameObject.name} State: {Name}");
    }

    protected override void OnUpdate(float deltaTime)
    {
    }

    protected override void OnExit()
    {
    }
}
