using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [Header("Agent")]
    [HideInInspector] public NavMeshAgent Agent;
    [SerializeField] private Transform pathHolder;
    public float PatrolSpeed = 5;

    [Space(5)]
    [Header("States")]
    private EnemyStatesFactory _enemyStatesFactory;
    private EnemyStatesBase _enemyStateBase;
    public EnemyStatesBase CurrentState { get { return _enemyStateBase; } set { _enemyStateBase = value; } }

    [Space(5)]
    [Header("Patroling")]
    public Coroutine LastWaitRoutine;
    public float MinWaitTime = 1f, MaxWaitTime = 6f;
    [HideInInspector] public Transform[] WayPoints;
    [HideInInspector] public Transform CurrentWayPoint;
    public float ExtraPetrolStopDistance = 1.5f;

    [field:Space(5)]
    [field:Header("Detect")]
    [field:SerializeField] public bool hasDetected {get; set;}

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        InitializeStates();
    }

    private void InitializeStates()
    {
        // Setting Waypoints
        WayPoints = EnemyManager.Instance.WayPoints;
        // Creating new instance of the states
        _enemyStatesFactory = new EnemyStatesFactory(this);
        _enemyStateBase = _enemyStatesFactory.Wait();
        _enemyStateBase.EnterState();
    }

    private void Update()
    {
        _enemyStateBase.UpdateState();
    }
}