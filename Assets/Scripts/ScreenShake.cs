using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float duration = 0.0f;
    public float magnitude = 0.7f;
    public float dampingSpeed = 1.0f;
    private Vector3 initialPosition;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        initialPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (duration > 0)
        {
            //transform.position = initialPosition + Random.insideUnitSphere * magnitude;

            Vector3 shakeAmt = Random.insideUnitSphere * magnitude;


            if (transform.position != player.transform.position)
            {
                transform.position = Vector3.Slerp(transform.position, (Vector2)player.transform.position, 10 * Time.deltaTime);
                //transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            }
            else
                transform.position = new Vector3(player.transform.position.x + shakeAmt.x, player.transform.position.y + shakeAmt.y, -10);

            duration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            duration = 0f;
            //transform.position = initialPosition;
        }

    }

    public void Shake(float shakeTime = 0.5f, float shakeMagnitude = 0.7f, float shakeDamping = 1.0f)
    {
        duration = shakeTime;
        magnitude = shakeMagnitude;
        shakeDamping = dampingSpeed;
    }
}
