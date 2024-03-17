using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    public abstract void Enter(EnemyStateData _data);
    public abstract void Tick();
    public abstract void Exit();
}
