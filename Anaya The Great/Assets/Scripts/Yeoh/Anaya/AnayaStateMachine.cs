using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnayaStateMachine : MonoBehaviour
{
    public Anaya anaya;

    // sub state machines
    public GameObject Pilot_SM;
    public GameObject AI_SM;
    public GameObject Action_SM;

    StateMachine sm;
    BaseState defaultState;

    void Awake()
    {
        sm = new StateMachine();
        
        // STATES ================================================================================

        State_Hub hub = new();
        //AnayaState_Grounded grounded = new(this);
        //AnayaState_MidAir midAir = new(this);

        //temp
        AnayaState_Idle idle = new(this);

        // TRANSITIONS ================================================================================

        hub.AddTransition(idle, (timeInState) =>
        {
            // if(
            //     &&
            // ){
            //     return true;
            // }
            return true;
        });
        
        // ================================================================================

        // idle.AddTransition(grounded, (timeInState) =>
        // {
        //     // if(
        //     //     ||
        //     // ){
        //     //     return true;
        //     // }
        //     return false;
        // });

        // DEFAULT ================================================================================
        
        defaultState = hub;
        sm.SetInitialState(defaultState);
    }

    void Update()
    {
        sm.Tick(Time.deltaTime);
    }

    void OnDisable()
    {
        if(sm!=null)
        {
            sm.currentState.Exit(); // call OnExit on current state
            sm.SetState(defaultState); // Change back to default state
        }
    }
}
