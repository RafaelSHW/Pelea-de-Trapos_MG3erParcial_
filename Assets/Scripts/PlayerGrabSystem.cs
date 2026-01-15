using UnityEngine;
using System.Collections;

public class PlayerGrabSystem : MonoBehaviour
{
    [Header("Grab Settings")]
    public float grabRange = 2f;
    public LayerMask grabMask;
    public KeyCode grabKey = KeyCode.E;
    public KeyCode dropKey = KeyCode.G;

    [Header("Holding")]
    public Transform rightHandBone;

    [Header("References")]
    public Animator animator;

    private bool isChanneling = false;
    private Coroutine channelRoutine;

    private GrabbableObject heldObject;

    void Update()
    {
        // No permitir agarre si ya sostiene algo
        if (heldObject == null)
        {
            if (Input.GetKey(grabKey))
            {
                if (!isChanneling)
                    TryStartGrab();
            }
            else
            {
                CancelGrab();
            }
        }

        // Soltar objeto
        if (heldObject != null && Input.GetKeyDown(dropKey))
        {
            DropObject();
        }
    }

    void TryStartGrab()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, grabRange, grabMask))
        {
            IGrabbable grabbable = hit.collider.GetComponent<IGrabbable>();
            if (grabbable != null && grabbable.CanBeGrabbed())
            {
                channelRoutine = StartCoroutine(ChannelGrab(grabbable));
            }
        }
    }

    IEnumerator ChannelGrab(IGrabbable target)
    {
        isChanneling = true;

        float time = target.GetChannelTime();
        float elapsed = 0f;

        animator.SetBool("IsGrabbing", true);

        while (elapsed < time)
        {
            if (!Input.GetKey(grabKey))
            {
                CancelGrab();
                yield break;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        animator.SetBool("IsGrabbing", false);
        target.OnGrabComplete(gameObject);

        isChanneling = false;
    }

    void CancelGrab()
    {
        if (!isChanneling) return;

        if (channelRoutine != null)
            StopCoroutine(channelRoutine);

        animator.SetBool("IsGrabbing", false);
        isChanneling = false;
    }

    public void HoldObject(GrabbableObject obj)
    {
        heldObject = obj;

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;

        obj.transform.SetParent(rightHandBone);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        animator.SetBool("IsHoldingObject", true);
    }

    void DropObject()
    {
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = false;

        heldObject.transform.SetParent(null);
        heldObject = null;

        animator.SetBool("IsHoldingObject", false);
    }

    public bool IsHoldingObject()
    {
        return heldObject != null;
    }
}