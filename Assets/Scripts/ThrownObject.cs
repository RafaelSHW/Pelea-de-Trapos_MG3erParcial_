using UnityEngine;
using System.Collections;

public class ThrownObject : MonoBehaviour
{
    public float lifetime = 5f;
    public float shrinkSpeed = 2f;
    public int damageAmount = 25;

    void Start()
    {
        StartCoroutine(DestroySequence());
    }

    IEnumerator DestroySequence()
    {
        yield return new WaitForSeconds(lifetime);

        while (transform.localScale.x > 0.01f)
        {
            transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
            yield return null;
        }

        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {        
        Consciousness targetConsciousness = collision.gameObject.GetComponentInParent<Consciousness>();

        if (targetConsciousness != null)
        {
            targetConsciousness.ReceiveImpact(damageAmount);

            Debug.Log("Objeto golpeó a: " + collision.gameObject.name + " causando " + damageAmount + " de daño.");

            StopAllCoroutines(); 
            
            Destroy(gameObject);
        }
    }
}