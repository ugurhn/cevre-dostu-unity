using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCreater : MonoBehaviour
{
    public float timer = 50f;
    public float maxTimer = 50f;
    public Vector3 center;
    public Vector3 size;
    public GameObject food;
    private void Update()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Food");
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            if (objects.Length <= 5)
            {
                Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
                
                Instantiate(food, pos, Quaternion.Euler(-90f, 0f, 0f));
            }
            timer = maxTimer;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
