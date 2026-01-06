using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public static void SaveFloat(float value, string returnString, GameObject thisgameobject)
    {
        PlayerPrefs.SetFloat(returnString + thisgameobject.GetHashCode().ToString(), value);
    }
    public static float GetFloat(string returningString, GameObject thisGameobject)
    {
        return PlayerPrefs.GetFloat(returningString + thisGameobject.GetHashCode().ToString());
    }
}
