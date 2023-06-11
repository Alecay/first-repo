using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDTower : MonoBehaviour
{
    public TDProjectile projectile;
    public float shootingSpeed = 1;
    private float timeSinceLastShot = 0;

    public TDEnemy targetedEnemy;

    private void Start()
    {
        timeSinceLastShot = Time.time;
    }

    private void Update()
    {
        Shoot(targetedEnemy.transform);
    }
    private void Shoot(Transform target)
    {
        if(Time.time > timeSinceLastShot + shootingSpeed)
        {
            timeSinceLastShot = Time.time;
            CreateProjectile(target);
        }
    }    

    private void CreateProjectile(Transform target)
    {
        var newP = Instantiate(projectile);
        newP.target = target;
        newP.dir = (target.position - transform.position).normalized;
        newP.transform.position = transform.position;
        newP.transform.parent = transform;
    }
}
