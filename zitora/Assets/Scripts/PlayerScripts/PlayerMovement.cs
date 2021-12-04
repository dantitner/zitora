using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour 
{
    private Rigidbody2D body;
    private Vector2 movement;
    public float acceleration = 20.0f;
    public float velocityMax = 20.0f;
    public float stopingPower = 0.5f;
    public float runnigMutiplayer = 1;
    public float defaultRunningMultiplayer = 1;
    public float runningMaxMultiplayer = 2;

    private void Start() {
        body = GetComponent<Rigidbody2D>();
        if(IsLocalPlayer)
            GameObject.Find("Main Camera").GetComponent<CameraFollow>().SetTarget(transform);
    }

    private void Update() {
        if (!IsOwner)
            return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        runnigMutiplayer = Input.GetKey(KeyCode.LeftShift) ? runningMaxMultiplayer : defaultRunningMultiplayer;
    }

    private void FixedUpdate() {
        if (!IsOwner)
            return;

        if (movement.x != 0 || movement.y != 0)
        {
            var movementVector = movement * acceleration;
            body.velocity = Vector2.ClampMagnitude(Vector2.Lerp(body.velocity, movementVector, Time.fixedDeltaTime), velocityMax * runnigMutiplayer);
        }
        else 
        {
            body.velocity = Vector2.Lerp(body.velocity, Vector2.zero, stopingPower);
        }
    }

    private void MoveWithForce(Vector2 vector)
    {
        body.AddForce(vector, ForceMode2D.Impulse);
    }
}
