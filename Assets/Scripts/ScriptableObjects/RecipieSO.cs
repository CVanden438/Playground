using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemRequirement
{
    public ItemSO item;
    public int quantity;
}

[CreateAssetMenu(fileName = "Recipie", menuName = "Recipie")]
public class RecipieSO : ScriptableObject
{
    public ItemSO outputItem;
    public List<ItemRequirement> inputItems;
}
