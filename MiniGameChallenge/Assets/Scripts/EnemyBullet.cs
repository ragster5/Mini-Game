using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class EnemyBullet : MonoBehaviour
{
    Rigidbody2D body;
    Vector3 player;
    public int speed = 450, damage = 20;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 direction = (player - transform.position).normalized;
        if (Vector3.Dot(body.velocity, direction) < 0)
        {
            return;
        }
        body.AddForce(body.mass * (speed * direction));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
