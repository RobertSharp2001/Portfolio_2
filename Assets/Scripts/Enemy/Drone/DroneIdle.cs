using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneIdle : BaseState{
    // Start is called before the first frame update

    private DroneFSM _sm;

    public DroneIdle(DroneFSM stateMachine) : base("Idle", stateMachine){
        _sm = stateMachine;
    }

    public override void Enter(){
        base.Enter();
    }

    public override void UpdateLogic() {
        //chase behaviour

        if (_sm.CheckHealth()){
            stateMachine.ChangeState(_sm.deadState);
        }

        if (_sm.triggered) {
            stateMachine.ChangeState(_sm.movingState);
        }
    }
}
