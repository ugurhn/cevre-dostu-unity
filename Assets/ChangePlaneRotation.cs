using UnityEngine;

public class ChangePlaneRotation : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }
}
