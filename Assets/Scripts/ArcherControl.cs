using UnityEngine;
using System.Collections;

public class ArcherController : MonoBehaviour {

    private Animator animator;
    public Rigidbody2D rb2d;
    public GameObject ArrowPrefab;
    public Transform ArrowSpawn;

    // Player Stats
    public float speed = 1F;
    public int health = 3;
    public int focus = 3;

    // Firing Speed
    public int shotSpeed = 12000;
    public float fireDelay = 0.25F;
    private float nextFire = 0.25F;
    private float myTime = 0.0F;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");
        myTime = myTime + Time.deltaTime;

        // Controls Movement
        if (Input.GetKey("up"))
        {
            animator.SetInteger("Direction", 2);
            animator.SetBool("Move", true);
        }
        else if (Input.GetKey("down"))
        {
            animator.SetInteger("Direction", 0);
            animator.SetBool("Move", true);
        }
        else if (Input.GetKey("right"))
        {
            animator.SetInteger("Direction", 3);
            animator.SetBool("Move", true);
        }
        else if (Input.GetKey("left"))
        {
            animator.SetInteger("Direction", 1);
            animator.SetBool("Move", true);
        }
        else if (!Input.anyKey)
        {
            animator.SetBool("Move", false);
        }

        // Controls Attack and FireRate
        if (Input.GetKeyDown(KeyCode.Space) && myTime > nextFire)
        {
            // Need to fix the fire rate
            animator.SetTrigger("Attack");
            nextFire = myTime + fireDelay;
            Invoke("Fire", 0.7692308F/2);
            nextFire = nextFire - myTime;
            myTime = 0.0F;
        }

        // Move the player
        transform.Translate(horizontal * speed, vertical * speed, 0);
    }

    void Fire()
    {
        if (animator.GetInteger("Direction") == 0)
        {
            var arrow = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, Quaternion.Euler(0, 0, 90));
            arrow.GetComponent<Rigidbody>().AddForce(arrow.transform.right * -1 * shotSpeed);
            Destroy(arrow, 3.0f);
        }
        else if (animator.GetInteger("Direction") == 1)
        {
            var arrow = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, ArrowSpawn.rotation);
            arrow.GetComponent<Rigidbody>().AddForce(arrow.transform.right * -1 * shotSpeed);
            Destroy(arrow, 3.0f);
        }
        else if (animator.GetInteger("Direction") == 2)
        {
            var arrow = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, Quaternion.Euler(0, 0, 270));
            arrow.GetComponent<Rigidbody>().AddForce(arrow.transform.right * -1 * shotSpeed);
            Destroy(arrow, 3.0f);
        }
        else if (animator.GetInteger("Direction") == 3)
        {
            var arrow = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, Quaternion.Euler(0, 180, 0));
            arrow.GetComponent<Rigidbody>().AddForce(arrow.transform.right * -1 * shotSpeed);
            Destroy(arrow, 3.0f);
        }
    }
}
