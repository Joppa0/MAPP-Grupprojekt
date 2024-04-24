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
        Load();
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
        string playerPrefKey = $"selectedOption_{gameObject.name}";
        selectedOption = PlayerPrefs.GetInt(playerPrefKey, 0);
    }

    private void Save()
    {
        string playerPrefKey = $"selectedOption_{gameObject.name}";
        PlayerPrefs.SetInt(playerPrefKey, selectedOption);
    }

}
