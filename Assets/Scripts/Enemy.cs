using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (UnityEngine.AI.NavMeshAgent))]
public class Enemy : LivingEntity {

    public enum State {Idle, Chasing, Attacking};
    State currentState;

    UnityEngine.AI.NavMeshAgent pathfinder;
    Transform target;

    float attackDistanceThreshold = 1.5f;
    float timeBetweenAttacks = 1;

    float nextAttackTime;

	protected override void Start () {
        base.Start();
        pathfinder = GetComponent<UnityEngine.AI.NavMeshAgent>();

        currentState = State.Chasing;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(UpdatePath());
	}

    void Update()
    {
        if (Time.time > nextAttackTime)
        {
            float sqrDistanceToTarget = (target.position - transform.position).sqrMagnitude;
            if (sqrDistanceToTarget < Mathf.Pow(attackDistanceThreshold, 2))
            {
                nextAttackTime = Time.time + timeBetweenAttacks;
                StartCoroutine(Attack());
            }
        }
    }
    
    IEnumerator Attack() {

            currentState = State.Attacking;
            pathfinder.enabled = false;

            Vector3 originalPosition = transform.position;
            Vector3 attackPosition = target.position;

            float attackSpeed = 3;
            float percent = 0;

            while (percent <= 1)
            {
                percent += Time.deltaTime + attackSpeed;
                float interpolation = (-percent * percent + percent) * 4;
                transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

                yield return null;
            }

            currentState = State.Chasing;
            pathfinder.enabled = true;
        }

    IEnumerator UpdatePath()
    {
        float refreshRate = .25f;
        while (target != null)
        {
                if (currentState == State.Chasing)
                {
                    Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
                    if (!dead)
                    {
                        pathfinder.SetDestination(targetPosition);
                    }
                }
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
