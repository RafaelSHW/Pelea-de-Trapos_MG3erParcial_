using UnityEngine;
using System.Collections;

public class Consciousness : MonoBehaviour
{
    [Header("Consciousness")]
    public int maxConsciousness = 100;
    public int currentConsciousness;

    [Header("Knockdown")]
    public float knockdownTime = 5f;

    private bool isKnockedDown = false;
    private RagdollController ragdoll;

    void Awake()
    {
        currentConsciousness = maxConsciousness;
        ragdoll = GetComponent<RagdollController>();
    }

    public void ReceiveImpact(int amount)
    {
        if (isKnockedDown) return;

        currentConsciousness -= amount;
        currentConsciousness = Mathf.Max(currentConsciousness, 0);

        if (currentConsciousness <= 0)
        {
            StartCoroutine(Knockdown());
        }
    }

    IEnumerator Knockdown()
    {
        isKnockedDown = true;

        // Activar ragdoll
        ragdoll.SetRagdoll(true);

        yield return new WaitForSeconds(knockdownTime);

        // Levantarse
        ragdoll.SetRagdoll(false);
        currentConsciousness = maxConsciousness;

        isKnockedDown = false;
    }

    public bool IsKnockedDown()
    {
        return isKnockedDown;
    }
}