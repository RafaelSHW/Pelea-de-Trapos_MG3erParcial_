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

    private bool isAttacking = false;

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

        animator.ResetTrigger("Punch");
        animator.SetTrigger("Punch");

        yield return new WaitForSeconds(punchHitTime);
        TryHit(punchPoints);

        yield return new WaitForSeconds(GetAnimationLength("Punch") - punchHitTime);
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
}