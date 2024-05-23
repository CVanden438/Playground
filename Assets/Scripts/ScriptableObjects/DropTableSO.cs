using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LootDrop
{
    public ItemSO item;
    public int chance;
}

[CreateAssetMenu(fileName = "DropTable", menuName = "DropTable")]
public class DropTableSO : ScriptableObject
{
    public List<LootDrop> drops;
}
