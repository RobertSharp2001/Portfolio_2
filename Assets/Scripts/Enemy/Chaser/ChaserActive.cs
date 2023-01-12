using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserActive : BaseState{
    // Start is called before the first frame update
    private ChaserFSM _sm;
    public ChaserActive(ChaserFSM stateMachine) : base("Idle", stateMachine) {
        _sm = stateMachine;
    }

    public override void Enter(){
        base.Enter();
    }

    // Update is called once per frame
    public override void UpdateLogic(){
        //chase behaviour
        _sm.AvoidObstacles(2f);
        _sm.MoveToPlayer();

        if (_sm.CheckHealth()) {
            stateMachine.ChangeState(_sm.deadState);
        }

        if (_sm.gunEnabled) {
            _sm.Shoot();
        }

        if (!_sm.triggered){
            stateMachine.ChangeState(_sm.idleState);
        }
    }
}
