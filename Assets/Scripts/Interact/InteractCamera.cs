using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class InteractCamera : MonoBehaviour
{
    [SerializeField] float distanceInteract;
    [SerializeField] Image crosshair;

    private void Update()
    {
        InteractBehaviour interact = GetInteractBehaviour();
        if (interact == null)
        {
            crosshair.transform.gameObject.SetActive(false);
        }
        else
        {
            crosshair.transform.gameObject.SetActive(true);
        }
    }

    private void TryInteract()
    {
        InteractBehaviour interact = GetInteractBehaviour();
        if (interact == null)
            return;
        interact.OnInteract();
    }

    private InteractBehaviour GetInteractBehaviour()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, distanceInteract))
        {
            if (hit.transform.TryGetComponent(out InteractBehaviour interact))
            {
                return interact;
            }
        }

        return null;
    }

    public void TriggerPressed(InputAction.CallbackContext context)
    {
        if(context.action.triggered && context.action.ReadValue<float>() != 0 && context.action.phase == InputActionPhase.Performed)
        {
            TryInteract();
        }
    }
}
