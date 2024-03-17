using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveState : State
{
    private Transform _target = null;

    private EnemyStateData _data = null;

    public override void Enter(EnemyStateData _data)
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;

        this._data = _data;
        _data.Controller.Anim.SetBool("isMoving", true);
    }
    public override void Tick()
    {
        if (_data.Controller.Anim.GetBool("isDeath") == true)
        {
            _data.Controller.Agent.SetDestination(_data.Controller.transform.position);
        }

        else if (_data.Controller.Anim.GetBool("isDeath") != true)
        {
            _data.Controller.Agent.SetDestination(_target.position);
            if((_data.Controller.transform.position.x - _target.position.x) < 0 && _data.Controller.IsFacingLeft)
            {
                _data.Controller.Flip();
                _data.Controller.IsFacingLeft = false;
            }
            else if((_data.Controller.transform.position.x - _target.position.x) > 0 && !_data.Controller.IsFacingLeft)
            {
                _data.Controller.Flip();
                _data.Controller.IsFacingLeft = true ;
            }
        }
        

        if (Vector2.Distance(_data.Controller.transform.position, _target.position) <= _data.Controller.AttackRange)
        {
            _data.Controller.Agent.SetDestination(_data.Controller.transform.position);
            _data.Controller.Anim.SetBool("isMoving", false);          
            _data.Controller.SetState<EnemyAttackState>();
        }
    }
    public override void Exit()
    {

    }
}
