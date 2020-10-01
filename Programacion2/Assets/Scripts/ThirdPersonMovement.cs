using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public sound playersound;
    public Rigidbody player_rb;
    public Animator animator;

    public GameObject espinaDorsal;

    private void Start()
    {
        playersound = GetComponent<sound>();
        player_rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        bool estaApuntando = animator.GetBool("estaApuntando");

        //MovimientoSinApuntar
        if (direction.magnitude >= 0.1f && !estaApuntando)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            player_rb.velocity = moveDir * speed;

            animator.SetFloat("movementSpeed", direction.magnitude);


            animator.Play("Walk");
            // playersound.SoundPlay(playersound.clips[1]);
        }
        else
        {
            animator.SetFloat("movementSpeed", 0f);
            player_rb.velocity = Vector3.zero;
        }

        if (direction.magnitude >= 0.1f && estaApuntando)
        {

            if(horizontal < 0)
            {
                player_rb.velocity = transform.right * -speed;
                animator.Play("AimWalkLeft");
                animator.SetFloat("movementSpeed", direction.magnitude);

            }
            if (horizontal > 0) 
            {
                player_rb.velocity = transform.right * speed;
                animator.Play("AimWalkRight");
                animator.SetFloat("movementSpeed", direction.magnitude);

            }
            if (vertical < 0) 
            {
                player_rb.velocity = transform.forward * -speed;
                animator.Play("AimWalkBack");
                animator.SetFloat("movementSpeed", direction.magnitude);

            }
            if (vertical > 0)
            {
                player_rb.velocity = transform.forward * speed;
                animator.Play("AimWalkForward");
                animator.SetFloat("movementSpeed", direction.magnitude);

            }

        }
        if (estaApuntando)
        {
            float rotationSpeed = 6f;
            player_rb.rotation = Quaternion.Euler(player_rb.rotation.eulerAngles + new Vector3(0f, rotationSpeed * Input.GetAxis("Mouse X"), 0f));
            espinaDorsal.transform.rotation = Quaternion.Euler(espinaDorsal.transform.rotation.eulerAngles + new Vector3(rotationSpeed * Input.GetAxis("Mouse Y"), 0f, 0f));

           // Mathf.Clamp(player_rb.rotation.eulerAngles.y, 5f, 30f);
        }



    }
}
