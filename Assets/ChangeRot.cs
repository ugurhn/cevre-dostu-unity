using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRot : MonoBehaviour
{
    public bool isElectric;
    public float rotationSpeed;
    void Update()
    {
        isElectric = GetComponentInParent<MusilajCleaner>().electric;
        if (isElectric)
        {
            transform.Rotate(0,6 * rotationSpeed * Time.deltaTime, 0);
        }
    }
}
