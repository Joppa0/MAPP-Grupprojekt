using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class SkinManager : MonoBehaviour
{
    public SkinDatabase skinDB;

    public SpriteRenderer artworkSprite;

    private int selectedOption = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            Load();
        }

        UpdateSkin(selectedOption);
    }

    public void NextOption()
    {
        selectedOption++;
        if(selectedOption >= skinDB.SkinCount)
        {
            selectedOption = 0;
        }

        UpdateSkin(selectedOption);
        Save();
    }

    public void BackOption()
    {
        selectedOption--;
        if(selectedOption < 0) { 
            selectedOption = skinDB.SkinCount - 1;
        }
        UpdateSkin(selectedOption);
        Save();
    }

    private void UpdateSkin(int selectedOption)
    {
        Skin skin = skinDB.GetSkin(selectedOption);
        artworkSprite.sprite = skin.skinSprite;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }

}
