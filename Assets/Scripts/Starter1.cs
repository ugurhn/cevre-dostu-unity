using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Starter1 : MonoBehaviour
{
    public GameObject slider;
    private void Start()
    {
        slider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("volume");
    }

    void Update()
    {
        float value = slider.GetComponent<Slider>().value;
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("volume", value);
        PlayerPrefs.Save();
    }
}
