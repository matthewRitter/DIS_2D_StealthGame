using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float duration = 0.0f;
    public float magnitude = 0.7f;
    public float dampingSpeed = 1.0f;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (duration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * magnitude;

            duration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            duration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void Shake(float shakeTime = 0.5f, float shakeMagnitude = 0.7f, float shakeDamping = 1.0f)
    {
        duration = shakeTime;
        magnitude = shakeMagnitude;
        shakeDamping = dampingSpeed;
    }
}
