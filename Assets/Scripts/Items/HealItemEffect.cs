using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Item Effects/Heal")]
public class HealItemEffect : UsableItemEffect
{
    public int HealAmount;

    public override void ExecuteEffect(UsableItem parentItem, Character character)
    {
        character.HealthPoint += HealAmount;
    }

    public override string GetDescription()
    {
        return "Heals for " + HealAmount + " health.";
    }
}

