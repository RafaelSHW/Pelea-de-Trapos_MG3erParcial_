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
    public float punchHitTime = 0.25f; // cuando el puño conecta
    public float kickHitTime = 0.35f;  // cuando la patada conecta

    [Header("Animator Layers")]
    [SerializeField] private int punchLayerIndex = 1;
    [SerializeField] private float layerBlendTime = 0.1f;

    private bool isAttacking = false;

    void Start()
    {
        animator.SetLayerWeight(punchLayerIndex, 0f);
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

        yield return StartCoroutine(BlendLayerWeight(punchLayerIndex, 1f, layerBlendTime));

        animator.ResetTrigger("Punch");
        animator.SetTrigger("Punch");

        yield return new WaitForSeconds(punchHitTime);
        TryHit(punchPoints);

        yield return new WaitForSeconds(GetAnimationLength("Punch") - punchHitTime);

        yield return StartCoroutine(BlendLayerWeight(punchLayerIndex, 0f, layerBlendTime));

        isAttacking = false;
    }

    IEnumerator Kick()
    {
        isAttacking = true;

        animator.ResetTrigger("Kick");
        animator.SetTrigger("Kick");

        yield return new WaitForSeconds(kickHitTime);
        TryHit(kickPoints);

        yield return new WaitForSeconds(GetAnimationLength("Kick") - kickHitTime);
        isAttacking = false;
    }

    void TryHit(int points)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, enemyLayer))
        {
            hit.collider.GetComponent<Consciousness>()?.ReceiveImpact(points);
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
            animator.SetLayerWeight(layer, Mathf.Lerp(start, target, time / duration));
            yield return null;
        }

        animator.SetLayerWeight(layer, target);
    }
}