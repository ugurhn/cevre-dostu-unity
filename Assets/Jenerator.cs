using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jenerator : MonoBehaviour
{
    public float power;
    public float regenerationElectric;
    public int connectedDevices;
    public float radius;
    public Vector3 center;
    public float distance = 35f;
    public int myLevel;
    public bool canElectric;

    private void Start()
    {
        int count = PlayerPrefs.GetInt("generatorCount");
        PlayerPrefs.SetInt("generatorCount", count + 1);
        PlayerPrefs.Save();
        if(myLevel == 0)
        {
            myLevel = 1;
        }
    }
    private void OnApplicationQuit()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("GeneratorMachine");
        PlayerPrefs.SetInt("generatorCount", objects.Length);
        for(int i = 0; i < objects.Length; i++)
        {
            PlayerPrefs.SetFloat(i + "generatorPosX", objects[i].transform.position.x);
            PlayerPrefs.SetFloat(i + "generatorPosY", objects[i].transform.position.y);
            PlayerPrefs.SetFloat(i + "generatorPosZ", objects[i].transform.position.z);
            PlayerPrefs.SetInt(i + "generatorLevel", objects[i].transform.GetComponent<Jenerator>().myLevel);
        }
        PlayerPrefs.Save();
    }
    public void LevelUp()
    {
        myLevel++;
        regenerationElectric = myLevel * 3f;
    }
    void Update()
    {
        power += regenerationElectric * Time.deltaTime;
        if(power <= 0f)
        {
            canElectric = false;
        } 
        else
        {
            power -= connectedDevices * Time.deltaTime;
            canElectric = true;
        }
        CheckCloseDistance();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,0,0,0.5f);
        Gizmos.DrawSphere(center, radius);
    }
    public void CheckCloseDistance()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("MusilajMachine");
        GameObject[] taggedObjects1 = GameObject.FindGameObjectsWithTag("GasMachine");
        for (int i = 0; i < taggedObjects.Length; i++)
        {
            if (Vector3.Distance(transform.position,taggedObjects[i].transform.position) <= distance)
             {
                if (canElectric)
                {
                    if (!taggedObjects[i].GetComponent<MusilajCleaner>().isAdded)
                    {
                        connectedDevices++;
                        taggedObjects[i].GetComponent<MusilajCleaner>().isAdded = true;
                    }
                    taggedObjects[i].GetComponent<MusilajCleaner>().electric = true;
                } else
                {
                    taggedObjects[i].GetComponent<MusilajCleaner>().electric = false;
                }
             }
           }
        for (int i = 0; i < taggedObjects1.Length; i++)
        {
            if (Vector3.Distance(transform.position,taggedObjects1[i].transform.position) <= distance)
             {
                if (canElectric)
                {
                    if (!taggedObjects1[i].GetComponent<MusilajCleaner>().isAdded)
                    {
                        connectedDevices++;
                        taggedObjects1[i].GetComponent<MusilajCleaner>().isAdded = true;
                    }
                    taggedObjects1[i].GetComponent<MusilajCleaner>().electric = true;
                } else
                {
                    taggedObjects1[i].GetComponent<MusilajCleaner>().electric = false;
                }
             }
           }
        }
}
