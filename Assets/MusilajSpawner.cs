using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusilajSpawner : MonoBehaviour
{
    public Vector3 size;
    public Vector3 center;
    public List<GameObject> musilaj = new List<GameObject>();
    public float timer = 40f;

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            SpawnObject();
            timer = 40f;
        }
    }
    public void SpawnObject()
    {
        int deger = Random.Range(0, musilaj.Count);
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        Instantiate(musilaj[deger], pos, musilaj[deger].transform.rotation);
    }
    public void SpawnAnObject()
    {
        int deger = Random.Range(0, musilaj.Count);
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        Instantiate(musilaj[deger], pos, musilaj[deger].transform.rotation);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,0,0,0.5f);
        Gizmos.DrawCube(center, size);
    }
}
