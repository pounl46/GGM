using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{
    public float vspeed = 0;
    public float gravity = 10f;
    public CharacterController controller;
    public float speed = 10f;
    public GameObject rotationobj;
    public float jumpHeight = 20f;
    private bool canJump = true;
    private bool qDash = true;
    public float dashSpeed = 10f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private Vector3 stand = new Vector3(0, 0, 0);
    
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveVec = new Vector3(x, 0, z);
        moveVec = controller.transform.TransformDirection(moveVec);

        if (Input.GetKeyDown(KeyCode.Q) && qDash)
        {
            if (moveVec != stand)
            {
                controller.Move(moveVec * dashSpeed * Time.deltaTime);
            }
            else
            {
                controller.Move(transform.forward * dashSpeed * Time.deltaTime);
            }
            qDash = false;
            StartCoroutine(DashCoolTime());
        }

        if (controller.isGrounded)
        {
            vspeed = 0;
            canJump = true;

            if (Input.GetButtonDown("Jump") && canJump)
            {
                vspeed = Mathf.Sqrt(jumpHeight * 2f * gravity);
                canJump = false;
            }
        }
        else
        {
            vspeed -= gravity * Time.deltaTime;
        }

        moveVec.y = vspeed;
        controller.Move(moveVec * Time.deltaTime * speed);
        this.transform.rotation = Quaternion.Euler(0, rotationobj.transform.eulerAngles.y, 0);
    }

    private IEnumerator DashCoolTime()
    {
        yield return new WaitForSeconds(3f);
        qDash = true;
    }
}
