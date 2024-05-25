using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChange : MonoBehaviour
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
    private void UpdateSkin(int selectedOption)
    {
        //Retrieving skin information from database
        Skin skin = skinDB.GetSkin(selectedOption);
        //Updataing sprite 
        artworkSprite.sprite = skin.skinSprite;
    }

    private void Load()
    {
        //Setting the value of selectedOption to the value that has been stored in the Key name
        string playerPrefKey = $"selectedOption_{gameObject.name}";
        selectedOption = PlayerPrefs.GetInt(playerPrefKey, 0);
        //Set to 0 if there is not any stored data
    }
}
