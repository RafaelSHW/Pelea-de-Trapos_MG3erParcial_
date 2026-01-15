using UnityEngine;

public interface IGrabbable
{
    float GetChannelTime();
    bool CanBeGrabbed();
    void OnGrabComplete(GameObject grabber);
}