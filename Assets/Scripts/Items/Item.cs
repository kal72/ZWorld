using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public int Id;
    public string Name;
    public string Description;
    public Sprite Icon;
    public bool IsStackable;
    public ItemType itemType;
    public ItemGroup itemGroup;

    public enum ItemType
    {
        Potion,
        Weapon,
        Shield,
        Helmet,
        Armor,
        Accesories,
        Core,
        Material,
        Junk
    }

    public enum ItemGroup
    {
        Usable,
        Equip,
        Material,
        Other
    }

    public abstract void Equip(Character c);
    public abstract void Unequip(Character c);
}
