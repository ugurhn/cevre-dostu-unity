using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunController : MonoBehaviour
{
    public float distance;
    public PlayerMovement controller;
    public Text txt;
    public Text txt2;
    public ParticleSystem particle;
    public float particleTimer = 0.4f;
    AudioSource source;
    public AudioClip clip;
    public int level;
    public TrashSpawner spawner;
    public float myPower;
    public float regenerationSpeed = 4f;
    bool canShot;
    public TMP_Text announcementText;
    float timer = 2f;

    private void Start()
    {
        controller = GetComponentInParent<PlayerMovement>();
        source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        level = PlayerPrefs.GetInt("level");
        myPower -= Time.deltaTime;
        myPower += Time.deltaTime * regenerationSpeed;
        if(announcementText.gameObject.activeSelf == true)
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0)
        {
            announcementText.gameObject.SetActive(false);
            timer = 2f;
        }
        if(myPower <= 0f)
        {
            canShot = false;
        } else
        {
            canShot = true;
        }
    }
    void LateUpdate()
    {
        if (level == 0)
        {
            distance = 10f;
        }
        else if (level == 1)
        {
            distance = 11.5f;
        }
        else if (level == 2)
        {
            distance = 13f;
        }
        else if (level == 3)
        {
            distance = 14f;
        }
        else if (level == 4)
        {
            distance = 15f;
        }
        if (particle.gameObject.activeSelf)
        {
            particleTimer -= Time.deltaTime;
        }
        if(particleTimer <= 0f)
        {
            particle.gameObject.SetActive(false);
            particleTimer = 0.4f;
        }
        GameObject camera = GetComponentInParent<MouseLook>().gameObject;
        RaycastHit hit;
        Debug.DrawRay(camera.transform.position, camera.transform.forward * distance, Color.yellow);
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, distance))
        {
            if (hit.transform.tag == "Trash")
            {
                txt.gameObject.SetActive(true);
                txt.text = "Çöpü Dönüştürmek İçin [Sol Click] Tuşuna Basınız: "+ hit.transform.gameObject.GetComponent<TrashInfo>().trashName;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (canShot && myPower >= 25f)
                    {
                        ConvertTrash(hit.transform.gameObject);
                        Destroy(hit.transform.gameObject);
                        myPower -= 25f;
                    } else
                    {
                        announcementText.gameObject.SetActive(true);
                        announcementText.text = "Siahın Yeteri Kadar Enerjisi Yok!";
                    }
                }
            } 
            else
            {
                txt.gameObject.SetActive(false);
            }
        }
    }
    public void ConvertTrash(GameObject trash)
    {
        source.PlayOneShot(clip);
        particle.gameObject.SetActive(true);
        TrashInfo info = trash.GetComponent<TrashInfo>();
        info.destroyGameObject();
        txt2.text = "Çöp Dönüştürüldü: " + info.trashName +"\nHesaba Eklenen Tutar:" + info.price;
        StartCoroutine(changeTextActiveSelf());
        controller.ChangeMoney(info.price);
        if(Random.Range(0,1) == 0)
        {
            spawner.SpawnObject();
        }
        particleTimer = 0.4f;
    }
    IEnumerator changeTextActiveSelf()
    {
        txt2.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        txt2.gameObject.SetActive(false);
        StopAllCoroutines();
    }
}
