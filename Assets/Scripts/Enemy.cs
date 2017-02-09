using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (UnityEngine.AI.NavMeshAgent))]
public class Enemy : MonoBehaviour {

    UnityEngine.AI.NavMeshAgent pathfinder;
    Transform target;

	void Start () {
        pathfinder = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(UpdatePath ());
	}
	
	void Update () {
 	}
    IEnumerator UpdatePath()
    {
        float refreshRate = .25f;
        while (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
            pathfinder.SetDestination(targetPosition);
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
