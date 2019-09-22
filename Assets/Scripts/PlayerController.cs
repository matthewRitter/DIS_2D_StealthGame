using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        protagonist = gameObject.GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        Debug.Log("x is" + movement.x);
        Debug.Log("y is" + movement.y);
        Debug.Log("magnitude is" + movement.magnitude);


        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

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
