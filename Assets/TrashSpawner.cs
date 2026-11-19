using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public Vector3 size;
    public Vector3 center;
    public List<GameObject> trashs = new List<GameObject>();
    public float timer;

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            SpawnObject();
            timer = 30f;
        }
    }
    public void SpawnObject()
    {
        int deger = Random.Range(0, trashs.Count);
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        Instantiate(trashs[deger], pos, trashs[deger].transform.rotation);
    }
    public void SpawnAnObject(int objectId)
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2) + 0.7f, Random.Range(-size.z / 2, size.z / 2));
        Instantiate(trashs[objectId], pos, trashs[objectId].transform.rotation);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,0,0,0.5f);
        Gizmos.DrawCube(center, size);
    }
}
