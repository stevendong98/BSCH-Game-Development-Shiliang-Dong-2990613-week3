using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] CharacterController controller;
    [SerializeField] float smooth = 1f;
    [SerializeField] float speed = 6.0F;
    [SerializeField] float jumpSpeed = 8.0F;
    [SerializeField] float gravity = 20.0F;
    [SerializeField] GameObject fx;

    Quaternion lookLeft;
    Quaternion lookRight;
    Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        lookRight = transform.rotation;
        lookLeft = lookRight * Quaternion.Euler(0, -180, 0);
    }

    void Update()
    {

        if (controller.isGrounded)
        {

            anim.SetBool("Run", false);

            moveDirection = new Vector3(0, 0, Input.GetAxis("Horizontal"));


            if (Input.GetKey(KeyCode.A))
            {

                transform.rotation = lookLeft;
                moveDirection = transform.TransformDirection(-moveDirection);
                moveDirection *= speed;

                anim.SetBool("Run", true);

            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.rotation = lookRight;
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;
                anim.SetBool("Run", true);
            }


            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

            fx.SetActive(true);
        }
        else fx.SetActive(false);

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        transform.position = new Vector3(transform.position.x,transform.position.y, 0.97f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}