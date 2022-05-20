using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Rigidbody2D rb;
    float xVelocity = 3f;
    float yVelocity = 0f;
    int direction = 1;
    float turnTime = 0.0f;

    private Transform guns;
    public GameObject leftGun;
    public GameObject rightGun;
    public GameObject laserPrefab;
    private GameController gameController;

    private Vector3 screenPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        direction = gameController.GetEnemyDirection();
        rb.velocity = new Vector2(xVelocity * direction, yVelocity);
        screenPos = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPos.x < 0 || screenPos.x > Screen.width)
        {
            gameController.SetEnemyDirection();
            turnTime = Time.time + 2.0f;
        }
        FireLaser();
    }

    private void FireLaser()
    {
        // Randomize Shooting
        int rand = Random.Range(0, 1560);

        if (rand > 1557)
        {
            if (rand == 1558)
            {
                guns = rightGun.transform;
            }
            else
            {
                guns = leftGun.transform;
            }
            Instantiate(laserPrefab, guns.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            this.GetComponent<Animator>().Play("Explode");
        }
    }
}
