using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MusilajCleaner : MonoBehaviour
{
    public bool electric;
    public ParticleSystem[] particles;
    public float usingElectric;
    public bool isAdded = false;
    public int myLevel;
    public float removeMusilajTimer;
    public float maxMusilajTimer = 50f;
    PlayerMovement player;
    TMP_Text announcementText;
    public GameObject[] musilajList;
    public int myType;
    public float gasTimer;
    public float maxGasTimer = 30f;
    private void Start()
    {
        if(myType == 1)
        {
            int count = PlayerPrefs.GetInt("gasCount");
            PlayerPrefs.SetInt("gasCount", count + 1);
            PlayerPrefs.Save();
        }
        if(myType == 0)
        {
            int count = PlayerPrefs.GetInt("musilajCount");
            PlayerPrefs.SetInt("musilajCount", count + 1);
            PlayerPrefs.Save();
        }  
        if(myLevel == 0)
        {
            myLevel = 1;
        }
        GameObject obje = GameObject.FindGameObjectWithTag("Player");
        player = obje.GetComponent<PlayerMovement>();
        announcementText = GameObject.FindGameObjectWithTag("announcementtext").GetComponent<TMP_Text>();
        gasTimer = maxGasTimer;
    }
    public void LevelUp(int levelUp)
    {
        myLevel += levelUp;
        usingElectric = usingElectric / myLevel;
        maxMusilajTimer = maxMusilajTimer / myLevel;
        removeMusilajTimer = maxMusilajTimer;
        gasTimer = maxGasTimer / myLevel;
    }
    
    private void OnApplicationQuit()
    {
        string type;
        if (myType == 0)
        {
            type = "musilaj";
            GameObject[] objects = GameObject.FindGameObjectsWithTag("MusilajMachine");
            PlayerPrefs.SetInt(type + "Count", objects.Length);
            for (int i = 0; i < objects.Length; i++)
            {
                PlayerPrefs.SetFloat(i + type + "PosX", objects[i].transform.position.x);
                PlayerPrefs.SetFloat(i + type + "PosY", objects[i].transform.position.y);
                PlayerPrefs.SetFloat(i + type + "PosZ", objects[i].transform.position.z);
                PlayerPrefs.SetInt(i + "musilajLevel", objects[i].transform.GetComponent<MusilajCleaner>().myLevel);
            }
            PlayerPrefs.Save();
        }
        else if(myType == 1)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("GasMachine");
            PlayerPrefs.SetInt("gasCount", objects.Length);
            for (int i = 0; i < objects.Length; i++)
            {
                PlayerPrefs.SetFloat(i + "gasPosX", objects[i].transform.position.x);
                PlayerPrefs.SetFloat(i + "gasPosY", objects[i].transform.position.y);
                PlayerPrefs.SetFloat(i + "gasPosZ", objects[i].transform.position.z);
                PlayerPrefs.SetInt(i + "gasLevel", objects[i].transform.GetComponent<MusilajCleaner>().myLevel);
            }
            PlayerPrefs.Save();
        }
        
    }
    private void OnSceneQuit()
    {
        string type;
        if (myType == 0)
        {
            type = "musilaj";
            GameObject[] objects = GameObject.FindGameObjectsWithTag("MusilajMachine");
            PlayerPrefs.SetInt(type + "Count", objects.Length);
            for (int i = 0; i < objects.Length; i++)
            {
                PlayerPrefs.SetFloat(i + type + "PosX", objects[i].transform.position.x);
                PlayerPrefs.SetFloat(i + type + "PosY", objects[i].transform.position.y);
                PlayerPrefs.SetFloat(i + type + "PosZ", objects[i].transform.position.z);
                PlayerPrefs.SetInt(i + "musilajLevel", objects[i].transform.GetComponent<MusilajCleaner>().myLevel);
            }
            PlayerPrefs.Save();
        }
        else if (myType == 1)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("GasMachine");
            PlayerPrefs.SetInt("gasCount", objects.Length);
            for (int i = 0; i < objects.Length; i++)
            {
                PlayerPrefs.SetFloat(i + "gasPosX", objects[i].transform.position.x);
                PlayerPrefs.SetFloat(i + "gasPosY", objects[i].transform.position.y);
                PlayerPrefs.SetFloat(i + "gasPosZ", objects[i].transform.position.z);
                PlayerPrefs.SetInt(i + "gasLevel", objects[i].transform.GetComponent<MusilajCleaner>().myLevel);
            }
            PlayerPrefs.Save();
        }

    }
    void Update()
    {
        if(myType == 0)
        {
            musilajList = GameObject.FindGameObjectsWithTag("Musilaj");
            if (electric)
            {
                removeMusilajTimer -= Time.deltaTime;
                if (removeMusilajTimer <= 0)
                {
                    if (musilajList.Length > 0)
                    {
                        int deger = Random.Range(0, musilajList.Length);
                        musilajList[deger].gameObject.GetComponent<MusilajInfo>().destroyGameObject();
                        int randomMoney = Random.Range(10, 100);
                        player.ChangeMoney(randomMoney);
                        announcementText.text = "Müsilaj Temizlendi Gelen Para: " + randomMoney;
                        StartCoroutine(changeTextActive());
                        Destroy(musilajList[deger]);
                        removeMusilajTimer = maxMusilajTimer;
                    }
                    else
                    {
                        announcementText.text = "Denizde Müsilaj Kalmadı";
                        StartCoroutine(changeTextActive());
                    }
                }
                for (int i = 0; i < particles.Length; i++)
                {
                    particles[i].gameObject.SetActive(true);
                }

            }
            else
            {
                for (int i = 0; i < particles.Length; i++)
                {
                    particles[i].gameObject.SetActive(false);
                }
            }
        } else if(myType == 1)
        {
            if (electric)
            {
                gasTimer -= Time.deltaTime;
                if (gasTimer <= 0)
                {
                    int randomMoney = Random.Range(10, 50);
                    player.ChangeMoney(randomMoney);
                    announcementText.text = "Kirli Gazlar Temizlendi Gelen Para: " + randomMoney;
                    StartCoroutine(changeTextActive());
                    gasTimer = maxGasTimer;
                }
                for (int i = 0; i < particles.Length; i++)
                {
                    particles[i].gameObject.SetActive(true);
                }
            } else
            {
                for (int i = 0; i < particles.Length; i++)
                {
                    particles[i].gameObject.SetActive(false);
                }
            }
        }
    }
    IEnumerator changeTextActive()
    {
        yield return new WaitForSeconds(1.5f);
        announcementText.text = "";
    }
}
