using UnityEngine;
using UnityEngine.UI;

public class EnemyConsciousnessUI : MonoBehaviour
{
    public Consciousness target;
    public Slider slider;
    public Camera mainCamera;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        slider.maxValue = target.maxConsciousness;
        slider.value = target.currentConsciousness;
    }

    void Update()
    {
        if (target == null) return;

        slider.value = target.currentConsciousness;

        // Siempre mirar a la cámara
        transform.rotation = Quaternion.LookRotation(
            transform.position - mainCamera.transform.position
        );
    }
}