using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

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

        //Check if selectedOption has reached the total amount of characters there is in the database
        if(selectedOption >= skinDB.SkinCount)
        {
            //Cycle throungh database, back to 0
            selectedOption = 0;
        }

        UpdateSkin(selectedOption);
        Save();
    }

    public void BackOption()
    {
        selectedOption--;
        //Check if selectedOption reached under 0
        if(selectedOption < 0) { 
            //Cycle through database, to highest index
            selectedOption = skinDB.SkinCount - 1;
        }
        UpdateSkin(selectedOption);
        Save();
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

    private void Save()
    {
        //Storing selectedOption varible in a Key name
        string playerPrefKey = $"selectedOption_{gameObject.name}";
        PlayerPrefs.SetInt(playerPrefKey, selectedOption);
    }

}
