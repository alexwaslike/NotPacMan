using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    public CharacterController controller;
    public GameController gameController;

    private int cycle = -14;

    void FixedUpdate()
    {

        Vector3 moveDir = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        moveDirection.x = moveDir.x * speed;
        moveDirection.z = moveDir.z * speed;

        if (controller.gameObject.activeSelf)
            controller.Move(moveDirection * Time.deltaTime);


        // movement effects
        if (gameController.AllowGameplay)
        {
            if (moveDirection.x != 0 || moveDirection.z != 0)
            {
                // footstep sounds
                if (!gameController.soundController.footstepsSource.isPlaying)
                    gameController.soundController.PlayFootsteps(true);

                // view bobbing
                if (cycle < 1)
                {
                    gameController.mainCamera.transform.position += new Vector3(0, 0.015f, 0);
                }
                else
                {
                    gameController.mainCamera.transform.position -= new Vector3(0, 0.015f, 0);
                    if (cycle == 14)
                        cycle = -14;
                }
                cycle++;
            }
            else
                gameController.soundController.PlayFootsteps(false);
        }

    }
}