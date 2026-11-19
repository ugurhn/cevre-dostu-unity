using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
public class Button3D : MonoBehaviour
{
    public string type;
    public TMP_Text announcementText;
    public MouseLook mouseLook;
    public PlayerMovement player;
    public SmoothGun gun;
    public Vector3 position;
    public GameObject obje;
    public int level;
    public void ChangeCursor()
    {
        player.canMove = true;
        mouseLook.ChangeCursorType(false);
        mouseLook.canLook = true;
        gun.CanSway = true;
    }
    public void GoTask()
    {
        if(player.myMoney >= 10000 * level)
        {
            if (type == "gas" || type == "musilaj")
            {
                LevelUp(1);
                player.ChangeMoney(-(10000 * level));
            }
            else if (type == "jenerator")
            {
                LevelUpGen();
                player.ChangeMoney(-(10000 * level));
            }
        } else
        {
            announcementText.gameObject.SetActive(true);
            announcementText.text = "Bakiye Yetersiz";
            StartCoroutine(changeActive());
            ChangeCursor();
        }
        gameObject.SetActive(false);
    }
    public void LevelUp(int level)
    {
            MusilajCleaner cleaner = obje.GetComponentInParent<MusilajCleaner>();
            cleaner.LevelUp(level);   
    }
    public void LevelUpGen()
    {
            Jenerator jenerator = obje.GetComponentInParent<Jenerator>();
            jenerator.LevelUp();
    }
    IEnumerator changeActive()
    {
        yield return new WaitForSeconds(2f);
        announcementText.gameObject.SetActive(false);
        announcementText.text = "";
    }
}