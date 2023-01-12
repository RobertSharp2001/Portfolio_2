using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretActive : BaseState{

    private TurretFSM _sm;
    public GameObject closest;
    public TurretActive(TurretFSM stateMachine) : base("Active", stateMachine) {
        _sm = stateMachine;
    }

    public override void Enter() {
        base.Enter();
    }

    public override void UpdateLogic(){
        findClosest();
        //Debug.Log("Closest is " + closest.name);
        lockOnClosest();

        if (_sm.CheckHealth()) {
            stateMachine.ChangeState(_sm.deadState);
        }

        if (_sm.gunEnabled){
            _sm.Shoot(closest);
        }

        if (!_sm.triggered) {
            stateMachine.ChangeState(_sm.idleState);
        }
    }

    private void lockOnClosest(){
        //smoothly look at the target

        //Calculate angle to player
        if (closest != null) {

            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(closest.transform.position.y - _sm.transform.position.y, closest.transform.position.x - _sm.transform.position.x)
                * Mathf.Rad2Deg - 90);

            //if the angle is within 10 degrees enable the gun, and there is still an objefct on the list
            float angle = Quaternion.Angle(_sm.transform.rotation, rotation);
            if (angle < 10 && _sm._inrange.Count > 0)
            {
                _sm.gunEnabled = true;
            }
            else
            {
                _sm.gunEnabled = false;
            }

            //Slowly change the value of the player
            _sm.transform.localRotation = Quaternion.Slerp(_sm.transform.rotation, rotation, _sm.turnspeed * Time.deltaTime);
        }
    }




    public void findClosest() {
        float minDist = float.MaxValue;

        foreach (GameObject go in _sm._inrange) {
            if(go != null) {
                float dist = Vector3.Distance(go.transform.position, _sm.transform.position);
                if (dist < minDist)
                {
                    closest = go;
                    minDist = dist;
                }
            }
        }
    }
}