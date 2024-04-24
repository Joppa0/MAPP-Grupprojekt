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
        Skin skin = skinDB.GetSkin(selectedOption);
        artworkSprite.sprite = skin.skinSprite;
    }

    private void Load()
    {
        string playerPrefKey = $"selectedOption_{gameObject.name}";
        selectedOption = PlayerPrefs.GetInt(playerPrefKey, 0);
    }
}
