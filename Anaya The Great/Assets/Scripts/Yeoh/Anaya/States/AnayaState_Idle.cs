using UnityEngine;

public class AnayaState_Idle : BaseState
{
    public override string Name => "Idle";

    Anaya anaya;

    public AnayaState_Idle(AnayaStateMachine sm)
    {
        anaya = sm.anaya;
    }

    protected override void OnEnter()
    {
        Debug.Log($"{anaya.gameObject.name} State: {Name}");
    }

    protected override void OnUpdate(float deltaTime)
    {
        // steve.move.target = steve.wander.wanderTr;
        // steve.move.evade=false;
        // steve.move.arrival=true;
        // steve.move.departure=true;
        // steve.combat.range=steve.huggyRange;
    }

    protected override void OnExit()
    {
    }
}
