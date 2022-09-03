using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    public int life = 100;
    public float fireRate = 2;
    float timerToShoot;
    GameObject player;

    [Header("References")]
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
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
}
