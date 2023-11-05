using UnityEngine;

public class BulletProjection : MonoBehaviour
{
    public int Attack;

    private void OnTriggerEnter(Collider other)
    {
       
    }

    private void OnCollisionEnter(Collision other)
    {
        var monsterStat = other.gameObject.GetComponent<MonsterStat>();
        if (monsterStat != null)
            monsterStat.TakeDamage(Attack);
    }

    private void OnCollisionExit(Collision collision)
    {
       //Destroy(gameObject);
    }
}
