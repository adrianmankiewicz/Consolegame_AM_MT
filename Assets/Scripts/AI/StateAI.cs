using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PatrolState: IEnemyState
{
    private EnemyAI enemyAI;
    private EnemyData enemyData;
    private Transform[] points;
    private float defaultSpeed;
    private float minDistanceTarget;

    private int destPoint;

    public PatrolState(EnemyAI ai, EnemyData enemyData, Transform[] points, float defaultSpeed, float minDistanceTarget)
    {
        enemyAI = ai;
        this.enemyData = enemyData;
        this.points = points;
        this.defaultSpeed = defaultSpeed;
        this.minDistanceTarget = minDistanceTarget;
    }

    public void Enter()
    {
        enemyData.Agent.autoBraking = false;
        GoToNextPoint();
        enemyData.AnimatorController.SetBool("IsWalking", true);
        enemyData.Agent.speed = defaultSpeed;
    }

    public void Exit()
    {
        enemyData.AnimatorController.SetBool("IsWalking", false);
    }

    public void Update()
    {
        if (!enemyData.Agent.pathPending && enemyData.Agent.remainingDistance < 2f)
        {
            GoToNextPoint();
        }
        float distanceTarget = Vector3.Distance(enemyAI.transform.position, enemyData.Target.position);
        if (distanceTarget < minDistanceTarget)
        {
            enemyAI.ChangeState(enemyAI.ChaseState);
        }
    }
    void GoToNextPoint()
    {
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        enemyData.Agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }
}
public class AttackState : IEnemyState
{
    private EnemyAI enemyAI;
    private EnemyData enemyData;
    private float minDistanceTarget;

    public AttackState(EnemyAI ai, EnemyData enemyData, float minDistanceTarget)
    {
        enemyAI = ai;
        this.enemyData = enemyData;
        this.minDistanceTarget = minDistanceTarget;
    }

    public void Enter()
    {
        enemyData.AnimatorController.SetBool("Attack", true);
    }

    public void Exit()
    {
        enemyData.AnimatorController.SetBool("Attack", false);
    }

    public void Update()
    {
        float distance = Vector3.Distance(enemyAI.transform.position, enemyData.Target.position);
        if (distance > minDistanceTarget)
        {
            enemyAI.ChangeState(enemyAI.ChaseState);
        }
    }
}
public class ChaseState : IEnemyState
{
    private EnemyAI enemyAI;
    private EnemyData enemyData;
    private float speedChase;
    private float minDistanceTarget;
    private float minDistanceTargetChase;


    public ChaseState(EnemyAI ai, EnemyData enemyData, float speedChase, float minDistanceTarget, float minDistanceTargetChase)
    {
        enemyAI = ai;
        this.enemyData = enemyData;
        this.speedChase = speedChase;
        this.minDistanceTarget = minDistanceTarget;
        this.minDistanceTargetChase = minDistanceTargetChase;
    }

    public void Enter()
    {
        enemyData.AnimatorController.SetBool("IsRun", true);
        enemyData.Agent.speed = speedChase;
    }

    public void Exit()
    {
        enemyData.AnimatorController.SetBool("IsRun", false);

    }

    public void Update()
    {
        enemyData.Agent.SetDestination(enemyData.Target.position);

        float distanceTarget = Vector3.Distance(enemyAI.transform.position, enemyData.Target.position);

        if (distanceTarget < minDistanceTarget) //attack
        {
            enemyAI.ChangeState(enemyAI.AttackState);
        }
        if (distanceTarget > minDistanceTargetChase)
        {
            enemyAI.ChangeState(enemyAI.PatrolState);
        }

    }
}