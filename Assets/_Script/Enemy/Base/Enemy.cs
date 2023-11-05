using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    [field:SerializeField] public bool hasSuspected {get; set;}
    [field:SerializeField] public Transform playerTransform {get; set;}
    [SerializeField] private float maxTimeDetection;

    [Header("UI")]
    [SerializeField] public TextMeshProUGUI deathText;
    [SerializeField] public Image susTimerBg;
    [SerializeField] public Image susTimer;
    [field:SerializeField] public Image sus;

    private EnemyFov enemyFov;

    private float timeElapsedWhenDetected;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        enemyFov = GetComponent<EnemyFov>();
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

        EnemyDetection();
    }

    private void EnemyDetection()
    {
        if (enemyFov.visibleTargets.Count != 0)
        {
            timeElapsedWhenDetected += Time.deltaTime;
        }
        else
        {
            timeElapsedWhenDetected = 0f;
        }

        if (timeElapsedWhenDetected > 0f)
        {
            Agent.ResetPath();
            Agent.isStopped = true;

           var targetRotation = Quaternion.LookRotation(playerTransform.transform.position - transform.position);
           transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime);

           susTimerBg.gameObject.SetActive(true);
           susTimer.fillAmount = timeElapsedWhenDetected / maxTimeDetection;

           hasDetected = true;
        }
        else
        {
            if (hasDetected)
            {
                sus.gameObject.SetActive(true);

                hasSuspected = true;

                Agent.ResetPath();
                Agent.isStopped = false;

                _enemyStateBase.SwitchStates(_enemyStatesFactory.Wait());

                hasDetected = false;
            }
            susTimerBg.gameObject.SetActive(false);
        }

        if (hasDetected == true && timeElapsedWhenDetected >= maxTimeDetection)
        {
            deathText.gameObject.SetActive(true);
            timeElapsedWhenDetected = maxTimeDetection;
        }
    }

}