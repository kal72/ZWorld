using UnityEngine;
using System.Collections;

public class MonsterStat : MonoBehaviour
{
	public int HealhPoint;
	public int MaxHealth;
	public int Attack;

	public void TakeDamage(int damage)
	{
		Debug.Log("damage = "+damage);
		HealhPoint -= damage;
		if (HealhPoint <= 0)
		{
			Destroy(gameObject);
		}
	}
}

