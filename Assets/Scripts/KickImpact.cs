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

        if (target != null)
        {
            alreadyHit = true;

            target.ReceiveImpact(kickDamage);

            if (GameManager.instance != null)
            {
                GameManager.instance.AddPoints(true, 20);
            }

            Debug.Log("Golpe único registrado");
            if (scorePopupPrefab != null)
            {

                GameObject popup = Instantiate(scorePopupPrefab, transform.position, Quaternion.identity);

                popup.GetComponent<FloatingNumber>().SetText(20);
            }
        }
    }
}
