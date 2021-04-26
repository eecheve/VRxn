using System;

public class StringValueAttribute : Attribute //https://weblogs.asp.net/stefansedich/enum-with-string-values-in-c
{
    /// <summary>
    /// This attribute is used to represent a string value
    /// for a value in an enum.
    /// </summary>
    #region Properties

    /// <summary>
    /// Holds the stringvalue for a value in an enum.
    /// </summary>
    public string StringValue { get; protected set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor used to init a StringValue Attribute
    /// </summary>
    /// <param name="value"></param>
    public StringValueAttribute(string value)
    {
        this.StringValue = value;
    }

    #endregion
}
