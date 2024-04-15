using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMenu : MonoBehaviour
{
    public GameObject weaponMenu;
    public Button menuButton; // Knappen för att visa menyn
    public Button snowShovelButton; // Knappen för att aktivera SnowShovel
    public Button snowballButton; // Knappen för att aktivera Snowball
    public SnowShovel snowShovelScript; // Referens till SnowShovel-skriptet som är knutet till spelaren
    public Snowball snowballScript; // Referens till Snowball-skriptet som är knutet till spelaren

    void Start()
    {
        // Tilldela klickhändelser till knapparna
        snowShovelButton.onClick.AddListener(ToggleSnowShovel);
        snowballButton.onClick.AddListener(ToggleSnowball);
    }

    public void ToggleWeaponMenu()
    {
        // Växlar aktivitetsstatus för vapenmenyn
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
