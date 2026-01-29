using UnityEngine;

public class KickImpact : MonoBehaviour
{
    public int kickDamage = 20;
    private bool alreadyHit = false;
    public GameObject scorePopupPrefab;

    void OnEnable()
    {
        alreadyHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (alreadyHit) return;

        Consciousness target = other.GetComponentInParent<Consciousness>();

        if (target != null && !target.IsUnconscious())
        {
            alreadyHit = true;
            target.ReceiveImpact(kickDamage);

            if (GameManager.instance != null)
            {
                GameManager.instance.AddPoints(true, 20);
            }

            if (scorePopupPrefab != null)
            {
                GameObject popup = Instantiate(scorePopupPrefab, transform.position, Quaternion.identity);
                if (popup.TryGetComponent<FloatingNumber>(out FloatingNumber fn))
                {
                    fn.SetText(kickDamage);
                }
            }
        }
    }
}