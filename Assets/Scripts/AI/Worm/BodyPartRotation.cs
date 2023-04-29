using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartRotation : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform target;

    private Vector2 direction;

    private void Update() {
        direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
}
