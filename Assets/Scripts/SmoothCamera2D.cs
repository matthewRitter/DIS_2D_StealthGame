//https://www.salusgames.com/2016/12/28/smooth-2d-camera-follow-in-unity3d/
using UnityEngine;

public class SmoothCamera2D : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform Target;

    private bool beingShaken;

    void Start()
    {
        beingShaken = false;
    }

    void Update()
    {
        if (!beingShaken)
        {
            //Vector3 newPosition = Target.position;
            Vector3 newPosition = new Vector3(Target.position.x, Target.position.y, -10);


            transform.position = Vector3.Slerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);
        }
    }

    public void SetShaking(bool shake)
    {
        beingShaken = shake;
    }
}