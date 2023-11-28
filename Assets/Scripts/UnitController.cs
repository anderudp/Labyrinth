using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private RaycastHit hit;
    private MySelectable ms;

    public static float delay = 0.5f;

    private float health;
    public float Health { get { return health; } set { health = value; } }

    public float Damage {get; set;}

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        ms = GetComponent<MySelectable>();

        Health = 500;
        Damage = 100;
    }

    // Update is called once per frame
    void Update()
    {
        // Move selected units
        foreach(var soldier in MySelectable.allMySelectables)
        {
            if(soldier.selected)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    anim.SetFloat("Running", agent.remainingDistance);
                    if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                    {
                        agent.destination = hit.point;
                    }
                }
            }
            else anim.SetFloat("Running", 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            agent.destination = other.gameObject.transform.position;
            anim.SetBool("Attack", true);
            
        }
    }
}
