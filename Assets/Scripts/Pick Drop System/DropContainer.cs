using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropContainer : ItemContainer
{
    public override bool CanAddItem(Item _item, int _amount = 1)
    {
        return true;
    }
}
