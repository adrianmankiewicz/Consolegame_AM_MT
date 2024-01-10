using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public void Damage()
    {
        GetComponentInParent<EnemyAI>().ThrowDamage();
    }
}
