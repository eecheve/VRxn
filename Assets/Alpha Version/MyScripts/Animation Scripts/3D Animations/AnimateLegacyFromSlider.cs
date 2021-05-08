using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AnimateLegacyFromSlider : MonoBehaviour
{
    [SerializeField] private GameObject animatable = null;

    private Animation anim;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        anim = animatable.GetComponent<Animation>();
        anim.clip.legacy = true; //<--- this could be turned on and off if I want to use an animator later to manage the clips!
    }

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(AnimateOnSliderValue);
    }

    public void AnimateOnSliderValue(float value)
    {
        Debug.Log(" AnimateOnSliderValue: animation name: " + anim.clip.name);
        
        anim.Play();
        anim[anim.clip.name].speed = 0; //so it doesn't move by itself

        Debug.Log("AnimateOnSliderValue: AnimatioState: " + anim[anim.clip.name].name);
        Debug.Log("AnimateOnSliderValue: is playing state is: " + anim.isPlaying.ToString());

        anim[anim.clip.name].normalizedTime = value;
        if (value == 0)
        {
            Debug.Log("AnimateOnSliderValue should stop animation");
            anim.Stop();
        }
        else
        {
            anim.Play();
        }
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(AnimateOnSliderValue);
    }

}
