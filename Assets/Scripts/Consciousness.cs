using UnityEngine;
using System.Collections;

public class Consciousness : MonoBehaviour
{
    [Header("Consciousness")]
    public int maxConsciousness = 100;
    public int currentConsciousness;

    [Header("Knockdown / Ragdoll")]
    public float ragdollTime = 5f;

    private bool isUnconscious = false;

    private RagdollController ragdoll;

    void Awake()
    {
        ragdoll = GetComponent<RagdollController>();
        currentConsciousness = maxConsciousness;
    }

    public void ReceiveImpact(int amount)
    {
        if (isUnconscious) return;

        currentConsciousness -= amount;
        currentConsciousness = Mathf.Clamp(currentConsciousness, 0, maxConsciousness);

        if (currentConsciousness <= 0)
        {
            StartCoroutine(Knockdown());
        }
    }

    private IEnumerator Knockdown()
    {
        isUnconscious = true;

        if (ragdoll != null)
            ragdoll.SetRagdoll(true);

        yield return new WaitForSeconds(ragdollTime);

        if (ragdoll != null)
            ragdoll.SetRagdoll(false);

        currentConsciousness = maxConsciousness;
        isUnconscious = false;
    }
    /// <summary>
    /// </summary>
    public bool IsUnconscious()
    {
        return isUnconscious;
    }

    /// <summary>
    /// </summary>
    public bool IsKnockedDown()
    {
        return isUnconscious;
    }
}