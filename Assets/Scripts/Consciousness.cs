using UnityEngine;
using System.Collections;

public class Consciousness : MonoBehaviour
{
    public int maxConsciousness = 100;
    public float ragdollDuration = 5f;

    private int current;
    private RagdollController ragdoll;

    void Awake()
    {
        current = maxConsciousness;
        ragdoll = GetComponent<RagdollController>();
    }

    public void ReceiveImpact(int amount)
    {
        current -= amount;

        if (current <= 0)
            StartCoroutine(RagdollState());
    }

    IEnumerator RagdollState()
    {
        ragdoll.EnableRagdoll(true);
        yield return new WaitForSeconds(ragdollDuration);
        current = maxConsciousness;
        ragdoll.EnableRagdoll(false);
    }
}