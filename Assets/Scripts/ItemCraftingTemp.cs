using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Crafting Recipe", menuName = "ScriptableObjects/CraftingRecipe", order = 1)]
public class ItemCraftingTemp : ScriptableObject
{
    public ItemDataTemp item1;
    public ItemDataTemp item2;
    public ItemDataTemp itemproduced;
}
