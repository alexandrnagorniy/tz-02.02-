using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    apple,
    banana,
    orange,
    pear,
    strawberry
}

[CreateAssetMenu(fileName = "New Item", menuName = "Create item", order = 51)]
public class ItemSO : ScriptableObject
{
    public ItemType type;
    public Sprite itemIcon;
    public GameObject prefab;
}