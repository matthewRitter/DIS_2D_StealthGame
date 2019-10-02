using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float duration = 0.0f;
    public float magnitude = 0.7f;
    public float dampingSpeed = 1.0f;
    public GameObject player;

    private Vector3 initialPosition;
    private SmoothCamera2D smoother;

    // Start is called before the first frame update
    void Start()
    {
        smoother = GetComponent<SmoothCamera2D>();
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
            smoother.SetShaking(true);
            //transform.position = initialPosition + Random.insideUnitSphere * magnitude;

            Vector3 shakeAmt = Random.insideUnitSphere * magnitude;

            Vector3 target = new Vector3(player.transform.position.x, player.transform.position.y, -10);

            if (!(transform.position.x <= target.x+1 && transform.position.x >= target.x-1) && (transform.position.y <= target.y + 1 && transform.position.y >= target.y - 1))
            {
                transform.position = Vector3.MoveTowards(transform.position, target, .05f * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector3(player.transform.position.x + shakeAmt.x, player.transform.position.y + shakeAmt.y, -10);

                duration -= Time.deltaTime * dampingSpeed;
            }
        }
        else
        {
            //duration = 0f;
            //transform.position = initialPosition;
            smoother.SetShaking(false);
        }

    }

    public void Shake(float shakeTime = 0.5f, float shakeMagnitude = 0.7f, float shakeDamping = 1.0f)
    {
        duration = shakeTime;
        magnitude = shakeMagnitude;
        shakeDamping = dampingSpeed;
    }
}
