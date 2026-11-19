using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saver_Loader : MonoBehaviour
{
    public TrashSpawner spawner;
    public MusilajSpawner spawner1;
    public BuildController build;
    private void Start()
    {
        LoadGame();
    }
    private void LoadGame()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("musilajCount"); i++)
        {
            build.CreateMachine("musilaj", new Vector3(PlayerPrefs.GetFloat(i + "musilajPosX"), PlayerPrefs.GetFloat(i + "musilajPosY"), PlayerPrefs.GetFloat(i + "musilajPosZ")), PlayerPrefs.GetInt(i + "musilajLevel"));
        }
        for (int i = 0; i < PlayerPrefs.GetInt("gasCount"); i++)
        {
            build.CreateMachine("gas", new Vector3(PlayerPrefs.GetFloat(i + "gasPosX"), PlayerPrefs.GetFloat(i + "gasPosY"), PlayerPrefs.GetFloat(i + "gasPosZ")), PlayerPrefs.GetInt(i + "gasLevel"));
        }
        for (int i = 0; i < PlayerPrefs.GetInt("generatorCount"); i++)
        {
            build.CreateMachine("generator", new Vector3(PlayerPrefs.GetFloat(i + "generatorPosX"), PlayerPrefs.GetFloat(i + "generatorPosY"), PlayerPrefs.GetFloat(i + "generatorPosZ")), PlayerPrefs.GetInt(i + "generatorLevel"));
        }
        for(int i = 0; i < PlayerPrefs.GetInt("lastMusilaj"); i++)
        {
            spawner1.SpawnObject();
        }
        for (int i = 0; i < PlayerPrefs.GetInt("lastPet"); i++)
        {
            spawner.SpawnAnObject(0);
        }
        for (int i = 0; i < PlayerPrefs.GetInt("lastCamSise"); i++)
        {
            spawner.SpawnAnObject(4);
        }
        for (int i = 0; i < PlayerPrefs.GetInt("lastPasli"); i++)
        {
            spawner.SpawnAnObject(1);
        }
        for (int i = 0; i < PlayerPrefs.GetInt("lastPassiz"); i++)
        {
            spawner.SpawnAnObject(2);
        }
        for (int i = 0; i < PlayerPrefs.GetInt("lastUcak"); i++)
        {
            spawner.SpawnAnObject(3);
        }
        PlayerPrefs.SetInt("lastPet", 0);
        PlayerPrefs.SetInt("lastPasli", 0);
        PlayerPrefs.SetInt("lastPassiz", 0);
        PlayerPrefs.SetInt("lastUcak", 0);
        PlayerPrefs.SetInt("lastCamSise", 0);
        PlayerPrefs.Save();
    }
}
