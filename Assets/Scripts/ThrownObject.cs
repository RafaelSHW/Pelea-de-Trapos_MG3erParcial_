using UnityEngine;
using System.Collections;

public class ThrownObject : MonoBehaviour
{
    public float lifetime = 5f; // Tiempo antes de empezar a desaparecer
    public float shrinkSpeed = 2f;

    void Start()
    {
        // Iniciamos la cuenta regresiva para desaparecer
        StartCoroutine(DestroySequence());
    }

    IEnumerator DestroySequence()
    {
        // Espera el tiempo de vida útil en el suelo
        yield return new WaitForSeconds(lifetime);

        // Se hace chiquito poco a poco
        while (transform.localScale.x > 0.01f)
        {
            transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
            yield return null;
        }

        // Finalmente se elimina de la jerarquía
        Destroy(gameObject);
    }

    // Aquí es donde iría tu código de daño cuando me lo pases
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            // Lógica de daño aquí
            Debug.Log("Golpeaste a: " + collision.gameObject.name);
        }
    }
}