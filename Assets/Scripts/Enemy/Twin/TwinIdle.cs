using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinIdle : BaseState{

    private TwinFSM _sm;
    public TwinIdle(TwinFSM stateMachine) : base("Idle", stateMachine){
        _sm = stateMachine;
    }

    public override void Enter(){
        base.Enter();
    }

    public override void UpdateLogic(){
        //
        //Debug.Log("Running");


        if (_sm.CheckHealth()){
            stateMachine.ChangeState(_sm.deadState);
        }

        _sm.AvoidObstacles(2f);
        _sm.TurnToMoveDirection();

        if (_sm.triggered){
            stateMachine.ChangeState(_sm.movingState);
        }
    }

}
