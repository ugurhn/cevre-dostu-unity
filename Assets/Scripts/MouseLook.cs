using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public bool lockCursor;
    public float sensitivity = 100f;
    public Transform body;
    float xRotation = 0f;
    public bool canLook = true;
    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void ChangeCursorType(bool cursor)
    {
        lockCursor = !cursor;
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        } else
        {
            Cursor.lockState = CursorLockMode.None;
        }
        Cursor.visible = cursor;
    }
    
    void LateUpdate()
    {
        if (lockCursor && canLook)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            body.Rotate(Vector3.up * mouseX);
        }
    }
}