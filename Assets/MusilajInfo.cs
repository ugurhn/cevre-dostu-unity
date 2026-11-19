using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusilajInfo : MonoBehaviour
{
    void Start()
    {
        int lastMusilaj = PlayerPrefs.GetInt("lastMusilaj");
        PlayerPrefs.SetInt("lastMusilaj", lastMusilaj + 1);
        PlayerPrefs.Save();
    }
    private void OnApplicationQuit()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Musilaj");
        PlayerPrefs.SetInt("lastMusilaj", objects.Length);
    }
    public void destroyGameObject()
    {
        int lastMusilaj = PlayerPrefs.GetInt("lastMusilaj");
        PlayerPrefs.SetInt("lastMusilaj", lastMusilaj - 1);
        PlayerPrefs.Save();
    }
}
