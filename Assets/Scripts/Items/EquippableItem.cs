using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using zw.CharacterStats;

public enum EquipmentType
    {
        // Potion,
        Weapon,
        Shield,
        Helmet,
        Armor,
        Accesories,
        // Core,
        // Material,
        // Junk
    }

[CreateAssetMenu(fileName = "Equipment", menuName = "Item/Equipment")]
public class EquippableItem : Item
{
    public EquipmentType EquipmentType;
    public int ATKBonus;
    public int DEFBonus;
    public int ManaBonus;
    public int HealthBonus;
    [Space]
    public float ATKPercentBonus;
    public float DEFPercentBonus;
    public float ManaPercentBonus;
    public float HealthPercentBonus;
    public float CritDamagePercentBonus;
    public float CritRatePercentBonus;
    public float ElementalResPercentBonus;

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
        sb.Length = 0;

        AddStat(ATKBonus, "ATK");
        AddStat(DEFBonus, "DEF");
        AddStat(ManaPercentBonus, "Max Mana");
        AddStat(HealthBonus, "Max HP");

        AddStat(ATKBonus, "ATK", isPercent: true);
        AddStat(DEFBonus, "DEF", isPercent: true);
        AddStat(ManaPercentBonus, "Max Mana", isPercent: true);
        AddStat(HealthBonus, "Max HP", isPercent: true);
        AddStat(CritDamagePercentBonus, "Crit DMG", isPercent: true);
        AddStat(CritRatePercentBonus, "Crit Rate", isPercent: true);
        AddStat(ElementalResPercentBonus, "Elemental RES", isPercent: true);

        return sb.ToString();
    }

    public override string GetItemType()
    {
        return EquipmentType.ToString();
    }

    public void Equip(Character c)
    {
        if (ATKBonus != 0)
            c.Attack.AddModifier(new StatModifier(ATKBonus, StatModType.Flat, this));
        if (DEFBonus != 0)
            c.Defense.AddModifier(new StatModifier(DEFBonus, StatModType.Flat, this));
        if (ManaBonus != 0)
            c.MaxMana.AddModifier(new StatModifier(ManaBonus, StatModType.Flat, this));
        if (HealthBonus != 0)
            c.MaxHealth.AddModifier(new StatModifier(HealthBonus, StatModType.Flat, this));

        if (ATKPercentBonus != 0)
            c.Attack.AddModifier(new StatModifier(ATKPercentBonus, StatModType.PercentMult, this));
        if (DEFPercentBonus != 0)
            c.Defense.AddModifier(new StatModifier(DEFPercentBonus, StatModType.PercentMult, this));
        if (ManaPercentBonus != 0)
            c.MaxMana.AddModifier(new StatModifier(ManaPercentBonus, StatModType.PercentMult, this));
        if (HealthPercentBonus != 0)
            c.MaxHealth.AddModifier(new StatModifier(HealthPercentBonus, StatModType.PercentMult, this));
        if (CritDamagePercentBonus != 0)
            c.CritDamage += CritDamagePercentBonus;
        if (CritRatePercentBonus != 0)
            c.CritRate += CritRatePercentBonus;

    }

    public void Unequip(Character c)
    {
        c.Attack.RemoveAllModifiersFromSource(this);
        c.Defense.RemoveAllModifiersFromSource(this);
        c.MaxMana.RemoveAllModifiersFromSource(this);
        c.MaxHealth.RemoveAllModifiersFromSource(this);
        c.CritDamage -= CritDamagePercentBonus;
        c.CritRate -= CritRatePercentBonus;
    }

    private void AddStat(float value, string statName, bool isPercent = false)
    {
        if (value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (value > 0)
                sb.Append("<color=yellow>+</color>");

            if (isPercent)
            {
                sb.Append(string.Format("<color=yellow>{0}</color>", value));
                sb.Append("<color=yellow>%</color> ");
            }
            else
            {
                sb.Append(string.Format("<color=yellow>{0}</color>", value));
                sb.Append(" ");
            }
            sb.Append(statName);
        }
    }
}
