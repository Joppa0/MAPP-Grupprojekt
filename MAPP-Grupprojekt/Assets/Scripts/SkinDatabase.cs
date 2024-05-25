using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkinDatabase : ScriptableObject
{
    public Skin[] skin;

    public int SkinCount
    {
        //Returning the number of skins in the array
        get
        {
            return skin.Length;
        }
    }

    public Skin GetSkin(int index)
    {
        //Retrieve selected skin information
        return skin[index];
    }
}
