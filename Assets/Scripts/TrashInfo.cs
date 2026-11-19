using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TrashInfo : MonoBehaviour
{
    public string trashName;
    public int price;
    public int myType;
    //0=Pet, 1=Paslı,2=passiz,3=ucak
    private void Start()
    {
        if (myType == 0)
        {
            int lastPet = PlayerPrefs.GetInt("lastPet");
            PlayerPrefs.SetInt("lastPet", lastPet + 1);
            PlayerPrefs.Save();
        } else if (myType == 1)
        {
            int lastPasli = PlayerPrefs.GetInt("lastPasli");
            PlayerPrefs.SetInt("lastPasli", lastPasli + 1);
            PlayerPrefs.Save();
        }
        else if (myType == 2)
        {
            int lastPassiz = PlayerPrefs.GetInt("lastPassiz");
            PlayerPrefs.SetInt("lastPassiz", lastPassiz + 1);
            PlayerPrefs.Save();
        }
        else if (myType == 3)
        {
            int lastUcak = PlayerPrefs.GetInt("lastUcak");
            PlayerPrefs.SetInt("lastUcak", lastUcak + 1);
            PlayerPrefs.Save();
        } else if(myType == 4)
        {
            int lastCamSise = PlayerPrefs.GetInt("lastCamSise");
            PlayerPrefs.SetInt("lastCamSise", lastCamSise + 1);
            PlayerPrefs.Save();
        }
    }
    public void destroyGameObject()
    {
        if (myType == 0)
        {
            int lastPet = PlayerPrefs.GetInt("lastPet");
            PlayerPrefs.SetInt("lastPet", lastPet - 1);
            PlayerPrefs.Save();
        }
        else if (myType == 1)
        {
            int lastPasli = PlayerPrefs.GetInt("lastPasli");
            PlayerPrefs.SetInt("lastPasli", lastPasli - 1);
            PlayerPrefs.Save();
        }
        else if (myType == 2)
        {
            int lastPassiz = PlayerPrefs.GetInt("lastPassiz");
            PlayerPrefs.SetInt("lastPassiz", lastPassiz - 1);
            PlayerPrefs.Save();
        }
        else if (myType == 3)
        {
            int lastUcak = PlayerPrefs.GetInt("lastUcak");
            PlayerPrefs.SetInt("lastUcak", lastUcak - 1);
            PlayerPrefs.Save();
        } else if(myType == 4)
        {
                int lastCam = PlayerPrefs.GetInt("lastCamSise");
                PlayerPrefs.SetInt("lastCamSise", lastCam - 1);
                PlayerPrefs.Save();
        }
    }
}
