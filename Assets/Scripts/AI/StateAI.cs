using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PatrolState: IEnemyState
{
    private EnemyAI enemyAI;
    private EnemyData enemyData;
    private Transform[] points;

    private int destPoint;

    public PatrolState(EnemyAI ai, EnemyData enemyData, Transform[] points)
    {
        enemyAI = ai;
        this.enemyData = enemyData;
        this.points = points;
    }

    public void Enter()
    {
        enemyData.Agent.autoBraking = false;
        GoToNextPoint();
        enemyData.AnimatorController.SetBool("IsWalking", true);
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

    public AttackState(EnemyAI ai, EnemyData enemyData)
    {
        enemyAI = ai;
        this.enemyData = enemyData;
    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }

    public void Update()
    {

    }
}