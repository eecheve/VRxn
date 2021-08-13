using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

[CustomEditor(typeof(AnimateTransformsFromList))]
public class AnimateTransformsFromListEditor : Editor
{
    private AnimateTransformsFromList transformsFromList;
    private List<Transform[]> listOfTransforms;

    public override void OnInspectorGUI()
    {
        transformsFromList = (AnimateTransformsFromList)target;

        DrawDefaultInspector();
        if (GUILayout.Button("Load Information"))
        {
            Debug.Log("Loading Information...");
            transformsFromList.LoadInformation();
            listOfTransforms = GetTransformsFromList.Instance.TransformList;
        }
        else if (GUILayout.Button("Build Animation"))
        {
            Debug.Log("Building animation...");
            BuildAnimation();
        }
    }

    private void BuildAnimation()
    {
        AnimationClip clip = new AnimationClip();
        string animName = transformsFromList.ObjectToAnimate.name;
        string assetPath = "Assets/Animations/" + animName;

        clip.name = animName;
        transformsFromList.Anim.clip = clip;
        //clip.legacy = true;

        //AssetDatabase.CreateAsset(clip, assetPath + ".anim"); //<---- repositioned??
        //AnimatorController animatorController = AnimatorController.CreateAnimatorControllerAtPath(assetPath + ".controller"); <-- I don't need an animator because I'm using legacy to modify the animation.
        //animatorController.name = animName;
        //animatorController.AddMotion(clip);

        for (int i = 0; i < listOfTransforms[0].Length; i++)
        {
            Transform t = listOfTransforms[0][i];
            if (t.name.Equals(animName))
            {
                continue; //<--- this is supposed to avoid creating an animation for the parent object but it is not working really.
            }
            else
            {
                string path = AnimationUtility.CalculateTransformPath(t, t.transform.root);
                var animatable = AnimationUtility.GetAnimatableBindings(t.gameObject, t.transform.root.gameObject);

                foreach (var item in animatable)
                {
                    string _propertyName = item.propertyName;
                    EditorCurveBinding floatCurve = EditorCurveBinding.FloatCurve(path, typeof(Transform), _propertyName);

                    if (_propertyName.Contains("aterial") || _propertyName.Contains("cale"))
                    {
                        break;
                    }
                    else
                    {
                        ManageValuesFromProperyName(i, _propertyName, clip, floatCurve);
                    }
                }
                //Animator objAnimator = transformsFromList.Animator; <-- I don't need an animator because I'm using legacy to modify the animation.
                //objAnimator.runtimeAnimatorController = animatorController;
            }
        }
        AssetDatabase.CreateAsset(clip, assetPath + ".anim"); //<-- repositioned.
    }

    private void ManageValuesFromProperyName(int index, string _propertyName, AnimationClip clip, EditorCurveBinding floatCurve)
    {
        if (_propertyName.Equals("m_LocalRotation.x"))
        {
            float[] values = new float[listOfTransforms.Count];
            for (int j = 0; j < values.Length; j++)
            {
                values[j] = listOfTransforms[j][index].localRotation.x;
            }

            AssignValueListToFloatCurveIntoClip(clip, floatCurve, values);
        }
        else if (_propertyName.Equals("m_LocalRotation.y"))
        {
            float[] values = new float[listOfTransforms.Count];
            for (int j = 0; j < values.Length; j++)
            {
                values[j] = listOfTransforms[j][index].localRotation.y;
            }

            AssignValueListToFloatCurveIntoClip(clip, floatCurve, values);
        }
        else if (_propertyName.Equals("m_LocalRotation.z"))
        {
            float[] values = new float[listOfTransforms.Count];
            for (int j = 0; j < values.Length; j++)
            {
                values[j] = listOfTransforms[j][index].localRotation.z;
            }

            AssignValueListToFloatCurveIntoClip(clip, floatCurve, values);
        }
        else if (_propertyName.Equals("m_LocalRotation.w"))
        {
            float[] values = new float[listOfTransforms.Count];
            for (int j = 0; j < values.Length; j++)
            {
                values[j] = listOfTransforms[j][index].localRotation.w;
            }

            AssignValueListToFloatCurveIntoClip(clip, floatCurve, values);
        }
        else if (_propertyName.Equals("m_LocalPosition.x"))
        {
            float[] values = new float[listOfTransforms.Count];
            for (int j = 0; j < values.Length; j++)
            {
                values[j] = listOfTransforms[j][index].localPosition.x;
            }

            AssignValueListToFloatCurveIntoClip(clip, floatCurve, values);
        }
        else if (_propertyName.Equals("m_LocalPosition.y"))
        {
            float[] values = new float[listOfTransforms.Count];
            for (int j = 0; j < values.Length; j++)
            {
                values[j] = listOfTransforms[j][index].localPosition.y;
            }

            AssignValueListToFloatCurveIntoClip(clip, floatCurve, values);
        }
        else if (_propertyName.Equals("m_LocalPosition.z"))
        {
            float[] values = new float[listOfTransforms.Count];
            for (int j = 0; j < values.Length; j++)
            {
                values[j] = listOfTransforms[j][index].localPosition.z;
            }

            AssignValueListToFloatCurveIntoClip(clip, floatCurve, values);
        }
    }

    private void AssignValueListToFloatCurveIntoClip(AnimationClip clip, EditorCurveBinding floatCurve, float[] values)
    {
        Keyframe[] keys = new Keyframe[values.Length];
        float deltaTime = 1 /(float)values.Length;
                
        float t = 0;

        for (int k = 0; k < values.Length; k++)
        {
            keys[k] = new Keyframe(t, values[k]);
            t += deltaTime;
        }

        AnimationCurve curve = new AnimationCurve(keys);
        AnimationUtility.SetEditorCurve(clip, floatCurve, curve);
    }
}
