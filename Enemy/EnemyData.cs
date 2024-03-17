using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyData")]

public class EnemyData : ScriptableObject
{
    [SerializeField] private int _health;
    [SerializeField] private int _speed;

    [SerializeField] private float _attackDuration;
    [SerializeField] private float _attackRange;
    [SerializeField] private int _damage;
    public void SetVariables(Enemy enemy)
    {
        enemy.Health = _health;
        enemy.Speed = _speed;
        enemy.AttackDuration = _attackDuration;
        enemy.AttackRange = _attackRange;
        enemy.Damage = _damage;
    }
}
