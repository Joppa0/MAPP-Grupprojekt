using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMenu : MonoBehaviour
{

    public bool HasChosenWeapon { get; set; }

    public GameObject weaponMenu;
    public Button menuButton; // Knappen för att visa menyn
    public Button snowShovelButton; // Knappen för att aktivera SnowShovel
    public Button snowballButton; // Knappen för att aktivera Snowball
    public Button heatseekButton; // Knappen för att aktivera HeatSeek
    [SerializeField] private int heatSeekingShotsPlayer1 = 1;
    [SerializeField] private int heatSeekingShotsPlayer2 = 1;
    [SerializeField] private Shooting player1, player2;
    private Shooting currentPlayer;
    

    void Start()
    {
        // Tilldela klickhändelser till knapparna
        snowShovelButton.onClick.AddListener(ToggleSnowShovel);
        snowballButton.onClick.AddListener(ToggleSnowball);
        heatseekButton.onClick.AddListener(ToggleHeatSeek);
        currentPlayer = player1;
    }

    public void ToggleWeaponMenu()
    {
        // Växlar aktivitetsstatus för vapenmenyn
        UpdateCurrentPlayer();
        weaponMenu.SetActive(!weaponMenu.activeSelf);
        SetHeatSeekButton();
    }

    // Sets the heat seeking button interactable if current player has enough shots.
    private void SetHeatSeekButton()
    {
        if (currentPlayer == player1 && heatSeekingShotsPlayer1 > 0)
        {
            heatseekButton.interactable = true;
        }
        else if (currentPlayer == player2 && heatSeekingShotsPlayer2 > 0)
        {
            heatseekButton.interactable = true;
        }
        else
        {
            heatseekButton.interactable = false;
        }
    }

    // Changes which player will have their shot updated.
    private void UpdateCurrentPlayer()
    {
        currentPlayer = GameController.currentState == GameController.BattleState.Player1ChooseWeapon ? player1 : player2;
    }

    public void ToggleSnowShovel()
    {
        currentPlayer.SetEquippedSnowball(Shooting.Snowballs.SnowShovel);
        
        HasChosenWeapon = true;
    }

    public void ToggleSnowball()
    {
        currentPlayer.SetEquippedSnowball(Shooting.Snowballs.Snowball);
        
        HasChosenWeapon = true;
    }

    public void ToggleHeatSeek()
    {
        DecrementHeatSeeking();

        currentPlayer.SetEquippedSnowball(Shooting.Snowballs.HeatSeeking);
        
        HasChosenWeapon = true;
    }

    // Removes one available shot from the player.
    private void DecrementHeatSeeking()
    {
        if (currentPlayer.Equals(player1))
        {
            heatSeekingShotsPlayer1--;
        }
        else
        {
            heatSeekingShotsPlayer2--;
        }
    }
}
