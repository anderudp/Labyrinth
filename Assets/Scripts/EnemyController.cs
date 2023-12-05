using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

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
        if(MySelectable.allMySelectables.Count > 0) 
        {
            if(unit != null && Vector3.Distance(transform.position, unit.position) < 15f)
            {
                transform.LookAt(unit);
                if (Vector3.Distance(transform.position, unit.position) > 1.5f) transform.position += transform.forward * agent.speed * Time.deltaTime;
                anim.SetFloat("Running", agent.speed);
            }
            else unit = MySelectable.allMySelectables[Random.Range(0, MySelectable.allMySelectables.Count)].transform;
        }
        if(unit == null) anim.SetFloat("Running", 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "myUnit")
        {
            anim.SetBool("Attack", true);
            StartCoroutine(Attack(other));
        }
    }

    IEnumerator Attack(Collider collider)
    {
        while (collider != null && collider.GetComponent<UnitController>().Health > 0f)
        {
            transform.LookAt(unit);
            collider.GetComponent<UnitController>().Health -= Damage;
            yield return new WaitForSeconds(UnitController.delay * 1.5f);
        }
        anim.SetBool("Attack", false);
        if (collider != null)
        {
            Destroy(collider.gameObject, UnitController.delay);
            foreach(var ms in MySelectable.allMySelectables)
            {
                if(Vector3.Distance(transform.position, collider.transform.position) < 5f)
                {
                    MySelectable.allMySelectables.Remove(collider.gameObject.GetComponent<MySelectable>());
                    MySelectable.currentlySelected.Remove(collider.gameObject.GetComponent<MySelectable>());
                }
            }
        }
    }
}
