using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D bulletRB;
    [SerializeField] float bulletSpeed=15f;
    PlayerMovement playerRef;
    float XSpeed;
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        playerRef = FindObjectOfType<PlayerMovement>();
        XSpeed = playerRef.transform.localScale.x * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        bulletRB.velocity = new Vector2(XSpeed, 0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="enemy")
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        Destroy(gameObject);
    }
}
