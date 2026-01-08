using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Rigidbody[] bodies;
    private Animator animator;

    void Awake()
    {
        bodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        EnableRagdoll(false);
    }

    public void EnableRagdoll(bool active)
    {
        animator.enabled = !active;

        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = !active;
            rb.detectCollisions = active;
        }
    }
}