using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Moving());
    }

    bool GetDistance(Vector3 target) 
    {
        return Vector3.Distance(transform.position, target) < 0.1f;
    }

    IEnumerator Moving() 
    {
        Vector3 target = Vector3.one * Random.Range(-2f, 2f);
        yield return new WaitForSeconds(Random.Range(0.1f, 3f));
        agent.SetDestination(transform.position + target);
        anim.SetBool("Walking", true);
        yield return new WaitUntil(() => GetDistance(target) == true);
        
        anim.SetBool("Walking", false);
        yield return new WaitForSeconds(Random.Range(0.1f, 3f));
        StartCoroutine(Moving());
        // yield return new WaitUntil(Vector3.Distance(transform.position, target) > 0.1f);
    }
}