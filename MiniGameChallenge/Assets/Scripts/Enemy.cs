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
    Animator anim;
    SpriteRenderer spriteR;

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
        anim = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();
        StartCoroutine("RunningShooting", Attitudes.Shoot);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.state.Equals(Mode.WAIT))
        {
            attitude = Attitudes.Wait;
        }
        switch (attitude)
        {
            case Attitudes.Wait:
                anim.SetBool("running", false);
                agent.SetDestination(transform.position);
                StopCoroutine("RunningShooting");
                break;
            case Attitudes.Run:
                anim.SetBool("running", true);
                agent.SetDestination(player.transform.position);
                if(agent.velocity.x > 0)
                {
                    spriteR.flipX = true;
                } else if(agent.velocity.x < 0)
                {
                    spriteR.flipX = false;
                }
                
                break;
            case Attitudes.Shoot:
                anim.SetBool("running", false);
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
            gameObject.SetActive(false);
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
    public void StartCreature()
    {
        attitude = Attitudes.Run;
        life = 100;
        StartCoroutine("RunningShooting", Attitudes.Shoot);
    }
}
