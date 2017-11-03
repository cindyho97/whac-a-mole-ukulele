using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Slider))]
[RequireComponent(typeof(RectTransform))]
public class TuneSlider : MonoBehaviour
{

    private Slider slider;
    private float sliderSize;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }
    void Update()
    {
        UpdateSliderSense();
    }
    public void UpdateSliderSense()
    {
        if (sliderSize == 0)
        {
            sliderSize = GetComponent<RectTransform>().rect.width;
            sliderSize = sliderSize / (slider.maxValue - slider.minValue);
        }

        slider.fillRect.rotation = new Quaternion(0, 0, 0, 0);
        slider.fillRect.pivot = new Vector2(slider.fillRect.transform.parent.localPosition.x, slider.fillRect.pivot.y);
        if (slider.value > 0)
        {
            slider.fillRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sliderSize * slider.value);
        }
        else
        {
            slider.fillRect.Rotate(0, 0, 180);
            slider.fillRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, -1 * sliderSize * slider.value);
        }
        slider.fillRect.localPosition = new Vector3(0, 0, 0);
    }
}
