using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 2)]
public class ItemDataTemp : ScriptableObject
{
    public GameObject prefabGFX;
    public GameObject uiPrefab;
    public GameObject Prefabitem;
    public Vector2 offset;
}
