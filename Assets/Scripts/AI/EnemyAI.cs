using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private IEnemyState currentState;
    private EnemyData enemyData;

    [SerializeField] Points points;
    [SerializeField] Animator animator;
    [SerializeField] Transform target;
    [SerializeField] float minDistanceTargetChase;
    [SerializeField] float minDistanceTargetAttack;

    public PatrolState PatrolState { get; private set; }
    public AttackState AttackState { get; private set; }
    public ChaseState ChaseState { get; private set; }

    private void Awake()
    {
        enemyData = new EnemyData(GetComponent<NavMeshAgent>(),animator,target);

        PatrolState = new PatrolState(this, enemyData, points.PointsObject, 6, minDistanceTargetChase);
        AttackState = new AttackState(this, enemyData, minDistanceTargetAttack);
        ChaseState = new ChaseState(this, enemyData, 8, minDistanceTargetAttack, minDistanceTargetChase);

        ChangeState(PatrolState);
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }
}

public class EnemyData
{
    public NavMeshAgent Agent;
    public Animator AnimatorController;
    public Transform Target;

    public EnemyData(NavMeshAgent agent, Animator animatorController, Transform target)
    {
        Agent = agent;
        AnimatorController = animatorController;
        Target = target;
    }
}
