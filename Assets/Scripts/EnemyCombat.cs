using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int damage = 15;
    public float attackRange = 2f;
    public LayerMask playerLayer;

    private Animator animator;
    private bool isAttacking = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TryAttack()
    {
        if (isAttacking) return;
        isAttacking = true;

        animator.ResetTrigger("Punch");
        animator.SetTrigger("Punch");
    }

    public void DealDamage()
    {
        Vector3 origin = transform.position + transform.forward * 0.5f + Vector3.up * 1.2f;

        Debug.DrawRay(origin, transform.forward * attackRange, Color.blue, 0.5f);

        if (Physics.Raycast(origin, transform.forward, out RaycastHit hit, attackRange, playerLayer))
        {
            hit.collider.GetComponentInParent<Consciousness>()?.ReceiveImpact(damage);
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
    }
}
