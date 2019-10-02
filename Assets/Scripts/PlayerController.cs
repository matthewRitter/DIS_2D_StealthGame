﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    private Rigidbody2D protagonist;
    public GameObject player;
    private bool facingRight = true;
    public AudioSource sound;
    public Animator animator;
    public GameObject knife;
    private Animator knifeAnimator;
    public GameObject damageHitBox;
    float prevMagnitude;
    private bool playerMoving;
    private Vector2 lastMove;

    // Start is called before the first frame update
    void Start()
    {
        protagonist = gameObject.GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
        knifeAnimator = knife.GetComponent<Animator>();
}

    IEnumerator Attack()
    {
        damageHitBox.SetActive(true);
        knifeAnimator.SetTrigger("isAttacking");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Working");
        damageHitBox.SetActive(false);
        knifeAnimator.ResetTrigger("isAttacking");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMoving = false;

        float knifeHori = Input.GetAxis("Horizontal");
        float knifeVert = Input.GetAxis("Vertical");

        if (protagonist.velocity.magnitude > 0) {
            knife.transform.up = protagonist.velocity.normalized;
        }

        if (Input.GetKeyDown("space"))
        {
            Debug.Log("SPACE");
            StartCoroutine(Attack());
        }

        if (Input.GetAxis("Horizontal") > 0.35)
        {
            knifeHori = 0.35f;
        }

        if (Input.GetAxis("Horizontal") < -0.35)
        {
            knifeHori = -0.35f;
        }

        if (Input.GetAxis("Vertical") > 0.35)
        {
            knifeVert = 0.35f;
        }

        if (Input.GetAxis("Vertical") < -0.35)
        {
            knifeVert = -0.35f;
        }

        Vector2 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //if the player is moving       
        if (movement.magnitude > 0)
        {
            //set the animator to moving
            animator.SetBool("PlayerMoving", true);

            //if the magnitude is increasing you want to update the "running" state in the animator but also the last move - this way when
            //player releases the key, the last move will be stored already in the animator  
            if (movement.magnitude > prevMagnitude || prevMagnitude >= 0.99f)
            {
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);


                lastMove = movement;
                animator.SetFloat("LastMoveX", lastMove.x);
                animator.SetFloat("LastMoveY", lastMove.y);
            }

        }

        //set the player movement to false in the animator
        if (movement.magnitude == 0)
        {
            animator.SetBool("PlayerMoving", false);
        }

        //update the previous magnitude
        prevMagnitude = movement.magnitude;

        knife.transform.localPosition = new Vector3(knifeHori, knifeVert, 0.0f);


        //redundant -> not used in the animator
        animator.SetFloat("Magnitude", movement.magnitude);

        //transform.position = transform.position + movement * Time.deltaTime * playerSpeed;
        protagonist.velocity = movement * playerSpeed;


    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
