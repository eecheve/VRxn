using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonHelperManager : MonoSingleton<ButtonHelperManager>
{
    [SerializeField] private InputActionReference toggleHelpAction = null;
    
    [SerializeField] private List<Button3DHelper> selectHelpers = null;
    [SerializeField] private List<Button3DHelper> grabHelpers = null;

    [SerializeField] private Button3DHelper teleportHelper = null;
    [SerializeField] private Button3DHelper rotateHelper = null;
    [SerializeField] private Button3DHelper helpHelper = null;
    [SerializeField] private Button3DHelper undoHelper = null;
    [SerializeField] private Button3DHelper prismHelper = null;
    [SerializeField] private Button3DHelper toggleGrabHelper = null;

    public List<Button3DHelper> SelectHelpers { get { return selectHelpers; } private set { selectHelpers = value; } }
    public List<Button3DHelper> GrabHelpers { get { return grabHelpers; } private set { grabHelpers = value; } }


    public Button3DHelper TeleportHelper { get { return teleportHelper; } private set { teleportHelper = value; } }
    public Button3DHelper RotateHelper { get { return rotateHelper; } private set { rotateHelper = value; } }
    public Button3DHelper HelpHelper { get { return helpHelper; } private set { helpHelper = value; } }
    public Button3DHelper UndoHelper { get { return undoHelper; } private set { undoHelper = value; } }
    public Button3DHelper PrismHelper { get { return prismHelper; } private set { prismHelper = value; } }
    public Button3DHelper ToggleGrabHelper { get { return toggleGrabHelper; } private set { toggleGrabHelper = value; } }

    
    private List<Button3DHelper> allHelpers = new List<Button3DHelper>();

    public delegate void HelperToggled();
    public event HelperToggled OnHelpActive;
    public event HelperToggled OnRotateActive;
    public event HelperToggled OnTeleportActive;
    public event HelperToggled OnToggleGrabActive;
    public event HelperToggled OnTogglePrismActive;
    public event HelperToggled OnSelectActive;
    public event HelperToggled OnGrabActive;
    public event HelperToggled OnDistanceGrabActive;
    public event HelperToggled OnUndoActive;

    private int helpIndex = 0;

    private void Awake()
    {
        for (int i = 0; i < 2; i++)
        {
            if(allHelpers.Contains(SelectHelpers[i]) == false)
                allHelpers.Add(SelectHelpers[i]);
            
            if(allHelpers.Contains(GrabHelpers[i]) == false)
                allHelpers.Add(GrabHelpers[i]);
        }
        if (allHelpers.Contains(TeleportHelper) == false)
            allHelpers.Add(TeleportHelper);

        if (allHelpers.Contains(RotateHelper) == false)
            allHelpers.Add(RotateHelper);

        if (allHelpers.Contains(HelpHelper) == false)
            allHelpers.Add(HelpHelper);

        if (allHelpers.Contains(UndoHelper) == false)
            allHelpers.Add(UndoHelper);

        if (allHelpers.Contains(PrismHelper) == false)
            allHelpers.Add(PrismHelper);

        if (allHelpers.Contains(ToggleGrabHelper) == false)
            allHelpers.Add(ToggleGrabHelper);

        ToggleHelpers(false);
    }

    private void OnEnable()
    {
        toggleHelpAction.action.performed += ToggleHelpOneByOne;
    }

    private void ToggleHelpOneByOne(InputAction.CallbackContext obj)
    {
        if(helpIndex == 0)
        {
            ToggleHelpers(false);
            ToggleHelpers(SelectHelpers, true);

            OnSelectActive?.Invoke();
        }
        else if(helpIndex == 1)
        {
            ToggleHelpers(false);
            ToggleHelpers(RotateHelper, true);

            OnRotateActive?.Invoke();
        }
        else if(helpIndex == 2)
        {
            ToggleHelpers(false);
            ToggleHelpers(TeleportHelper, true);

            OnTeleportActive?.Invoke();
        }
        else if (helpIndex == 3)
        {
            ToggleHelpers(false);
            ToggleHelpers(ToggleGrabHelper, true);

            OnToggleGrabActive?.Invoke();
        }
        else if (helpIndex == 4)
        {
            ToggleHelpers(false);
            ToggleHelpers(PrismHelper, true);

            OnTogglePrismActive?.Invoke();
        }
        else if (helpIndex == 5)
        {
            ToggleHelpers(false);
            ToggleHelpers(UndoHelper, true);

            OnUndoActive?.Invoke();
        }
        else if (helpIndex == 6)
        {
            ToggleHelpers(false);
            ToggleHelpers(HelpHelper, true);

            OnHelpActive?.Invoke();
        }
        else if (helpIndex == 7)
        {
            ToggleHelpers(false);

            helpIndex = 0;
            return;
        }
        else
        {
            Debug.LogError("ButtonHelperManager_ToggleHelpOneByOne: error choosing index");
        }

        helpIndex++;
    }

    public void ToggleHelpers(bool state)
    {
        foreach (var helper in allHelpers)
        {
            helper.ToggleButtonHelper(state);
        }
    }

    public void ToggleHelpers(List<Button3DHelper> helpers, bool state)
    {
        foreach (var helper in helpers)
        {
            helper.ToggleButtonHelper(state);
        }
    }

    public void ToggleHelpers(Button3DHelper helper, bool state)
    {
        helper.ToggleButtonHelper(state);
    }

    public void ToggleHelpers(int index, bool state)
    {
        if(index < allHelpers.Count)
        {
            allHelpers[index].ToggleButtonHelper(state);
        }
    }

    public void ActivateHelperFromObject(GameObject helperObject)
    {
        Button3DHelper helper = helperObject.GetComponent<Button3DHelper>();
        if(helper != null)
        {
            helper.ToggleButtonHelper(true);
        }
    }

    public void DeactivateHelperFromObject(GameObject helperObject)
    {
        Button3DHelper helper = helperObject.GetComponent<Button3DHelper>();
        if (helper != null)
        {
            helper.ToggleButtonHelper(false);
        }
    }
    private void OnDisable()
    {
        toggleHelpAction.action.performed -= ToggleHelpOneByOne;
    }
}
