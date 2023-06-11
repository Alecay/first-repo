using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDProjectile : MonoBehaviour
{
    public float speed;
    public Vector3 dir;
    public Transform target;

    public void Move()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    private void Update()
    {
        Move();

        if(target != null && Vector3.Distance(target.position, transform.position) < 0.3f)
        {
            Destroy(gameObject);
        }
    }
}
