using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    public CharacterController controller;

    void FixedUpdate()
    {

        Vector3 moveDir = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        moveDirection.x = moveDir.x * speed;
        moveDirection.z = moveDir.z * speed;

        if (Input.GetButton("Jump") && controller.isGrounded)
            moveDirection.y = jumpSpeed;
        moveDirection.y -= gravity * Time.deltaTime;

        if (controller.gameObject.activeSelf)
            controller.Move(moveDirection * Time.deltaTime);

    }
}