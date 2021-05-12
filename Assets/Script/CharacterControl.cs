using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    float inputX;
    float inputY;
    Animator animator;
    Vector3 currentDirection;
    Camera mainCam;
    float maxLength = 1;
    float rotationSpeed = 10;
    float maxSpeed;

    void Start()
    {
        animator = GetComponent<Animator>();
        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        InputMove();
        InputRotation();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxSpeed = 1;
            //inputX = maxSpeed * Input.GetAxis("Horizontal"); // karakterin kamerayla dönmesi için
            //inputY = maxSpeed * Input.GetAxis("Vertical"); // karakterin kamerayla dönmesi için
        }
        else if (Input.GetKey(KeyCode.W))
        {
            maxSpeed = 0.2f;
            inputX = 1;
            //inputY = maxSpeed * Input.GetAxis("Vertical"); // karakterin kamerayla dönmesi için
        }
        else
        {
            maxSpeed = 0f;
            inputX = 0;
            //inputX = maxSpeed * Input.GetAxis("Horizontal"); // karakterin kamerayla dönmesi için
            //inputY = maxSpeed * Input.GetAxis("Vertical"); // karakterin kamerayla dönmesi için
        }

//----------------------------------------------------------------------------------
        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetFloat("sol_hareket", 0.34f);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                animator.SetFloat("sol_hareket", 0.63f);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                animator.SetFloat("sol_hareket", 0.92f);
            }
            else
            {
                animator.SetFloat("sol_hareket", 0.12f);
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetFloat("sol_hareket", 0f);
        }
//----------------------------------------------------------------------------------

        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("walk_right", true);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("walk_right", false);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("walk_back", true);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("walk_back", false);
        }

        currentDirection = new Vector3(inputX, 0, inputY);

    }

    void InputMove()
    {
        //stabil deðer saðlar hýz yavaþ yavaþ artar ve azalýr
        //time.delta oynanan cihaza göre çalýþmasýný saðlar
        animator.SetFloat("speed", Vector3.ClampMagnitude(currentDirection, maxSpeed).magnitude, maxLength, Time.deltaTime * 10);

    }

    void InputRotation()
    {
        Vector3 camOfset = mainCam.transform.forward; // mousela beraber dönsün
        //Vector3 camOfset = mainCam.transform.TransformDirection(currentDirection); //mouse döndükten sonra herhangi bir tuþa basarsan karakter döner
        camOfset.y = 0;
        //karakteri döndürme
        transform.forward = Vector3.Slerp(transform.forward, camOfset, Time.deltaTime * rotationSpeed);
    }
}
