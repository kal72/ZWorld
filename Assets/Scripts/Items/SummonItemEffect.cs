using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using zw.CharacterStats;

[CreateAssetMenu(menuName = "Item Effects/Summon Minion")]
public class SummonItemEffect : UsableItemEffect
{
    public string MinionName;
    public GameObject MinionPrefab;
    public float Duration; //seconds

    public override void ExecuteEffect(UsableItem parentItem, Character character)
    {
        var minionObj = Instantiate(MinionPrefab, character.Player.transform.position, character.Player.transform.rotation);
        character.StartCoroutine(DestroyMinion(minionObj, Duration));
    }

    public override string GetDescription()
    {
        return "Summon minion " + MinionName + " for " + Duration + " seconds.";
    }

    private static IEnumerator DestroyMinion(GameObject minionObj, float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(minionObj);
    }
}
