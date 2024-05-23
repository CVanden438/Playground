using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class CSVtoSO
{
    private static readonly string meleeWepCSVPath = "/Editor/CSVs/MeleeWeps.csv";
    private static readonly string rangeWepCSVPath = "/Editor/CSVs/RangeWeps.csv";
    private static readonly string magicWepCSVPath = "/Editor/CSVs/MagicWeps.csv";
    private static readonly string enemiesCSVPath = "/Editor/CSVs/Enemies.csv";

    [MenuItem("Utilities/Generate Melee")]
    public static void GenerateMeleeWeps()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + meleeWepCSVPath);
        foreach (string s in allLines.Skip(1))
        {
            string[] splitData = s.Split(",");
            string name = splitData[0];
            string damage = splitData[1];
            string range = splitData[2];
            string cooldown = splitData[3];
            string repeat = splitData[4];

            string assetPath = $"Assets/Data/Weapons/Melee/{name}.asset";
            WeaponSO item = AssetDatabase.LoadAssetAtPath<WeaponSO>(assetPath);

            if (item == null)
            {
                // Create a new scriptable object if it doesn't exist
                item = ScriptableObject.CreateInstance<WeaponSO>();
                AssetDatabase.CreateAsset(item, assetPath);
            }

            // Update the scriptable object's properties
            item.itemName = name;
            item.damage = float.Parse(damage);
            item.range = float.Parse(range);
            item.cooldown = float.Parse(cooldown);
            item.repeat = int.Parse(repeat);
            item.itemType = ItemType.equipment;
            item.equipmentType = EquipmentType.weapon;
            // item.cost = itemCost;

            // Mark the scriptable object as dirty to trigger serialization
            EditorUtility.SetDirty(item);
        }
        AssetDatabase.SaveAssets();
    }

    [MenuItem("Utilities/Generate Range")]
    public static void GenerateRangeWeps()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + rangeWepCSVPath);
        foreach (string s in allLines.Skip(1))
        {
            string[] splitData = s.Split(",");
            string name = splitData[0];
            string damage = splitData[1];
            string range = splitData[2];
            string cooldown = splitData[3];
            string repeat = splitData[4];
            string proj = splitData[5];

            string assetPath = $"Assets/Data/Weapons/Range/{name}.asset";
            WeaponSO item = AssetDatabase.LoadAssetAtPath<WeaponSO>(assetPath);

            if (item == null)
            {
                // Create a new scriptable object if it doesn't exist
                item = ScriptableObject.CreateInstance<WeaponSO>();
                AssetDatabase.CreateAsset(item, assetPath);
            }

            // Update the scriptable object's properties
            item.itemName = name;
            item.damage = float.Parse(damage);
            item.range = float.Parse(range);
            item.cooldown = float.Parse(cooldown);
            item.repeat = int.Parse(repeat);
            item.itemType = ItemType.equipment;
            item.equipmentType = EquipmentType.weapon;
            item.proj = int.Parse(proj);
            // item.cost = itemCost;

            // Mark the scriptable object as dirty to trigger serialization
            EditorUtility.SetDirty(item);
        }
        AssetDatabase.SaveAssets();
    }

    [MenuItem("Utilities/Generate Enemies")]
    public static void GenerateEnemies()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + enemiesCSVPath);
        foreach (string s in allLines.Skip(1))
        {
            string[] splitData = s.Split(",");
            string name = splitData[0];
            string health = splitData[1];
            string mvoespeed = splitData[2];
            string waitDuration = splitData[3];
            string damage = splitData[4];

            string assetPath = $"Assets/Data/Enemies/{name}.asset";
            EnemySO enemy = AssetDatabase.LoadAssetAtPath<EnemySO>(assetPath);

            if (enemy == null)
            {
                // Create a new scriptable object if it doesn't exist
                enemy = ScriptableObject.CreateInstance<EnemySO>();
                AssetDatabase.CreateAsset(enemy, assetPath);
            }

            // Update the scriptable object's properties
            enemy.enemyName = name;
            enemy.damage = float.Parse(damage);
            enemy.health = int.Parse(health);
            enemy.moveSpeed = float.Parse(mvoespeed);
            enemy.waitDuration = float.Parse(waitDuration);
            // Mark the scriptable object as dirty to trigger serialization
            EditorUtility.SetDirty(enemy);
        }
        AssetDatabase.SaveAssets();
    }
}
