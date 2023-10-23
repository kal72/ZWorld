using UnityEngine;
using zw.CharacterStats;

public class Character : MonoBehaviour 
{

    public static Character Instance;
    public int HealthPoint;
    public int ManaPoint;
    public int StaminaPoint;
    public float CritDamage;
    public float CritRate;
    public CharacterStat MaxHealth;
    public CharacterStat MaxStamina;
    public CharacterStat MaxMana;
    public CharacterStat Attack;
    public CharacterStat Defense;
    public CharacterStat ElementalResistance;
    public CharacterStat Speed;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    public void UpdateStatValues()
    {
        //statPanel.UpdateStatValues();
    }
}
