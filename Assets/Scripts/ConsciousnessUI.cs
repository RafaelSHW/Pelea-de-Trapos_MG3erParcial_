using UnityEngine;
using UnityEngine.UI;

public class ConsciousnessUI : MonoBehaviour
{
    public Consciousness target;
    public Slider slider;

    void Start()
    {
        slider.maxValue = target.maxConsciousness;
        slider.value = target.currentConsciousness;
    }

    void Update()
    {
        slider.value = target.currentConsciousness;
    }
}