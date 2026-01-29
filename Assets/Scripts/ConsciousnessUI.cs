using UnityEngine;
using UnityEngine.UI;

public class ConsciousnessUI : MonoBehaviour
{
    public Consciousness target;
    public Slider sliderUI;

    [Header("Visual Elements")]
    public Image fillImage;
    public Image heartIcon;

    [Header("Sprites")]
    public Sprite fullHeart;
    public Sprite brokenHeart;
    public Sprite emptyHeart;

    void Start()
    {
        if (target != null && sliderUI != null)
        {
            sliderUI.maxValue = target.maxConsciousness;
            sliderUI.value = target.currentConsciousness;
        }
    }

    void Update()
    {
        if (target == null || sliderUI == null) return;

        sliderUI.value = target.currentConsciousness;
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        float healthPercent = sliderUI.value / sliderUI.maxValue;

        if (healthPercent <= 0)
        {
            if (heartIcon != null) heartIcon.sprite = emptyHeart;
        }
        else if (healthPercent < 0.4f)
        {
            if (heartIcon != null) heartIcon.sprite = brokenHeart;
        }
        else
        {
            if (heartIcon != null) heartIcon.sprite = fullHeart;
        }
    }
}