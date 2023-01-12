using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneActive : BaseState{
    private DroneFSM _sm;
    public DroneActive(DroneFSM stateMachine) : base("Active", stateMachine)  {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    // Update is called once per frame
    public override void UpdateLogic() {
        //chase behaviour
        _sm.MoveToPlayer();

        if (_sm.CheckHealth()){
            stateMachine.ChangeState(_sm.deadState);
        }

        if (!_sm.triggered){
            stateMachine.ChangeState(_sm.idleState);
        }
    }
}
