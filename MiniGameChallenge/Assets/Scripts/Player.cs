using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    Rigidbody2D body;
    float horizontal, vertical, timerToShoot;
    public float speed = 3, fireRate = 1;
    [Header("References")]
    public GameObject bullet; 
    public GameObject gun;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        timerToShoot = fireRate;
    }

    // Update is called once per frame
    private void Update()
    {
        timerToShoot += Time.deltaTime;
        Controls();
    }
    void FixedUpdate()
    {
        
    }
    void Controls()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        body.velocity = new Vector2(horizontal * speed, vertical * speed);

        
        if (Input.GetButtonDown("Fire1") && timerToShoot >= fireRate)
        {
            GameObject tempPrefab = Instantiate(bullet);
            tempPrefab.transform.position = gun.transform.position;
            timerToShoot = 0;
        }
    }
}
