using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryIdle : BaseState{
    // Start is called before the first frame update
    private FactoryFSM _sm;

    public FactoryIdle(FactoryFSM stateMachine) : base("Idle", stateMachine) {
        _sm = stateMachine;
    }

    public override void Enter() {
        base.Enter();
    }

    public override void UpdateLogic() {
        //chase behaviour

        if (_sm.CheckHealth()){
            stateMachine.ChangeState(_sm.deadState);
        }
        //Move back to active mode
        stateMachine.ChangeState(_sm.movingState);
    }
}
