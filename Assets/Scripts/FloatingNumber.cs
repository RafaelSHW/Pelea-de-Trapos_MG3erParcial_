using UnityEngine;
using TMPro;

public class FloatingNumber : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float destroyTime = 1f;
    public TextMeshPro textMesh;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        if (Camera.main != null)
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
                             Camera.main.transform.rotation * Vector3.up);
    }

    public void SetText(int amount)
    {
        textMesh.text = "+" + amount;
    }
}
