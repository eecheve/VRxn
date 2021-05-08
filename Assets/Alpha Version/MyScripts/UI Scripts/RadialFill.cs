using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RadialFill : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public Action<float> OnValueChange;
    private Image img;

    private void Start()
    {
        img = GetComponent<Image>();
    }

    public void OnDrag(PointerEventData ped)
    {
        Vector2 pos = default(Vector2);
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(img.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / img.rectTransform.sizeDelta.x);
            pos.y = (pos.y / img.rectTransform.sizeDelta.y);

            Vector2 p = img.rectTransform.pivot;
            pos.x += p.x - 0.5f;
            pos.y += p.y - 0.5f;

            float x = Mathf.Clamp(pos.x, -1, 1);
            float y = Mathf.Clamp(pos.y, -1, 1);

            Vector3 angle = new Vector3(x, y, 0).normalized;
            Quaternion r = Quaternion.FromToRotation(Vector3.up, angle);
            float ratio = (360f - (r.eulerAngles.z)) / 360;

            SetRatio(ratio, 0f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void SetRatio(float ratio, float duration = 0f)
    {
        StartCoroutine(UpdateCircleGraphics(img.fillAmount, ratio, duration));
        OnValueChange.Invoke(ratio);
    }

    private IEnumerator UpdateCircleGraphics(float currentRatio, float desiredRatio, float animationDuration)
    {
        for (float t = 0; t < animationDuration; t += Time.deltaTime)
        {
            img.fillAmount = Mathf.Lerp(currentRatio,desiredRatio,t/animationDuration);
            yield return null;
        }

        img.fillAmount = desiredRatio;
    }
}
