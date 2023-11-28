using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Transform unit;
    private NavMeshAgent agent;
    private Animator anim;

    public float Health { get; set; }
    public float Damage { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        Health = 500;
        Damage = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
