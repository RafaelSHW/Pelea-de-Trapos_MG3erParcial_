using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Rigidbody[] rigidbodies;
    private Collider[] colliders;

    private Animator animator;
    private CharacterController characterController;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip DeathClip;

    void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();

        SetRagdoll(false);
    }

    public void SetRagdoll(bool state)
    {
        // Activar / desactivar Animator
        if (animator != null)
            animator.enabled = !state;

        // Activar / desactivar CharacterController
        if (characterController != null)
            characterController.enabled = !state;

        foreach (Rigidbody rb in rigidbodies)
        {
            if (rb == null) continue;

            rb.isKinematic = !state;

            if (state)
            {
                // Solo cuando el ragdoll está ACTIVO
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                audioSource.PlayOneShot(DeathClip);
            }
        }

        foreach (Collider col in colliders)
        {
            if (col == null) continue;

            // Evitamos apagar el collider raíz (si existe)
            if (col.transform == transform) continue;

            col.enabled = state;
        }
    }
}