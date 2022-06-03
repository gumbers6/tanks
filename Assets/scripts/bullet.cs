using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,3f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.up*speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            // get player
            Tank tank = collision.gameObject.GetComponent<Tank>();

            // hit player
            tank.Hit();
        }
    }
}
