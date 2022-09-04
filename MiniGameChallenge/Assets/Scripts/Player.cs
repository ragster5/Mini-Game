using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    Rigidbody2D body;
    GameController gc;
    Animator anim;
    Vector3 mousePos;

    float horizontal, vertical, timerToShoot;
    public float speed = 3, fireRate = 1;

    [Header("References")]
    public GameObject bullet; 
    public GameObject gun;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gc = FindObjectOfType(typeof(GameController)) as GameController;
        body = GetComponent<Rigidbody2D>();
        timerToShoot = fireRate;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameController.state.Equals(Mode.PLAY))
        {
            Controls();
            Art();
        }
        else
        {
            body.velocity = Vector2.zero;
        }
    }
    void Controls()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        body.velocity = new Vector2(horizontal * speed, vertical * speed);

        timerToShoot += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && timerToShoot >= fireRate)
        {
            GameObject tempPrefab = Instantiate(bullet);
            tempPrefab.transform.position = gun.transform.position;
            timerToShoot = 0;
        }

        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
    }
    void Art()
    {
        anim.SetBool("running", horizontal != 0 || vertical != 0);
        if (mousePos.x > 0)
        {
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            }
        }
        else if (mousePos.x < 0)
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        gc.playerLife -= damage;
        if (gc.playerLife <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
