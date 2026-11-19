using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomGiveInfo : MonoBehaviour
{
    public float timer;
    public string[] stringList;
    public string[] baslikList;
    public GameObject panel;
    public TMP_Text aciklama;
    public TMP_Text baslik;
    public MouseLook look;
    public SmoothGun gun;
    public PlayerMovement player;
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            GiveInfo();
            timer = 120f;
        }
    }
    public void ChangeCursor()
    {
        gun.CanSway = true;
        look.canLook = true;
        look.ChangeCursorType(false);
        player.canMove = true;
    }
    public void GiveInfo()
    {
        int random = Random.Range(0, stringList.Length);
        aciklama.text = stringList[random];
        baslik.text = baslikList[random];
        panel.SetActive(true);
        gun.CanSway = false;
        look.canLook = false;
        look.ChangeCursorType(true);
        player.canMove = false;
    }
}
