using UnityEngine;
using zw.CharacterStats;

public class Character : MonoBehaviour 
{

    public static Character Instance;

    [Header("Ability")]
    [SerializeField] private int healthPoint;
    public int HealthPoint
    {
        get { return healthPoint; }
        set { healthPoint = value; HealthChangeEvent.Publish(healthPoint); }
    }
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

    [Header("Event Channel")]
    public GameEvent<int> HealthChangeEvent;

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
