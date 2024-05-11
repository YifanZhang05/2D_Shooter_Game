using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector2 direction;
    
    protected Rigidbody2D rb;
    protected float bulletSpeed = 10;
    protected float bulletPower = 100;    // how much knockback when hit 

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = direction*bulletSpeed;
    }

    protected void removeBullet()
    {
        Destroy(gameObject);
    }
}
