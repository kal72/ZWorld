using UnityEngine;
using zw.CharacterStats;

public class Character : MonoBehaviour 
{

    public static Character Instance;
    private int HealthPoint;
    private int ManaPoint;
    private int StaminaPoint;
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
}
