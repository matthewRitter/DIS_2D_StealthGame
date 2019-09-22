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
    public GameObject knife;

    // Start is called before the first frame update
    void Start()
    {
        protagonist = gameObject.GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float knifeHori = Input.GetAxis("Horizontal");
        float knifeVert = Input.GetAxis("Vertical");
        
    
        if (Input.GetAxis("Horizontal") > 0.7)
        {
            knifeHori = 0.5f;
        }

        if (Input.GetAxis("Horizontal") < -0.7)
        {
            knifeHori = -0.5f;
        }

        if (Input.GetAxis("Vertical") > 0.7)
        {
            knifeVert = 0.5f;
        }

        if (Input.GetAxis("Vertical") < -0.7)
        {
            knifeVert = -0.5f;
        }

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        Debug.Log("x is" + movement.x);
        Debug.Log("y is" + movement.y);
        Debug.Log("magnitude is" + movement.magnitude);

        knife.transform.localPosition = new Vector3(knifeHori, knifeVert, 0.0f);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
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
