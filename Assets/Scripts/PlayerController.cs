using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Consciousness))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float rotationSpeed = 10f;

    [Header("References")]
    public Animator animator;
    public Transform cameraTransform;

    private CharacterController controller;
    private Consciousness consciousness;
    private Vector3 verticalVelocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        consciousness = GetComponent<Consciousness>();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (consciousness == null) return;

        if (consciousness.IsUnconscious() || consciousness.IsKnockedDown())
        {
            if (animator != null) animator.SetBool("IsWalking", false);
            return;
        }

        HandleMovement();
        HandleAnimations();
    }

    void HandleMovement()
    {
        if (controller == null || !controller.enabled || !gameObject.activeInHierarchy) return;

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(x, 0f, z).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            Vector3 moveDirection = (forward * inputDirection.z + right * inputDirection.x).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }

        if (controller.isGrounded && verticalVelocity.y < 0)
        {
            verticalVelocity.y = -2f;
        }

        verticalVelocity.y += gravity * Time.deltaTime;

        if (controller.enabled)
        {
            controller.Move(verticalVelocity * Time.deltaTime);
        }
    }

    void HandleAnimations()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        bool isWalking = Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f;
        animator.SetBool("IsWalking", isWalking);
    }

    public void DealDamage() { }
    public void EndAttack() { }
}