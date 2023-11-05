using UnityEngine;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Item : ScriptableObject
{
    [SerializeField] string id;
	public string ID { get { return id; } }
    public string Name;
    public string Description;
    public Sprite Icon;
    public bool IsStackable;
    
    public ItemGroup itemGroup;
    [Range(1,999)]
	public int MaximumStacks = 1;

    public enum ItemGroup
    {
        Usable,
        Equip,
        Material,
        Other
    }

    protected static readonly StringBuilder sb = new StringBuilder();

	#if UNITY_EDITOR
	protected virtual void OnValidate()
	{
		string path = AssetDatabase.GetAssetPath(this);
		id = AssetDatabase.AssetPathToGUID(path);
	}
    #endif

    public virtual Item GetCopy()
    {
        return this;
    }

    public virtual void Destroy(){}

	public virtual string GetDescription(){
        return "";
    }

    public virtual string GetItemType(){
        return itemGroup.ToString();
    }
}
