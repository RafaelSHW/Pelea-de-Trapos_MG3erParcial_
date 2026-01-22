using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public float attackRange = 2f;
    public LayerMask enemyLayer;

    [Header("Attack Values")]
    public int punchPoints = 20;
    public int kickPoints = 20;

    [Header("Timing (seconds)")]
    public float punchHitTime = 0.25f;
    public float kickHitTime = 0.35f;

    [Header("Animator Layers")]
    [SerializeField] private int punchLayerIndex = 1;
    [SerializeField] private float layerBlendTime = 0.1f;

    private PlayerGrabSystem grabSystem;
    private bool isAttacking = false;

    void Start()
    {
        animator.SetLayerWeight(punchLayerIndex, 0f);
        grabSystem = GetComponent<PlayerGrabSystem>();
    }

    void Update()
    {
        if (isAttacking) return;

        if (Input.GetMouseButtonDown(0))
            StartCoroutine(Punch());

        if (Input.GetMouseButtonDown(1))
            StartCoroutine(Kick());
    }

    IEnumerator Punch()
    {
        isAttacking = true;

        // Activar capa superior
        yield return StartCoroutine(
            BlendLayerWeight(punchLayerIndex, 1f, layerBlendTime)
        );

        animator.ResetTrigger("Punch");
        animator.ResetTrigger("AttackWithObject");

        if (grabSystem != null && grabSystem.IsHoldingObject())
            animator.SetTrigger("AttackWithObject");
        else
            animator.SetTrigger("Punch");

        // Esperar al momento del impacto
        yield return new WaitForSeconds(punchHitTime);
        TryHit(punchPoints);

        // Esperar a que termine la animación
        yield return new WaitForSeconds(
            Mathf.Max(0.05f, GetAnimationLength("Punch") - punchHitTime)
        );

        // Desactivar capa
        yield return StartCoroutine(
            BlendLayerWeight(punchLayerIndex, 0f, layerBlendTime)
        );

        isAttacking = false;
    }

    public void ObjectHit()
    {
        TryHit(punchPoints);
    }

    IEnumerator Kick()
    {
        isAttacking = true;

        animator.ResetTrigger("Kick");
        animator.SetTrigger("Kick");

        yield return new WaitForSeconds(kickHitTime);
        TryHit(kickPoints);

        yield return new WaitForSeconds(
            Mathf.Max(0.05f, GetAnimationLength("Kick") - kickHitTime)
        );

        isAttacking = false;
    }

    void TryHit(int points)
    {
        Vector3 origin =
            transform.position +
            transform.forward * 0.5f +
            Vector3.up * 1.2f;

        Debug.DrawRay(origin, transform.forward * attackRange, Color.red, 0.5f);

        if (Physics.Raycast(origin, transform.forward, out RaycastHit hit, attackRange, enemyLayer))
        {
            Consciousness target =
                hit.collider.GetComponentInParent<Consciousness>();

            if (target != null)
            {
                target.ReceiveImpact(points);
            }
        }
    }

    float GetAnimationLength(string animName)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name.Contains(animName))
                return clip.length;
        }

        return 0.6f;
    }

    IEnumerator BlendLayerWeight(int layer, float target, float duration)
    {
        float start = animator.GetLayerWeight(layer);
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            animator.SetLayerWeight(
                layer,
                Mathf.Lerp(start, target, time / duration)
            );
            yield return null;
        }

        animator.SetLayerWeight(layer, target);
    }
}