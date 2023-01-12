using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIdle : BaseState{
    // Start is called before the first frame update
    private TurretFSM _sm;

    public TurretIdle(TurretFSM stateMachine) : base("Idle", stateMachine) {
        _sm = stateMachine;
    }

    public override void Enter() {
        base.Enter();
    }

    public override void UpdateLogic(){
        //
        if (_sm.CheckHealth()) {
            stateMachine.ChangeState(_sm.deadState);
        }

        if (_sm.triggered){
            stateMachine.ChangeState(_sm.movingState);
        }
    }
}
