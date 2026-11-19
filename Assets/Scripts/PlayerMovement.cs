using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public AudioSource audioSource;
    public AudioSource enviSource;
    public AudioClip[] natureClips;
    public Slider müzikVolume;
    public Slider efektVolume;
    public Toggle müzikToggle;
    public Slider viewDistance;
    public Toggle efektToggle;
    bool isMoving;
    public float speed = 7.7f;
    public float gravity = -9.81f;
    Vector3 velocity;
    public Transform groundCheck;
    public LayerMask mask;
    public float groundDistance = 0.4f;
    bool isGrounded;
    public float jumpHeight = 3f;
    public float timer;
    public float timeBetweenSteps;
    public List<AudioClip> sandClips;
    public List<AudioClip> asphaltClips;
    public List<AudioClip> grassClips;
    public CheckerGroundName playerGround;
    Animator anim;
    public float myMoney;
    float saveTimer = 1f;
    public bool canMove = true;
    public GunController gun;
    public TMP_Text text;
    public TMP_Text upgradeText;
    public TMP_Text bakiyeText;
    public GameObject marketPanel;
    public GameObject levelUp;
    public MouseLook mouseLook;
    public Vector3 defaultStart;
    public bool isBuy;
    public float timerBuy = 2f;
    public TMP_Text upgradeAnnouncements;
    public BuildController buildController;
    public LayerMask grass;
    public LayerMask sand;
    public LayerMask asphalt;
    public GameObject options;
    public SmoothGun smooth;
    public float food;
    public float foodCapacity = 100f;
    bool canRun = true;
    public GameObject slider;
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("food", food);
        PlayerPrefs.SetFloat("müzikVolume", müzikVolume.value);
        PlayerPrefs.SetFloat("efektVolume", efektVolume.value);
        PlayerPrefs.SetInt("müzikToggle", müzikToggle.isOn ? 0 : 1);
        PlayerPrefs.SetInt("efektToggle", efektToggle.isOn ? 0 : 1);
        PlayerPrefs.SetFloat("viewDistance", viewDistance.value);
        PlayerPrefs.Save();
    }
    public void Back()
    {
        options.SetActive(false);
        mouseLook.ChangeCursorType(false);
        smooth.CanSway = true;
    }
    private void Awake()
    {
        if(PlayerPrefs.GetFloat("myY") <= 15)
        {
            transform.position = defaultStart;
        } 
        else
        {
            transform.position = new Vector3(PlayerPrefs.GetFloat("myX"), PlayerPrefs.GetFloat("myY"), PlayerPrefs.GetFloat("myZ"));
        }
    }
    void Start()
    {
        if(PlayerPrefs.GetFloat("viewDistance") == 0 || viewDistance.value == 0)
        {
            PlayerPrefs.SetFloat("viewDistance", (viewDistance.maxValue / 2));
            viewDistance.value = (viewDistance.maxValue / 2);
        }
        viewDistance.value = PlayerPrefs.GetFloat("viewDistance");
        müzikVolume.value = PlayerPrefs.GetFloat("müzikVolume");
        efektVolume.value = PlayerPrefs.GetFloat("efektVolume");
        PlayerPrefs.SetInt("developerMode", 0);
        PlayerPrefs.Save();
        food = foodCapacity;
        buildController = GetComponent<BuildController>();
        anim = GetComponent<Animator>();
        float volume = PlayerPrefs.GetFloat("volume");
        AudioListener.volume = volume;
        myMoney = PlayerPrefs.GetFloat("myMoney");
    }
    public void GoMainMenu()
    {
        Application.Quit();
    }
    public void BuyMusilaj()
    {
        if (myMoney >= 6000)
        {
            buildController.buildingObject = "musilaj";
            buildController.canBuild = true;
            marketPanel.SetActive(false);
            mouseLook.ChangeCursorType(marketPanel.activeSelf);
            ChangeMoney(-6000);
        }
        else
        {
            text.text = "Yetersiz Bakiye";
        }
        text.gameObject.SetActive(true);
        isBuy = true;
    }
    public void BuyGasCleaner()
    {
        if (myMoney >= 12000)
        {
            buildController.buildingObject = "gas";
            buildController.canBuild = true;
            marketPanel.SetActive(false);
            mouseLook.ChangeCursorType(marketPanel.activeSelf);
            ChangeMoney(-12000);
        }
        else
        {
            text.text = "Yetersiz Bakiye";
        }
        text.gameObject.SetActive(true);
        isBuy = true;
    }
    public void BuyGenerator()
    {
        if (myMoney >= 10000)
        {
            buildController.buildingObject = "generator";
            buildController.canBuild = true;
            marketPanel.SetActive(false);
            mouseLook.ChangeCursorType(marketPanel.activeSelf);
            ChangeMoney(-10000);
        }
        else
        {
            text.text = "Yetersiz Bakiye";
        }
        text.gameObject.SetActive(true);
        isBuy = true;
    }
    public void UpgradeWeapon()
    {
        isBuy = true;
        if (PlayerPrefs.GetInt("level") == 0)
        {
            if(myMoney >= 1200)
            {
                ChangeMoney(-1200);
                PlayerPrefs.SetInt("level", 1);
                PlayerPrefs.Save();
                text.text = "Silahın Leveli Yükseltildi: 1";
                upgradeAnnouncements.text = "Silah Güçlendirilir yeni seviyede gelen özellİk: Menzil uzatılır";
                upgradeText.text = "Yükselt: 2000";
            } else
            {
                text.text = "Yetersiz Bakiye";
            }
        } else if(PlayerPrefs.GetInt("level") == 1)
        {
            if (myMoney >= 2000)
            {
                ChangeMoney(-2000);
                PlayerPrefs.SetInt("level", 2);
                PlayerPrefs.Save();
                text.text = "Silahın Leveli Yükseltildi: 2";
                upgradeAnnouncements.text = "Silah Güçlendirilir yeni seviyede gelen özellİk: Menzil uzatılır ve enerji daha hızlı dolar";
                upgradeText.text = "Yükselt: 4000";
            }
            else
            {
                text.text = "Yetersiz Bakiye";
            }
        }
        else if (PlayerPrefs.GetInt("level") == 2)
        {
            if (myMoney >= 4000)
            {
                ChangeMoney(-4000);
                PlayerPrefs.SetInt("level", 3);
                PlayerPrefs.Save();
                text.text = "Silahın Leveli Yükseltildi: 3"; 
                upgradeAnnouncements.text = "Silah Güçlendirilir yeni seviyede gelen özellİk: Enerjisi daha hızlı dolar, menzil uzatılır ve 2 katı para verir";
                upgradeText.text = "Yükselt: 20000";
            }
            else
            {
                text.text = "Yetersiz Bakiye";
            }
        }
        else if (PlayerPrefs.GetInt("level") == 3)
        {
            if (myMoney >= 20000)
            {
                ChangeMoney(-20000);
                PlayerPrefs.SetInt("level", 4);
                PlayerPrefs.Save();
                text.text = "Silahın Leveli Yükseltildi: 4";
                upgradeAnnouncements.text = "Maksimum";
                upgradeText.text = "Maksimum";
                upgradeText.GetComponentInParent<Button>().interactable = false;
            }
            else
            {
                text.text = "Yetersiz Bakiye";
            }
        }
        text.gameObject.SetActive(true);
    }
    public void ChangeMoney(int price)
    {
        if(PlayerPrefs.GetInt("level") > 4)
        {
            myMoney += price*2;
            PlayerPrefs.SetFloat("myMoney", myMoney);
            PlayerPrefs.Save();
        } else
        {
            myMoney += price;
            PlayerPrefs.SetFloat("myMoney", myMoney);
            PlayerPrefs.Save();
        }
    }
    public void AddFood(float miktar)
    {
        if((miktar + food) >= foodCapacity)
        {
            food = foodCapacity;
        } else
        {
            food += miktar;
        }
    }
    void Update()
    {
        if (müzikToggle.isOn)
        {
            enviSource.enabled = true;
        } else
        {
            enviSource.enabled = false;
        }
        if (efektToggle.isOn)
        {
            audioSource.enabled = true;
        } else
        {
            audioSource.enabled = false;
        }
        mouseLook.gameObject.GetComponent<Camera>().farClipPlane = viewDistance.value;
        enviSource.volume = müzikVolume.value;
        audioSource.volume = efektVolume.value;
        if (!enviSource.isPlaying)
        {
            if (enviSource.enabled)
            {
                enviSource.clip = natureClips[Random.Range(0, natureClips.Length)];
                enviSource.Play();
            }
        }
        if (PlayerPrefs.GetInt("developerMode") == 1)
        {
            ChangeMoney(9000000);
            PlayerPrefs.SetInt("developerMode", 0);
            PlayerPrefs.Save();
        }
        slider.GetComponent<Slider>().value = food;
        food -= Time.deltaTime * 0.007f;
        if(food <= 0f)
        {
            canRun = false;
        } else
        {
            canRun = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            options.SetActive(!options.activeSelf);
            mouseLook.ChangeCursorType(options.activeSelf);
            smooth.CanSway = !options.activeSelf;
        }
        bakiyeText.text = "Bakiye:" + PlayerPrefs.GetFloat("myMoney");
        if (isBuy == true)
        {
            timerBuy -= Time.deltaTime;
        }
        if(timerBuy <= 0f)
        {
            text.gameObject.SetActive(false);
            timerBuy = 2f;
            isBuy = false;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            marketPanel.SetActive(!marketPanel.activeSelf);
            mouseLook.ChangeCursorType(marketPanel.activeSelf);
            smooth.CanSway = !marketPanel.activeSelf;
        }
        saveTimer -= Time.deltaTime;
        if(saveTimer <= 0f)
        {
            PlayerPrefs.SetFloat("myX", transform.position.x);
            PlayerPrefs.SetFloat("myY", transform.position.y);
            PlayerPrefs.SetFloat("myZ", transform.position.z);
            PlayerPrefs.Save();
            saveTimer = 1f;
        }
#region Movement
        if(Physics.CheckSphere(groundCheck.position, groundDistance, mask) == true || Physics.CheckSphere(groundCheck.position, groundDistance, grass) == true || Physics.CheckSphere(groundCheck.position, groundDistance, sand) == true|| Physics.CheckSphere(groundCheck.position, groundDistance, asphalt) == true){
            isGrounded = true;
        } else
        {
            isGrounded = false;
        }
        if (isGrounded && velocity.y >= 0f)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * y;
        if (canMove)
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                if (canRun)
                {
                    controller.Move(move * speed * Time.deltaTime);
                    timeBetweenSteps = 0.37f;
                } else
                {
                    controller.Move(move * 3f * Time.deltaTime);
                    timeBetweenSteps = 0.37f;
                }
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                if (canRun)
                {
                    controller.Move(move * (speed + 8f) * Time.deltaTime);
                    timeBetweenSteps = 0.27f;
                } else
                {
                    controller.Move(move * 3f * Time.deltaTime);
                    timeBetweenSteps = 0.27f;
                }

            }
        }
        if (x != 0 || y != 0)
        {
            isMoving = true;
            anim.SetBool("isWalking", true);
        }
        else
        {
            isMoving = false;
            anim.SetBool("isWalking", false);
        }
        if (isMoving && isGrounded)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (audioSource.enabled)
                {
                    if (playerGround.GetGround() == "SandGround")
                    {
                        audioSource.clip = sandClips[Random.Range(0, sandClips.Count)];
                        float pitch = Random.Range(0.70f, 1.18f);
                        audioSource.pitch = pitch;
                        audioSource.Play();
                    }
                    else if (playerGround.GetGround() == "GrassGround")
                    {
                        audioSource.clip = grassClips[Random.Range(0, grassClips.Count)];
                        float pitch = Random.Range(0.70f, 1.18f);
                        audioSource.pitch = pitch;
                        audioSource.Play();
                    }
                    else if (playerGround.GetGround() == "AsphaltGround")
                    {
                        audioSource.clip = asphaltClips[Random.Range(0, asphaltClips.Count)];
                        float pitch = Random.Range(0.70f, 1.18f);
                        audioSource.pitch = pitch;
                        audioSource.Play();
                    }
                }
                timer = timeBetweenSteps;
            }
        } else
        {
            timer = timeBetweenSteps;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            StartCoroutine(HasGround());

        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
#endregion

    }
    private IEnumerator HasGround()
    {
        if (audioSource.enabled)
        {
            if (playerGround.GetGround() == "SandGround")
            {
                audioSource.clip = sandClips[Random.Range(0, sandClips.Count)];
            }
            else if (playerGround.GetGround() == "GrassGround")
            {
                audioSource.clip = grassClips[Random.Range(0, grassClips.Count)];
            }
            else if (playerGround.GetGround() == "AsphaltGround")
            {
                audioSource.clip = asphaltClips[Random.Range(0, asphaltClips.Count)];
            }
            float pitch = Random.Range(0.70f, 1.30f);
            audioSource.pitch = pitch;
            yield return new WaitForSeconds(1f);
            audioSource.Play();
        }
    }
}