using UnityEngine;

public class State_Anaya_Control_AI_Idle : BaseState
{
    public override string Name => "AI Idle";

    Anaya anaya;

    public State_Anaya_Control_AI_Idle(StateMachine_Anaya_Control sm)
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
