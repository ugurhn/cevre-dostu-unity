using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Starter : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject mainPanel;
    public GameObject loadScreen;
    public GameObject slider;

    void Update()
    {
        float value = slider.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("volume", value);
        PlayerPrefs.Save();
    }
    public void StartButton()
    {
        loadScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }
    public void OptionsButton()
    {
        optionsPanel.SetActive(true);
        mainPanel.SetActive(false);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void Back()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }
}
