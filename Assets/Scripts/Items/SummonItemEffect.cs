using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Summon Minion")]
public class SummonItemEffect : UsableItemEffect
{
    public string MinionName;
    public GameObject MinionPrefab;
    public float Duration; //seconds

    public override void ExecuteEffect(Item parentItem, Character character)
    {
        var minionObj = Instantiate(MinionPrefab, character.Player.transform.position, character.Player.transform.rotation);
        minionObj.layer = LayerMask.NameToLayer("WhatIsPlayer");
        var control = minionObj.GetComponent<MonsterController>();
        control.IsMinion = true;
        control.isActivated = true;
        character.AddSummon(minionObj);
        character.StartCoroutine(DestroyMinion(minionObj, Duration));
    }

    public override string GetDescription()
    {
        return "Summon " + MinionName + " for " + Duration + " seconds.";
    }

    private static IEnumerator DestroyMinion(GameObject minionObj, float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(minionObj);
    }
}
