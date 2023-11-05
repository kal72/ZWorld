using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using zw.CharacterStats;

public class Character : MonoBehaviour 
{

    public static Character Instance;

    public GameObject Player;

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
    public int MaxSummon;
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

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            SummonMinion();
        }
    }

    public void UpdateStatValues()
    {
        //statPanel.UpdateStatValues();
    }

    // cuman ngetest
    public GameObject minionPrefab;
    private List<GameObject> summonedMinions = new List<GameObject>();

    public void SummonMinion() {
        Vector3 spawnPosition = Player.transform.position + Player.transform.forward * 2.0f; // Adjust the spawn position as needed.
        GameObject summonedEnemy = Instantiate(minionPrefab, spawnPosition, Quaternion.identity);
        summonedMinions.Add(summonedEnemy);
    }

    public void MinionLockTarget(Transform _target)
    {
        foreach(var minion in summonedMinions)
        {
            if (minion != null)
            {
                var minionControl = minion.GetComponent<MinionController>();
                minionControl.SetTarget(_target);
            }
        }
    }
}
