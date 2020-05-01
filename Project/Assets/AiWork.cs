using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class AiWork : MonoBehaviour
{
    public Vector3 Destination;
    NavMeshAgent navMeshAgent;
    public ExtractionArea MyArea;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Destination, 50);
    }
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();


    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(Destination);
    //  navMeshAgent.SetDestination(MyArea.ExtractionGameObject.transform.position);
    }
}
