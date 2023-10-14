using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public Transform[] PointsObject;

    private void Awake()
    {
        PointsObject = GetComponentsInChildren<Transform>();
    }
    private void Reset()
    {
        PointsObject = GetComponentsInChildren<Transform>();
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < PointsObject.Length; i++)
        {
            Gizmos.DrawSphere(PointsObject[i].position, 1f);
        }
    }
}
