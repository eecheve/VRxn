using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimatorParameters : MonoBehaviour
{
    [SerializeField] private string parameter = "";
    [SerializeField] private Animator m_animator = null;

    public void SetFloat(float value)
    {
        try
        {
            m_animator.SetFloat(parameter, value);
        }
        catch (System.Exception)
        {
            Debug.Log(name + "SetAnimatorParameters: parameter name not found");
            throw;
        }
    }

    public void SetBool(bool state)
    {
        try
        {
            m_animator.SetBool(parameter, state);
        }
        catch (System.Exception)
        {
            Debug.Log(name + "SetAnimatorParameters: parameter name not found");
            throw;
        }

    }
}
