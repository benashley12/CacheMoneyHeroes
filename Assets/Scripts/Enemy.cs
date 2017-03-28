using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Enemy : MonoBehaviour
{

    public float health;
    public float speed;
    public float pullRange;
    public Transform player;
    public Rigidbody2D rb;
    private bool movedPosition = false;
    private float initialPositionX;
    private float initialPositionY;
    private float initialPositionZ;

    void Start()
    {
        initialPositionX = transform.position.x;
        initialPositionY = transform.position.y;
        initialPositionZ = transform.position.z;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (health == 0)
        {
            Destroy(this.gameObject);
        }

        float distance = Vector3.Distance(transform.position, player.position);
        System.Diagnostics.Debug.WriteLine(distance);

        if ((distance < pullRange))
        {
            movedPosition = true;
            float z = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
            transform.eulerAngles = new Vector3(0, 0, z);
            rb.AddForce(gameObject.transform.up * speed);
        }

        else if (movedPosition)
        {
            float z = Mathf.Atan2((initialPositionY - transform.position.y), (initialPositionX - transform.position.x)) * Mathf.Rad2Deg - 90;
            transform.eulerAngles = new Vector3(0, 0, z);
            rb.AddForce(gameObject.transform.up * speed);
            if (((initialPositionX > transform.position.x - 1) && (initialPositionX < transform.position.x + 1)) &&
                ((initialPositionY > transform.position.y - 1) && (initialPositionY < transform.position.y + 1)))
            {
                movedPosition = false;
                // Re-align sprites upright.
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyWeapon")
        {
            health--;
        }
    }
}
