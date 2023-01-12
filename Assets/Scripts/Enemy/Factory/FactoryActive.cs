using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryActive : BaseState{
    private FactoryFSM _sm;

    public FactoryActive(FactoryFSM stateMachine) : base("Active", stateMachine){
        _sm = stateMachine;
    }

    public override void Enter()  {
        base.Enter();
    }

    public override void UpdateLogic(){
        //chase behaviour

        if (_sm.CheckHealth()){
            stateMachine.ChangeState(_sm.deadState);
        }

        _sm.doSpawns();
    }
}
