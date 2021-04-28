using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Reflection;
using System.Text.RegularExpressions;

public static class ExtensionMethods
{
    public static void Shuffle<T>(this IList<T> list, System.Random rnd) //https://stackoverflow.com/questions/273313/randomize-a-listt
    {
        for (var i = list.Count; i > 0; i--)
            list.Swap(0, rnd.Next(0, i));
    }

    public static void Swap<T>(this IList<T> list, int i, int j) //https://stackoverflow.com/questions/273313/randomize-a-listt
    {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }

    public static string GetStringValue(this Enum value)
    {
        /// <summary>
        /// Will get the string value for a given enums value, this will
        /// only work if you assign the StringValue attribute to
        /// the items in your enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>

        // Get the type
        Type type = value.GetType();

        // Get fieldinfo for this type
        FieldInfo fieldInfo = type.GetField(value.ToString());

        // Get the stringvalue attributes
        StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
            typeof(StringValueAttribute), false) as StringValueAttribute[];

        // Return the first if there was a match.
        return attribs.Length > 0 ? attribs[0].StringValue : null;
    }

    public static float GetAngleValue(this Vector2 origin, Vector2 target)
    {
        ///<summary>
        ///Calculates the angle value between this vector and a target
        ///</summary>
        ///<param name="target"></param>
        /// <returns></returns>
        Vector2 refVector = target - origin;
        return (float)(Mathf.Atan2(refVector.y, refVector.x) * (180 / Mathf.PI));
    }

    public static float GetAngleValue(this Vector3 origin, Vector3 target)
    {
        ///<summary>
        ///Calculates the angle value between this vector and a target
        ///</summary>
        ///<param name="target"></param>
        /// <returns></returns>
        float dot = Vector3.Dot(origin, target);
        float denom = origin.magnitude * target.magnitude;
        return Mathf.Acos(dot / denom);
    }

    public static bool IsNumeric(this string s) //https://stackoverflow.com/questions/894263/identify-if-a-string-is-a-number
    {
        foreach (char c in s)
        {
            if (!char.IsDigit(c) && c != '.' && c != '-')
            {
                return false;
            }
        }

        return true;
    }

    public static int IntValue(this bool b)
    {
        ///<summary>
        ///Gets the int value of a boolean.
        ///</summary>
        if (b == true)
            return 1;
        else
            return 0;
    }

    public static bool AnimatorIsPlaying(this Animator animator) //https://answers.unity.com/questions/362629/how-can-i-check-if-an-animation-is-being-played-or.html
    {
        return animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public static bool IsBetweenNumbers(this float numberToCheck, float low, float high) //https://stackoverflow.com/questions/3188672/how-to-elegantly-check-if-a-number-is-within-a-range
    {
        return (numberToCheck >= low && numberToCheck <= high);
    }

    public static string RemoveDigits(this string key)
    {
        return Regex.Replace(key, @"\d", "");
    }

    public static string RemoveSpecialChars(this string key)
    {
        return Regex.Replace(key, @"[^\w]", "");
    }
}
