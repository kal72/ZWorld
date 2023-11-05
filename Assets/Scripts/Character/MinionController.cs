using UnityEngine;
using UnityEngine.AI;

public class MinionController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float attackRange;
    public bool enemyInAttackRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Vector3 pos = transform.position;
        pos.y = 1f;
        transform.position = pos;
    }

    private void Update()
    {
        if (target != null && !alreadyAttacked)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget <= attackRange)
            {
                // Within attack range, initiate attack.
                Attack();
            }
            else
            {
                // Move towards the target.
                Chase();
            }
        }
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void Chase()
    {
        agent.SetDestination(target.position);
    }

    private void Attack()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(target);

        if (!alreadyAttacked)
        {
            ///Attack code here
            var projectileObj = Instantiate(projectile, transform.position, Quaternion.identity);
            BulletProjection bullet = projectileObj.GetComponent<BulletProjection>();
            bullet.Attack = GetComponent<MonsterStat>().Attack;

            Rigidbody rb = projectileObj.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void FireProjectile()
    {
        GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        ProjectileController projectileScript = newProjectile.GetComponent<ProjectileController>();
        projectileScript.target = target;
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
