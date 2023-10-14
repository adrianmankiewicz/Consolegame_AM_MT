using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractBehaviour
{
    public override void OnInteract()
    {
        Debug.Log("Open");
    }
}
