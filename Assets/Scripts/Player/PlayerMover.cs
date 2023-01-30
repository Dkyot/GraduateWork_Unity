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

    private bool rollingFlag = false;
    private float rollSpeed;


    void Update() {
        moveDirection = (Vector3)inputs.movementInput;
    }

    private void FixedUpdate() {
        
        
        if (rollingFlag) {// кувырок
            float rollSpeedMultiplier = 5f;
            rollSpeed -= rollSpeed * rollSpeedMultiplier * Time.deltaTime;
            float rollSpeedMinimum = 30f;
            if (rollSpeed < rollSpeedMinimum)
                rollingFlag = false;
        }
        else // обычное передвижение
            rigidbody2D.velocity = moveDirection * 4;

        Debug.Log(rigidbody2D.velocity);
    }

    private void Move(object sender, EventArgs e) {
        // // работает как телепорт
        // Vector3 dashPosition = transform.position + moveDirection * 10f;
        // RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, moveDirection, 10f, dashLayerMask);
        // if (raycastHit2D.collider != null) {
        //     dashPosition = raycastHit2D.point;
        // }
        // rigidbody2D.MovePosition(dashPosition);


        // кувырок
        rollSpeed = 60f;
        rollingFlag = true;

        rigidbody2D.velocity = moveDirection * rollSpeed;

    }
}
