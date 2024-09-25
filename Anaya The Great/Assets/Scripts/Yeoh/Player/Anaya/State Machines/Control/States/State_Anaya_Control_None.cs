using UnityEngine;

public class State_Anaya_Control_None : BaseState
{
    public override string Name => "No Controls";

    Anaya anaya;

    public State_Anaya_Control_None(StateMachine_Anaya_Control sm)
    {
        anaya = sm.anaya;
    }

    protected override void OnEnter()
    {
        Debug.Log($"{anaya.gameObject.name} State: {Name}");

        anaya.AllowPlayer = false;
        anaya.AllowAI = false;
    }

    protected override void OnUpdate(float deltaTime)
    {
    }

    protected override void OnExit()
    {
    }
}
