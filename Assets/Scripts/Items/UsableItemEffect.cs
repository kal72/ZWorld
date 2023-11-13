using UnityEngine;

public abstract class UsableItemEffect : ScriptableObject
{
	public abstract void ExecuteEffect(Item parentItem, Character character);
    public abstract string GetDescription();
}

