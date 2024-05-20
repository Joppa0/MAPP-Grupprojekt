using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowActionController : MonoBehaviour


{
    public GameObject p1Move;
    public GameObject p2Move;
    public GameObject p1Weapon;
    public GameObject p2Weapon;
    public GameObject p1Shoot;
    public GameObject p2Shoot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void P1Move()
    {
        p1Move.SetActive(!p1Move.activeSelf);

    }

    public void P2Move()
    {
        p2Move.SetActive(!p2Move.activeSelf);

    }

    public void P1ChooseWeapon()
    {
        p1Weapon.SetActive(!p1Weapon.activeSelf);

    }

    public void P2ChooseWeapon()
    {
        p2Weapon.SetActive(!p2Weapon.activeSelf);

    }

    public void P1Shoot()
    {
        p1Shoot.SetActive(!p1Shoot.activeSelf);

    }

    public void P2Shoot()
    {
        p2Shoot.SetActive(!p2Shoot.activeSelf);

    }
}
