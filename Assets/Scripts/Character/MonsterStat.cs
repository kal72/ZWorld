using UnityEngine;
using System.Collections;

public class MonsterStat : MonoBehaviour
{
	public float HealhPoint;
	public float MaxHealth;
	public float Attack;

	public void TakeDamage(float damage)
	{
		Debug.Log("damage = "+damage);
		HealhPoint -= damage;
		if (HealhPoint <= 0)
		{
			Destroy(gameObject);
		}
	}

	public float DealDamage()
	{
		return Attack;
	}
}

