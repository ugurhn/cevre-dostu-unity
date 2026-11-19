using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildController : MonoBehaviour
{
    public GameObject generatorPre;
    public GameObject musilajPre;
    public GameObject gasPre;
    Transform socket;
    public GameObject musilajMachine;
    public GameObject gasMachine;
    public GameObject generatorMachine;
    public TMP_Text announcementTxt;
    public GameObject marketPanel;
    public MouseLook mouseLook;
    public bool canBuild = false;
    public Camera cam;
    public Vector3 firstPre;
    public string buildingObject;
    public GameObject levelUpMenu;
    public SmoothGun smooth;
    public PlayerMovement player;
    public TMP_Text gerekenPara;
    public TMP_Text nowLevel;
    public TMP_Text unlockNew;
    void Update()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray,out hit, 20f))
        {
            if(hit.transform.tag == "GasMachine" || hit.transform.tag == "MusilajMachine" || hit.transform.tag == "GeneratorMachine")
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    Button3D button = levelUpMenu.gameObject.GetComponent<Button3D>();
                    button.obje = hit.transform.gameObject;
                    if (hit.transform.tag == "GasMachine")
                    {
                        button.type = "gas";
                        nowLevel.text = "Şuanki Seviye: " + button.obje.GetComponent<MusilajCleaner>().myLevel;
                        gerekenPara.text = "Gereken Para: " + button.obje.GetComponent<MusilajCleaner>().myLevel * 10000;
                        button.level = button.obje.GetComponent<MusilajCleaner>().myLevel;
                        unlockNew.text = "Sonraki sevİyede gelenler: Kirli Gaz Temizlenme Süresi Azalır ve Elektrik Harcama Düzeyi Düşer";
                    }
                    else if (hit.transform.tag == "MusilajMachine")
                    {
                        button.type = "musilaj";
                        nowLevel.text = "Şuanki Seviye:" + button.obje.GetComponent<MusilajCleaner>().myLevel;
                        gerekenPara.text = "Gereken Para: " + button.obje.GetComponent<MusilajCleaner>().myLevel * 10000;
                        button.level = button.obje.GetComponent<MusilajCleaner>().myLevel;
                        unlockNew.text = "Sonraki sevİyede gelenler: Musilaj Temizlenme Süresi Azalır ve Elektrik Harcama Düzeyi Düşer";
                    }
                    else if (hit.transform.tag == "GeneratorMachine")
                    {
                        button.type = "jenerator";
                        nowLevel.text = "Şuanki Seviye:" + button.obje.GetComponent<Jenerator>().myLevel;
                        button.level = button.obje.GetComponent<Jenerator>().myLevel;
                        gerekenPara.text = "Gereken Para: " + button.obje.GetComponent<Jenerator>().myLevel * 10000;
                        unlockNew.text = "Sonraki sevİyede gelenler: Enerji 2 Katı Hızla Yenilenir";
                    }
                    player.canMove = false;
                    levelUpMenu.SetActive(true);
                    mouseLook.ChangeCursorType(true);
                    smooth.CanSway = false;
                }
            }
        }
        if (canBuild)
        {
            if (Physics.Raycast(ray,out hit, 20f))
            {
                if(buildingObject == "musilaj")
                {
                    if (hit.transform.tag == "Socket")
                    {
                        announcementTxt.gameObject.SetActive(false);
                        socket = hit.transform;
                        musilajPre.transform.position = new Vector3(socket.transform.position.x, socket.transform.position.y + 0.5f, socket.transform.position.z);
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            GameObject spawnMusilaj = Instantiate(musilajMachine, socket.transform.position, Quaternion.identity);
                            canBuild = false;
                            musilajPre.transform.position = firstPre;
                            Destroy(socket.gameObject);
                        }
                    }
                    else
                    {
                        musilajPre.transform.position = new Vector3(hit.point.x, hit.point.y + 3f, hit.point.z);
                        announcementTxt.gameObject.SetActive(true);
                        announcementTxt.text = "Lütfen ekranı tam olarak deniz kenarındaki sokete denk getiriniz.";

                    }
                }
                if(buildingObject == "generator")
                {
                        generatorPre.transform.position = new Vector3(hit.point.x, hit.point.y + 1f, hit.point.z);
                    if (hit.transform.gameObject.layer == 9 || hit.transform.gameObject.layer == 12 || hit.transform.gameObject.layer == 13 || hit.transform.gameObject.layer == 14 || hit.transform.gameObject.layer == 15)
                    {
                        announcementTxt.gameObject.SetActive(false);
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            GameObject spawnGenerator = Instantiate(generatorMachine, new Vector3(hit.point.x, hit.point.y + 0.3f, hit.point.z), Quaternion.identity);
                            canBuild = false;
                            generatorPre.transform.position = firstPre;
                        }
                    } else
                    {
                        announcementTxt.gameObject.SetActive(true);
                        announcementTxt.text = "Lütfen ekranı tam olarak düzgün bir yere getiriniz.";
                    }
                }
                if(buildingObject == "gas")
                {
                        gasPre.transform.position = new Vector3(hit.point.x, hit.point.y + 1f, hit.point.z);
                    if (hit.transform.gameObject.layer == 9 || hit.transform.gameObject.layer == 12 || hit.transform.gameObject.layer == 13 || hit.transform.gameObject.layer == 14 || hit.transform.gameObject.layer == 15)
                    {
                        announcementTxt.gameObject.SetActive(false);
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            GameObject spawnGenerator = Instantiate(gasMachine, new Vector3(hit.point.x, hit.point.y + 0.6f, hit.point.z), Quaternion.identity);
                            canBuild = false;
                            gasPre.transform.position = firstPre;
                        }
                    } else
                    {
                        announcementTxt.gameObject.SetActive(true);
                        announcementTxt.text = "Lütfen ekranı tam olarak düzgün bir yere getiriniz.";
                    }
                }
            }
        }
    }
    public void CreateMachine(string type, Vector3 position, int level)
    {
         if(type == "musilaj")
        {
            GameObject spawnMusilaj = Instantiate(musilajMachine, position, Quaternion.identity);
            spawnMusilaj.GetComponent<MusilajCleaner>().myLevel = level;
        } else if(type == "generator")
        {
            GameObject spawnGenerator = Instantiate(generatorMachine, position, Quaternion.identity);
            spawnGenerator.GetComponent<Jenerator>().myLevel = level;
        } else if(type == "gas")
        {
            GameObject spawnGas = Instantiate(gasMachine, position, Quaternion.identity);
            spawnGas.GetComponent<MusilajCleaner>().myLevel = level;
        }
    }
}
