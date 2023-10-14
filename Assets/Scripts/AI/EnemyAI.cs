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

    private PatrolState PatrolState { get; set; }
    private AttackState AttackState { get; set; }

    private void Awake()
    {
        enemyData = new EnemyData(GetComponent<NavMeshAgent>(),animator);

        PatrolState = new PatrolState(this, enemyData, points.PointsObject);
        AttackState = new AttackState(this, enemyData);

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

    public EnemyData(NavMeshAgent agent, Animator animatorController)
    {
        Agent = agent;
        AnimatorController = animatorController;
    }
}
