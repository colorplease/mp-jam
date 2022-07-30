using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 10;
    Vector2 dir;
    public KeyCode shoot;
    [SerializeField]Transform firePoint;
    [SerializeField]GameObject bulletPrefab;
    float inverseScale;
    float normalScale;
    bool isFacingRight;
    float x;
    public InputAction playerControls;

    void OnEnable()
    {
        playerControls.Enable();
    }

    void OnDisable()
    {
        playerControls.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inverseScale = -transform.localScale.x;
        normalScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        dir = new Vector2(x,y);
        Flip();
        if(Input.GetKeyDown(shoot))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        Walk(dir);
    }

    void Walk(Vector2 dir)
    {
        rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
    }

    void Flip()
    {
        Debug.Log(rb.velocity.x);
        if(rb.velocity.x > 0.1f || rb.velocity.x < -0.1f)
        {
            if(x > 0)
        {
            isFacingRight = true;
            if(transform.localScale.x != inverseScale)
            {
                transform.localScale = new Vector2(inverseScale, transform.localScale.y);
            }
        }
        if(x < 0.1f)
        {
            isFacingRight = false;
             if(transform.localScale.x != normalScale)
            {
                transform.localScale = new Vector2(normalScale, transform.localScale.y);
            }
        }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().isFacingRight = isFacingRight;
    }
}
