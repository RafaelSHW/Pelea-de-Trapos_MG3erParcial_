using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public float gravity = -9.81f;

    private Animator animator;
    private CharacterController controller;

    private Vector3 velocity;

    void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void MoveTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        direction.y = 0f;

        if (direction.magnitude > 0.1f)
        {
            controller.Move(direction * moveSpeed * Time.deltaTime);
            transform.forward = direction;
        }

        animator.SetBool("IsWalking", true);
    }

    public void Stop()
    {
        animator.SetBool("IsWalking", false);
    }
}