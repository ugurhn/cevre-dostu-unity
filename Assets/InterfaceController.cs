using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    [SerializeField]
    Text txt;
    private void OnEnable()
    {
        CamInteraction.itemCanGet += InInteraction;
    }
    private void OnDisable()
    {
        CamInteraction.itemCanGet -= InInteraction;
    }
    void Start()
    {
        txt.gameObject.SetActive(false);
    }
    void InInteraction(string itemName, bool interaction)
    {
        txt.text ="Almak İçin [E] Tuşuna Basınız: " + itemName;
        txt.gameObject.SetActive(interaction);
    }
}
