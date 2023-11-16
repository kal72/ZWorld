using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using zw.CharacterStats;

public class Character : MonoBehaviour 
{

    public static Character Instance;

    public GameObject Player;

    [Header("Ability")]
    [SerializeField] private float healthPoint;
    public float HealthPoint
    {
        get { return healthPoint; }
        set { healthPoint = value; /* HealthChangeEvent.Publish(healthPoint); */ }
    }
    public float ManaPoint;
    public float StaminaPoint;
    public float CritDamage;
    public float CritRate;
    public int MaxSummon;
    public CharacterStat MaxHealth;
    public CharacterStat MaxStamina;
    public CharacterStat MaxMana;
    public CharacterStat Attack;
    public CharacterStat Defense;
    public CharacterStat ElementalResistance;
    public CharacterStat Speed;

    [Header("Event Channel")]
    public GameEvent<float> HealthChangeEvent;

    [Header("List")]
    [SerializeField] private List<MonsterController> summonedMinions = new List<MonsterController>();

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    public void UpdateStatValues()
    {
        //statPanel.UpdateStatValues();
    }

    public void TakeDamage(float _damage)
    {
        float result = HealthPoint - _damage;
        HealthPoint = result <= 0f ? 0f : result; 
    }

    public float DealDamage()
    {
        return Attack.Value;
    }

    public void AddSummon(MonsterController minion)
    {
        summonedMinions.Add(minion);
    }

    public void RemoveSummon(MonsterController minion)
    {
        summonedMinions.Remove(minion);
    }

    public void MinionLockTarget(Transform _target)
    {
        foreach(var minion in summonedMinions)
        {
            if (minion != null)
            {
                minion.target = _target;
            }
        }
    }
}
