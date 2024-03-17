using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : State
{
    private EnemyStateData _data = null;

    private Transform _target = null;

    private float _elapsedTime = 0.00f;


    public override void Enter(EnemyStateData _data)
    {
        this._data = _data;

        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _data.Controller.Anim.SetBool("isAttacking",true);
    }
    public override void Tick()
    {
        _elapsedTime += Time.deltaTime;

        if (_data.Controller.AttackDuration > _elapsedTime)
        {
            if (Vector2.Distance(_data.Controller.transform.position, _target.position) > _data.Controller.AttackRange)
            {
                _data.Controller.Anim.SetBool("isAttacking", false);
                _data.Controller.SetState<EnemyMoveState>();
            }
            return;
        }

        _elapsedTime = 0;
        _target.GetComponent<IDamagable>().TakeDamage(_data.Controller.Damage);

    }
    public override void Exit()
    {
        _elapsedTime = 0;
    }


}
