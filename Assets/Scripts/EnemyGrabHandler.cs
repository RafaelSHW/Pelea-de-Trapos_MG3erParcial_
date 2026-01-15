using UnityEngine;

[RequireComponent(typeof(Consciousness))]
public class EnemyGrabHandler : MonoBehaviour, IGrabbable
{
    public float channelTime = 3f;

    private Consciousness consciousness;

    void Awake()
    {
        consciousness = GetComponent<Consciousness>();
    }

    public float GetChannelTime()
    {
        return channelTime;
    }

    public bool CanBeGrabbed()
    {
        return consciousness != null && consciousness.IsKnockedDown();
    }

    public void OnGrabComplete(GameObject grabber)
    {
        Debug.Log("Enemigo agarrado");


    }
}