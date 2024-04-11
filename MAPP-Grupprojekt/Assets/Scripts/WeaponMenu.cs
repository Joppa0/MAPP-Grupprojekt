using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMenu : MonoBehaviour
{
    public GameObject weaponMenu;
    public Button menuButton; // Knappen för att visa menyn
    public Button snowShovelButton; // Knappen för att inaktivera SnowShovel
    public SnowShovel snowShovelScript; // Referens till SnowShovel-skriptet som är knutet till spelaren

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleWeaponMenu()
    {
        // Antingen växla aktivitetsstatus för Canvas eller sätt den till aktiv
        weaponMenu.SetActive(!weaponMenu.activeSelf);
    }

    public void ToggleSnowShovel()
    {
        if (snowShovelScript != null)
        {
            snowShovelScript.enabled = !snowShovelScript.enabled; // Toogla aktiviteten för SnowShovel-skriptet
        }


    }
}
