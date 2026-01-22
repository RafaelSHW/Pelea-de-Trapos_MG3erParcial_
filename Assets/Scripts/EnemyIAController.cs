using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    public Transform player;
    public float attackDistance = 2f;

    private EnemyMovement movement;
    private EnemyCombat combat;
    private Consciousness consciousness;

    void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        combat = GetComponent<EnemyCombat>();
        consciousness = GetComponent<Consciousness>();
    }

    void Update()
    {
        if (consciousness.IsUnconscious())
        {
            movement.Stop();
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackDistance)
        {
            movement.MoveTowards(player.position);
        }
        else
        {
            movement.Stop();
            combat.TryAttack();
        }
    }
}