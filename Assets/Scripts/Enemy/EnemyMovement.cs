using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private Transform[] points;
    [SerializeField] private int startPoint;
    [SerializeField] private Animator ani;
    [SerializeField] private SpriteRenderer spriteRenderer;


    HealthManager health;
    private int i = 1;
    private void Start()
    {
        health = GetComponent<HealthManager>();
        transform.position = points[startPoint].position;
        
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Update()
    {
        
        Rotate();
        
    }
    void Move()
    {
        if (rb.position == (Vector2)points[points.Count() - 1].position)
        {
            Destroy(gameObject);
            return;
        }
        if (Vector2.Distance(transform.position, points[i].position) < 0.01)
            i++;
        Vector2 newposition = Vector2.MoveTowards(rb.position, points[i].position, speed * Time.deltaTime);
        rb.MovePosition(newposition);        
    }
    void Rotate()
    {
        if (transform.position.x > points[i-1].position.x)
            spriteRenderer.flipX = true;
        else spriteRenderer.flipX = false;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Arrow"))
        {
            health.TakeDamage(20);
        }
    }
}
