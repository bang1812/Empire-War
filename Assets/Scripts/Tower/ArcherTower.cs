using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : MonoBehaviour
{
    public List<Transform> listEnemy = new List<Transform>();
    public Transform shotPoint;
    [SerializeField] private float shootrate;
    private float shoottime;
    Transform CurrentTarget;
    
    public GameObject arrow;
    public float trajectoryMaxHeight;

    private void Update()
    {
        shoottime -= Time.deltaTime;
        if(shoottime <= 0 && listEnemy.Count > 0)
        {
            shoottime = shootrate;
            Shoot();
        }
    }
    void Shoot()
    {
        Arrow newarrow = Instantiate(arrow, shotPoint.position, Quaternion.identity).GetComponent<Arrow>();
        newarrow.SetTarget(CurrentTarget);
        newarrow.SetStartpoint((Vector2)shotPoint.position);
        newarrow.SetMaxHeight(trajectoryMaxHeight);
        
    }

    void AddEnemy(Transform enemy)
    {
        listEnemy.Add(enemy);
        SetTarget();
    }
    void RemoveEnemy(Transform enemy) 
    { 
        listEnemy.Remove(enemy);
    }
    void SetTarget()
    {
        if (listEnemy.Count <= 0)
        {
            CurrentTarget = null;
            return;
        }
        CurrentTarget = listEnemy[0];

    }

    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            AddEnemy(enemy.transform);
        }
        
    }
    private void OnTriggerExit2D(Collider2D enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            RemoveEnemy(enemy.transform);
        }
    }
    //IEnumerator DelaySpawnArrow()
    //{
    //    yield return new WaitForSeconds(2);
    //    GameObject newArrow = Instantiate(arrow, shotPoint.position, Quaternion.identity);
    //    
    //    newArrow.transform.SetParent(shotPoint);
    //    yield return new WaitForSeconds(2);
    //}
}
