using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum Attitudes
{
    Wait, Run, Shoot
}
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public int life = 100;
    public float fireRate = 2, timeRunning = 2;

    float timerToShoot;
    GameObject player;
    NavMeshAgent agent;
    Attitudes attitude;

    [Header("References")]
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        attitude = Attitudes.Run;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("RunningShooting", Attitudes.Shoot);
    }

    // Update is called once per frame
    void Update()
    {
        switch (attitude)
        {
            case Attitudes.Wait:

                break;
            case Attitudes.Run:
                agent.SetDestination(player.transform.position);
                break;

            case Attitudes.Shoot:
                agent.SetDestination(transform.position);
                Shoot();
                break;
        }
        
    }
    void Shoot()
    {
        if (player.activeSelf)
        {
            timerToShoot += Time.deltaTime;
            if (timerToShoot > fireRate)
            {
                GameObject tempPrefab = Instantiate(bullet);
                tempPrefab.transform.position = transform.position;
                timerToShoot = 0;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        life -= damage;
        if(life <= 0)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator RunningShooting(Attitudes change)
    {
        yield return new WaitForSeconds(timeRunning);
        attitude = change;
        if (attitude == Attitudes.Shoot)
        {
            StartCoroutine("RunningShooting", Attitudes.Run);
        } else if(attitude == Attitudes.Run)
        {
            StartCoroutine("RunningShooting", Attitudes.Shoot);
        }
    }
}
