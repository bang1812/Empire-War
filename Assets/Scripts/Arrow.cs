using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] private Transform sprite;
    [SerializeField] private float movespeed;
    [SerializeField] private AnimationCurve Curve;
    [SerializeField] private AnimationCurve axisCorrectionCurve;
    private Vector3 spriteDir;
    Vector2 startpoint;
    Transform enemy;
    
    private float trajectoryMaxRelativeHeight;
    private void Update()
    {
        
        if (enemy != null)
        {
            UpdateCurve();
            Move();
            
        }
        
        
    }
    void UpdateCurve()
    {
        Vector2 trajectoryRange = (Vector2)enemy.position - startpoint;
        if (trajectoryRange.x < 0)
        {
            movespeed = -movespeed;
        }

        float nextPosX = transform.position.x + movespeed * Time.deltaTime;
        float nextPosXNormalized = (nextPosX - startpoint.x) / trajectoryRange.x;
        float nextPosYNormalized = Curve.Evaluate(nextPosXNormalized);
        float nextPosYCorrectionNormalized = axisCorrectionCurve.Evaluate(nextPosXNormalized);
        float nextPosYCorrectionAbsolute = nextPosYCorrectionNormalized * trajectoryRange.y;
        float nextPosY = startpoint.y + nextPosYNormalized * trajectoryMaxRelativeHeight + nextPosYCorrectionAbsolute;
        Vector3 newPos = new Vector3(nextPosX, nextPosY, 0);
        spriteDir = newPos - transform.position;
        
        transform.position = newPos;
    }
    void Move()
    {
        Vector3 moveDir = enemy.position - transform.position;
        if (moveDir.x < 0)
            movespeed = -movespeed;
        transform.position += moveDir.normalized * movespeed * Time.deltaTime;
        sprite.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(spriteDir.y, spriteDir.x) * Mathf.Rad2Deg);
        
    }
    public void SetTarget(Transform enemy)
    {
        this.enemy = enemy;
    }
    public void SetStartpoint(Vector2 Startpoint)
    {
        this.startpoint = Startpoint;
    }
    public void SetMaxHeight(float maxheight)
    {
        float xDistanceToTarget = enemy.position.x - transform.position.x;
        trajectoryMaxRelativeHeight = Mathf.Abs(xDistanceToTarget) * maxheight;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Enemy"))
            Destroy(gameObject);
    }
}
