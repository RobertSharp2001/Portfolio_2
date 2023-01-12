using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinActive : BaseState{
    private TwinFSM _sm;
    public TwinActive(TwinFSM stateMachine) : base("Active", stateMachine){
        _sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic() {
        //chase behaviour

        if (_sm.CheckHealth()) {
            stateMachine.ChangeState(_sm.deadState);
        }

        _sm.AvoidObstacles(2f);
        _sm.MoveToPlayer();


        if (_sm.gunEnabled) {
            _sm.Shoot();
        }

        if (!_sm.triggered) {
            stateMachine.ChangeState(_sm.idleState);
        }
    }
}
