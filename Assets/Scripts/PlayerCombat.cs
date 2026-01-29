using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    [Header("Timing (seconds)")]
    public float punchHitTime = 0.25f;
    public float kickHitTime = 0.35f;

    [Header("Animator Layers")]
    [SerializeField] private int punchLayerIndex = 1;
    [SerializeField] private float layerBlendTime = 0.1f;

    private PlayerGrabSystem grabSystem;
    private bool isAttacking = false;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip KickClip;

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

        yield return StartCoroutine(BlendLayerWeight(punchLayerIndex, 1f, layerBlendTime));

        animator.ResetTrigger("Punch");
        animator.ResetTrigger("AttackWithObject");

        audioSource.PlayOneShot(hitClip);

        if (grabSystem != null && grabSystem.IsHoldingObject())
            animator.SetTrigger("AttackWithObject");
        else
            animator.SetTrigger("Punch");

        yield return new WaitForSeconds(GetAnimationLength("Punch"));

        yield return StartCoroutine(BlendLayerWeight(punchLayerIndex, 0f, layerBlendTime));

        isAttacking = false;
    }

    IEnumerator Kick()
    {
        isAttacking = true;

        animator.ResetTrigger("Kick");
        animator.SetTrigger("Kick");

        audioSource.PlayOneShot(KickClip);

        yield return new WaitForSeconds(GetAnimationLength("Kick"));

        isAttacking = false;
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

    public void DealDamage() { }
    public void EndAttack() { }
}