//https://www.salusgames.com/2016/12/28/smooth-2d-camera-follow-in-unity3d/
using UnityEngine;

public class SmoothCamera2D : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform Target;

    private void Update()
    {
        Vector3 newPosition = Target.position;
        newPosition.z = -10;
        transform.position = Vector3.Slerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);
    }
}