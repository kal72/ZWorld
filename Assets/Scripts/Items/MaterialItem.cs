using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Material", menuName = "Item/Material")]
public class MaterialItem : Item
{
    public string MaterialType = "Material";
    public override void Destroy()
    {
        Destroy(this);
    }

    public override Item GetCopy()
    {
        return Instantiate(this);
    }

    public override string GetDescription()
    {
        return Description;
    }

    public override string GetItemType()
    {
        return MaterialType;
    }
}
