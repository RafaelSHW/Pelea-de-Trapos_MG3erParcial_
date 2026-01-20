using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class GrabbableObject : MonoBehaviour, IGrabbable
{
    public float channelTime = 2f;

    public float GetChannelTime()
    {
        return channelTime;
    }

    public bool CanBeGrabbed()
    {
        return true;
    }

    public void OnGrabComplete(GameObject grabber)
    {
        PlayerGrabSystem grabSystem = grabber.GetComponent<PlayerGrabSystem>();
        if (grabSystem == null) return;

        grabSystem.HoldObject(this);
    }
}