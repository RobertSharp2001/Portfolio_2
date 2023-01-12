using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BaseState {
    protected FiniteStateMachine _sm;

    public DeadState(FiniteStateMachine stateMachine) : base("Dead", stateMachine){
        _sm = stateMachine;
    }


    public override void Enter() {
        base.Enter();
    }
    public override void UpdateLogic() {
        if(_sm.health.currentHealth <= 0) {
            _sm.currentSprite.GetComponent<SpriteRenderer>().sprite = _sm.health.deadSprite;

            //Add ammo and health drops here
            _sm.destory();
        } else{
            if (_sm.FindState("Idle") != null) { stateMachine.ChangeState(_sm.FindState("Idle")); }
            
        }

    }


    public override void UpdatePhysics() { }
    public override void Exit() { }


}
