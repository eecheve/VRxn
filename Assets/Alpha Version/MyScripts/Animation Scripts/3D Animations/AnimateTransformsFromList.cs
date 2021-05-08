using UnityEngine;

public class AnimateTransformsFromList : MonoBehaviour
{
    public GameObject ObjectToAnimate { get; set; }
    public Animation Anim { get; set; }
    //public Animator Animator { get; set; }

    public void LoadInformation()
    {
        ObjectToAnimate = GetTransformsFromList.Instance.objectToAnimate;
        if(ObjectToAnimate == null)
        {
            Debug.Log("Please load from GetTransformsFromList(Script) first");
        }
        else
        {
            GetAnimationComponentReferences();
        }
    }

    private void GetAnimationComponentReferences()
    {
        Anim = ObjectToAnimate.GetComponent<Animation>();
        //Animator = ObjectToAnimate.GetComponent<Animator>();
        if (Anim == null)
            Anim = ObjectToAnimate.AddComponent<Animation>();
        //if (Animator == null)
        //    Animator = ObjectToAnimate.AddComponent<Animator>();
    }
}
