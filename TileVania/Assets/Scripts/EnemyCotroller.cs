using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCotroller : MonoBehaviour
{
    float moveSpeed = 1f;
    Rigidbody2D enemyRigid;

    void Start()
    {
        enemyRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        enemyRigid.velocity = new Vector2(moveSpeed, 0f);
    }
    void flip()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRigid.velocity.x)), 1f);
    }    
    private void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;
        flip();
    }
}
