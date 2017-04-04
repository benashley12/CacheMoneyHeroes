using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfRadius : MonoBehaviour {


    // Use this for initialization
    void Start () {


    }

    // Update is called once per frame
    void Update () {
		
	}


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyMelee")
        {
            Physics2D.IgnoreLayerCollision(collision.gameObject.layer, this.gameObject.layer);
            var playerWolf = GetComponentInParent<PlayerWolf>();
            playerWolf.enemyTarget = collision.gameObject.transform;
        }


    }
}
