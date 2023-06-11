using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDEnemy : MonoBehaviour
{
    public Transform target;
    public float speed;

    private void Update()
    {
        MoveTowardsTarget();
    }

    public void MoveTowardsTarget()
    {
        Vector3 towardsTarget = (target.position - transform.position).normalized;
        transform.Translate(towardsTarget * speed * Time.deltaTime);
    }
}
