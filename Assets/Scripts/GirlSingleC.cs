using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
public class GirlSingleC : MonoBehaviour
{
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] GameObject girl;

    [SerializeField] Transform barPosition;
    CollisionPlayer collision;
    Animator anim;
    void Start()
    {
        collision = GameObject.FindGameObjectWithTag("Player").GetComponent<CollisionPlayer>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (collision.IsFinished)
        {
            navMeshAgent.SetDestination(barPosition.position);
            anim.SetFloat("Speed", navMeshAgent.speed);
        }
        
        if(Vector3.Distance(girl.transform.position, barPosition.position) < 0.5f)
        {
            anim.SetBool("Sitting", true);
            navMeshAgent.speed = 0;
            navMeshAgent.transform.DOMove(barPosition.position, 0.3f);
        }
    }

}
