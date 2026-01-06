using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public float attackRange = 2f;
    public LayerMask enemyLayer;

    public int punchPoints = 20;
    public int kickPoints = 20;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Punch();

        if (Input.GetMouseButtonDown(1))
            Kick();
    }

    void Punch()
    {
        animator.SetTrigger("Punch");
        TryHit(punchPoints);
    }

    void Kick()
    {
        animator.SetTrigger("Kick");
        TryHit(kickPoints);
    }

    void TryHit(int points)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, enemyLayer))
        {
            hit.collider.GetComponent<Consciousness>()?.ReceiveImpact(points);
        }
    }
}