using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMenu : MonoBehaviour
{
    public GameObject weaponMenu;
    public Button menuButton; // Knappen f�r att visa menyn
    public Button snowShovelButton; // Knappen f�r att aktivera SnowShovel
    public Button snowballButton; // Knappen f�r att aktivera Snowball
    public SnowShovel snowShovelScript; // Referens till SnowShovel-skriptet som �r knutet till spelaren
    public Snowball snowballScript; // Referens till Snowball-skriptet som �r knutet till spelaren

    void Start()
    {
        // Tilldela klickh�ndelser till knapparna
        snowShovelButton.onClick.AddListener(ToggleSnowShovel);
        snowballButton.onClick.AddListener(ToggleSnowball);
    }

    public void ToggleWeaponMenu()
    {
        // V�xlar aktivitetsstatus f�r vapenmenyn
        weaponMenu.SetActive(!weaponMenu.activeSelf);
    }

    public void ToggleSnowShovel()
    {
        if (snowShovelScript != null)
        {
            snowShovelScript.enabled = true; // Aktiverar SnowShovel-skriptet
            if (snowballScript != null) snowballScript.enabled = false; // Inaktiverar Snowball-skriptet om det finns
        }
    }

    public void ToggleSnowball()
    {
        if (snowballScript != null)
        {
            snowballScript.enabled = true; // Aktiverar Snowball-skriptet
            if (snowShovelScript != null) snowShovelScript.enabled = false; // Inaktiverar SnowShovel-skriptet om det finns
        }
    }
}
