﻿using System.Collections;
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
    private Animator animator;
    private bool movedPosition = false;
    private float initialPositionX;
    private float initialPositionY;
    private float initialPositionZ;
    private SpriteRenderer SpriteRend;
    private float timeStamp;

    void Start()
    {
        initialPositionX = transform.position.x;
        initialPositionY = transform.position.y;
        initialPositionZ = transform.position.z;
        rb = GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        SpriteRend = this.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

        if (timeStamp < Time.time)
        {
            SpriteRend.color = new Color(1F, 1F, 1F, 1F);
        }

        float distance = Vector3.Distance(transform.position, player.position);
        System.Diagnostics.Debug.WriteLine(distance);

        if ((distance < pullRange))
        {
            movedPosition = true;
            float z = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
            transform.eulerAngles = new Vector3(0, 0, z);
            rb.AddForce(gameObject.transform.up * speed);
            animator.SetInteger("Direction", 2);
            animator.SetBool("Move", true);
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
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerWeapon")
        {
            SpriteRend.color = new Color(255F, 0F, 0F, .75F);
            timeStamp = Time.time + .35F;
            health--;
            Destroy(collision.gameObject);
        }
    }
}
