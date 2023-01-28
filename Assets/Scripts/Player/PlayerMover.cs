using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField]
    private LayerMask dashLayerMask;
    
    private PlayerController inputs;
    private new Rigidbody2D rigidbody2D;

    private Vector3 moveDirection;

    
    void Awake() {
        inputs = GetComponent<PlayerController>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        inputs.OnAttack += Move;
    }

    void Update() {
        moveDirection = (Vector3)inputs.movementInput;
    }

    private void FixedUpdate() {
        rigidbody2D.velocity = moveDirection * 4;
    }

    private void Move(object sender, EventArgs e) {
        Vector3 dashPosition = transform.position + moveDirection * 3f;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, moveDirection, 3f, dashLayerMask);
        if (raycastHit2D.collider != null) {
            dashPosition = raycastHit2D.point;
        }
        rigidbody2D.MovePosition(dashPosition);
    }
}
