using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour, ICharacter
{
    public NavMeshAgent agent;
    public Transform target;
    public LayerMask whatIsGround, whatIsPlayer, whatIsEnemy;
    public Animator anim;
    private MonsterStat monsterStat;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool targetInSightRange, targetInAttackRange;

    public bool isActivated;
    public bool IsMinion;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (anim == null)
            anim = GetComponentInChildren<Animator>();
        if (monsterStat == null)
            monsterStat = GetComponent<MonsterStat>();
    }

    private void Update()
    {
        if (!isActivated) return;

        //Check for sight and attack range
        if (!IsMinion)
            targetInSightRange = detectTarget();

        targetInAttackRange = detectRange();

        if (!targetInSightRange && !targetInAttackRange && !IsMinion) Patroling();
        if ((targetInSightRange || IsMinion) && !targetInAttackRange) ChaseTarget();
        if ((targetInSightRange || IsMinion) && targetInAttackRange && target != null) AttackTarget();
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            walkAnim();
            agent.SetDestination(walkPoint);
        }   

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChaseTarget()
    {
        if (target == null) return;

        runAnim();
        agent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        //Make sure enemy doesn't move
        if (Vector3.Distance(transform.position, target.position) <= (attackRange - 0.5f))
            agent.SetDestination(transform.position);

        transform.LookAt(target);

        if (!alreadyAttacked)
        {
            // Attack code here
            attackAnim();
            var targetChar = target.GetComponent<ICharacter>();
            if (targetChar != null) targetChar.TakeDamage(monsterStat.DealDamage(), gameObject);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void walkAnim()
    {
        anim.SetBool("Walk", true);
    }

    private void runAnim()
    {
        anim.SetBool("Attack", false);
        anim.SetBool("Walk", false);
        anim.SetBool("Run", true);
    }

    private void attackAnim()
    {
        anim.SetBool("Run", false);
        anim.SetBool("Attack", true);
    }

    private void idle() {
        anim.SetBool("Walk", false);
        anim.SetBool("Run", false);
        anim.SetBool("Attack", false);
    }

    private bool detectTarget()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, sightRange, whatIsPlayer);
        if (collider.Length != 0)
        {
            target = collider[0].transform;
            return true;
        }

        return false;
    }

    private bool detectRange()
    {
        if (!IsMinion)
            return Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        Collider[] collider = Physics.OverlapSphere(transform.position, attackRange, whatIsEnemy);
        for (int i=0; i<collider.Length; i++)
        {
            if (collider[i].transform == target)
            {
                return true;
            }
        }

        return false;
    }

    public void TakeDamage(float _damage, GameObject _attacker)
    {
        monsterStat.TakeDamage(_damage);
        if (!IsMinion)
            target = _attacker.transform;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
