using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamInteraction : MonoBehaviour
{
    public delegate void CameraInteraction(string itemName, bool interaction);
    public static CameraInteraction itemCanGet;

    [SerializeField]
    LayerMask layer;

    [SerializeField]
    float distance;

    IInteraction currentObject;

    void Update()
    {
        if(currentObject != null && Input.GetKeyDown(KeyCode.E))
        {
            IInteraction fornow = currentObject;
            currentObject = null;
            fornow.Interaction();
        }
        else
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
            if(Physics.Raycast(transform.position, transform.forward, out hit, distance))
            {
                IInteraction fornowItem = hit.transform.gameObject.GetComponent<IInteraction>();
                if(fornowItem != null)
                {
                    currentObject = fornowItem;
                    itemCanGet(currentObject.GetName(), true);
                }
            }
            else
            {
                currentObject = null;
                itemCanGet("", false);
            }
        }
    }
}
