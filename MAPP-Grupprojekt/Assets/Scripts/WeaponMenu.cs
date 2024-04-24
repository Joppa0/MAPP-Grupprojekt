using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMenu : MonoBehaviour
{

    public bool HasChosenWeapon { get; set; }

    public GameObject weaponMenu;
    public Button menuButton; // Knappen f�r att visa menyn
    public Button snowShovelButton; // Knappen f�r att aktivera SnowShovel
    public Button snowballButton; // Knappen f�r att aktivera Snowball
    public Button heatseekButton; // Knappen f�r att aktivera HeatSeek
    /*public SnowShovel snowShovelScript; // Referens till SnowShovel-skriptet som �r knutet till spelaren
    public Snowball snowballScript; // Referens till Snowball-skriptet som �r knutet till spelaren
    public SnowballHeatSeekPrefab heatSeekScript; // Referens till HeatSeek-skriptet som �r knutet till spelaren
    */
    [SerializeField] private Shooting player1, player2;
    private Shooting currentPlayer;

    void Start()
    {
        // Tilldela klickh�ndelser till knapparna
        snowShovelButton.onClick.AddListener(ToggleSnowShovel);
        snowballButton.onClick.AddListener(ToggleSnowball);
        heatseekButton.onClick.AddListener(ToggleHeatSeek);
        currentPlayer = player1;
    }

    public void ToggleWeaponMenu()
    {
        // V�xlar aktivitetsstatus f�r vapenmenyn
        weaponMenu.SetActive(!weaponMenu.activeSelf);
    }

    private void UpdateCurrentPlayer()
    {
        currentPlayer = GameController.currentState == GameController.BattleState.Player1ChooseWeapon ? player1 : player2;
    }

    public void ToggleSnowShovel()
    {
        // Aktiverar SnowShovel och inaktiverar andra vapen
        //if (snowShovelScript != null)
        //{
        //    snowShovelScript.enabled = true;
        //    if (snowballScript != null) snowballScript.enabled = false;
        //    if (heatSeekScript != null) heatSeekScript.enabled = false;
        //    HasChosenWeapon = true;
        //}
        UpdateCurrentPlayer();

        currentPlayer.SetEquippedSnowball(Shooting.Snowballs.SnowShovel);
        
        HasChosenWeapon = true;
    }

    public void ToggleSnowball()
    {
        // Aktiverar Snowball och inaktiverar andra vapen
        //if (snowballScript != null)
        //{
        //    snowballScript.enabled = true;
        //    if (snowShovelScript != null) snowShovelScript.enabled = false;
        //    if (heatSeekScript != null) heatSeekScript.enabled = false;
        //    HasChosenWeapon = true;
        //}
        UpdateCurrentPlayer();

        currentPlayer.SetEquippedSnowball(Shooting.Snowballs.Snowball);
        
        HasChosenWeapon = true;
    }

    public void ToggleHeatSeek()
    {
        // Aktiverar HeatSeek och inaktiverar andra vapen
        //if (heatSeekScript != null)
        //{
        //    heatSeekScript.enabled = true;
        //    if (snowShovelScript != null) snowShovelScript.enabled = false;
        //    if (snowballScript != null) snowballScript.enabled = false;
        //    HasChosenWeapon = true;
        //}
        UpdateCurrentPlayer();

        currentPlayer.SetEquippedSnowball(Shooting.Snowballs.HeatSeeking);
        
        HasChosenWeapon = true;
    }
}
