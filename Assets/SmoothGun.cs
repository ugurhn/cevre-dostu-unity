using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothGun : MonoBehaviour
{
    public bool CanSway = true;

    public float Amount = 0.1f;
    public float maxAmount = 0.1f;
    public float SmoothAmount = 2f;

    Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.localPosition;

        CanSway = true;
    }
    private void Update()
    {
        if (CanSway)
        {
            float movementX = -Input.GetAxis("Mouse X") * Amount;
            float movementY = -Input.GetAxis("Mouse Y") * Amount;
            movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
            movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

            Vector3 finalPosition = new Vector3(movementX, movementY, 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * SmoothAmount);
        }
    }
}
