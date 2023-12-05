using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;
    public Transform PlayerTransform;
    public Transform EnemyTransform;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SummonEnemy());
        StartCoroutine(SummonPlayer());
    }

    IEnumerator SummonEnemy()
    {
        while(true)
        {
            yield return new WaitForSeconds(6.66f);
            Instantiate(EnemyPrefab, EnemyTransform.position, Quaternion.Euler(0f, 180f, 0f), EnemyTransform);
        }
    }

    IEnumerator SummonPlayer()
    {
        while(true)
        {
            Instantiate(PlayerPrefab, PlayerTransform.position, Quaternion.identity, PlayerTransform);
            yield return new WaitForSeconds(10f);
        }
    }
}
