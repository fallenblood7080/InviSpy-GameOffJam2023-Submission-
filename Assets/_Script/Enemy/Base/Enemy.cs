using Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 currentTarget;
    private int confuseMeter;
    bool isSupected;
    private float susMeter;
    private EnemyFov fov;
    private Transform player;
    private bool isSaying = false;

    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float waitDuration;
    [SerializeField] private List<Transform> patrolPoints;
    [SerializeField] private GameObject susMark;
    [SerializeField] private GameObject susMeterObject;
    [SerializeField] private Image susMeterFill;
    [SerializeField] private GameObject gameMusic;
    [SerializeField] private AudioSource[] enemyDialogues;

    [field: SerializeField] public EnemyState State { get; private set; }


    private static readonly int SPEED_TAG = Animator.StringToHash("Speed");

    private void Start() => InitializeEnemy();

    private void InitializeEnemy()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = walkSpeed;
        currentTarget = GetRandomPoint();
        agent.SetDestination(currentTarget);
        fov = GetComponent<EnemyFov>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    private void Update()
    {
        susMark.SetActive(isSupected);

        enemyAnimator.SetFloat(SPEED_TAG, agent.speed);
        if (isSupected == false)
        {
            if (agent.remainingDistance <= 0.1f)
            {
                currentTarget = GetRandomPoint();
                agent.SetDestination(currentTarget);
                agent.speed = 0;
                Invoke(nameof(Waiting), waitDuration);
            } 
        }

        if (fov.visibleTargets.Contains(player) && !player.GetComponent<ShapeShiftPower>().IsCurrentlySmall)
        {
            susMark.SetActive(false);
            susMeterObject.SetActive(true);
            if (!gameMusic.GetComponent<PlayerIsSus_EnemyMusic>().isplayerIsSusPlaying)
            {
                gameMusic.GetComponent<PlayerIsSus_EnemyMusic>().PlayMusic();
            }
            else{}
            if (!isSaying)
            {
                enemyDialogues[UnityEngine.Random.Range(0,2)].Play();
                isSaying = true;
                StartCoroutine(stopSaying());
            }
            susMeter += Time.deltaTime / 2;
        }
        else
        {
            agent.stoppingDistance = 0;
            susMeter -= Time.deltaTime;
        }
        susMeterFill.fillAmount = susMeter;
        if(susMeter <= 0)
        {
            susMeterObject.SetActive(false);
            susMeter = 0;
        }
        if(susMeter >= 1)
        {
            agent.speed = 0;
            GameOverManager.GetInstance.OnGameOver?.Invoke(false);
        }
    }

    private IEnumerator stopSaying()
    {
        yield return new WaitForSeconds(2f);
        isSaying = false;
    }

    private void Waiting()
    {
        agent.speed = walkSpeed;

    }

    private Vector3 GetRandomPoint()
    {
        return patrolPoints.GetRandomItems(1)[0].position;
    }
    public void OnHearNoise(Vector3 source , int noise)
    {
        isSupected = true;
        CancelInvoke();
        confuseMeter += noise;
        Invoke(nameof(Waiting), waitDuration);
        agent.SetDestination(source);
        if(confuseMeter > 50)
        {
            CancelInvoke();
            agent.speed = confuseMeter > 80 ? runSpeed : walkSpeed;
        }
        else
        {
            agent.speed = 0;
        }
    }
    public void OnEndHearingNoise(Vector3 source, int noise)
    {
        confuseMeter = 0;
        isSupected = false;
    }

    #region Old Enemy
/*    [Header("Agent")]
    [HideInInspector] public NavMeshAgent Agent;
    [SerializeField] private Transform pathHolder;
    [SerializeField] private AnimationState animationState = AnimationState.Idle;

    [Space(5)]
    [Header("States")]
    private EnemyStatesFactory _enemyStatesFactory;
    private EnemyStatesBase _enemyStateBase;
    public EnemyStatesBase CurrentState { get { return _enemyStateBase; } set { _enemyStateBase = value; } }

    [Space(5)]
    [Header("Patroling")]
    public Coroutine LastWaitRoutine;
    public float PatrolSpeed = 3;
    public float MinWaitTime = 1f, MaxWaitTime = 6f;
    [HideInInspector] public Transform[] WayPoints;
    [HideInInspector] public Transform CurrentWayPoint;
    public float ExtraPetrolStopDistance = 1.5f;

    [field: Space(5)]
    [field: Header("Detect")]
    [field: SerializeField] public bool hasDetected { get; private set; }
    [field: SerializeField] public bool HasSuspectedAfterDetection { get; set; }
    [field: SerializeField] public float maxTimeDetection { get; private set; }

    [field: Space(5)]
    [field: Header("Inspecting")]
    [field: SerializeField] public float ChaseSpeed { get; private set; }
    [field: SerializeField] public bool isChasing { get; set; }

    [Header("UI")]
    [SerializeField] public TextMeshProUGUI deathText;
    [SerializeField] public Image susTimerBg;
    [SerializeField] public Image susTimer;
    [field: SerializeField] public Image sus;

    private EnemyFov enemyFov;
    public Animator anim;

    [HideInInspector] public float timeElapsedWhenDetected;


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
        EnemyChasing();
        EnemyAnimation();
    }

    private void EnemyDetection()
    {
        if (enemyFov.visibleTargets.Count != 0 && !EnemyManager.Instance.playerPower.IsCurrentlySmall)
        {
            timeElapsedWhenDetected += Time.deltaTime;
        }
        else
        {
            timeElapsedWhenDetected = 0f;
        }

        if (timeElapsedWhenDetected > 0f)
        {
            hasDetected = true;
        }
        else
        {
            hasDetected = false;
        }

        if (hasDetected && timeElapsedWhenDetected >= maxTimeDetection)
        {
            deathText.gameObject.SetActive(true);
            timeElapsedWhenDetected = maxTimeDetection;
        }
    }

    public void HasDetected()
    {
        if (hasDetected)
        {
            _enemyStateBase.SwitchStates(_enemyStatesFactory.Detect());
        }
    }

    private void EnemyChasing()
    {
        if (EnemyManager.Instance.isCreatingNoise)
        {
            "Noisey".Log();
            if (hasDetected)
            {
                return;
            }
            else
            {
                isChasing = true;
            }
        }
    }

    public void HasChased()
    {
        if (isChasing)
        {
            _enemyStateBase.SwitchStates(_enemyStatesFactory.Chase());
        }
    }

    private void EnemyAnimation()
    {
        switch (animationState)
        {
            case AnimationState.Idle:
                anim.SetFloat(SPEED_TAG, 0f);
                return;

            case AnimationState.Walk:
                anim.SetFloat(SPEED_TAG, 3f);
                return;

            case AnimationState.Run:
                anim.SetFloat(SPEED_TAG, 6f);
                return;
        }
    }

    public void ChangeeAnimationState(AnimationState state)
    {
        animationState = state;
    }



*/

    #endregion 
}

public enum EnemyState
{
    Idle,
    Walk,
    Run
}