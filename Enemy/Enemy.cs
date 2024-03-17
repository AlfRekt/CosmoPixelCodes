using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private EnemyData _data;

    [SerializeField] private State _currentState;

    private Animator _anim;

    private float _speed;
    private int _health;

    private float _attackDuration;
    private float _attackRange;
    private int _damage;

    private SpriteRenderer _spriteRenderer;

    private EnemyStateData _stateData = null;
    private NavMeshAgent _agent;

    private bool _isFacingLeft;

    #region Encapsulations
    public float Speed { get => _speed; set => _speed = value; }

    public int Health { get => _health; set => _health = value; }
    public SpriteRenderer SpriteRenderer { get => _spriteRenderer; }
    public Animator Anim { get => _anim; }
    public NavMeshAgent Agent { get => _agent; set => Agent = value; }
    public float AttackDuration { get => _attackDuration; set => _attackDuration = value; }
    public float AttackRange { get => _attackRange; set => _attackRange = value; }
    public int Damage { get => _damage; set => _damage = value; }
    public bool IsFacingLeft { get => _isFacingLeft; set => _isFacingLeft = value; }

    #endregion


    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        _stateData = new(this);

        _data.SetVariables(this);

        SetState<EnemyMoveState>();
    }
    void Update()
    {
        _currentState?.Tick();
    }

    public void SetState<T>() where T : State
    {
        _currentState?.Exit();

        _currentState = ScriptableObject.CreateInstance<T>();

        _currentState?.Enter(_stateData);
    }

    public void TakeDamage(int _amount)
    {
        Health -= _amount;

        if (Health <= 0)
            StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        _anim.SetBool("isDeath", true);
        gameManager.KillCount++;
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
    }
}

public class EnemyStateData
{
    private Enemy _controller;
    public Enemy Controller => _controller;

    public EnemyStateData(Enemy _controller)
    {
        this._controller = _controller;
    }
}
