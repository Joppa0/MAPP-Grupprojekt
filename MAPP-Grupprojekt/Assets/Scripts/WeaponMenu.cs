using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMenu : MonoBehaviour
{
    public GameObject weaponMenu;
    public Button button;

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

}
