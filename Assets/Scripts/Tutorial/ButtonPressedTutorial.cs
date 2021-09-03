using UnityEngine;
using UnityEngine.UI;

public class ButtonPressedTutorial : Condition
{
    [SerializeField] private Button button = null;
    [SerializeField] private SpriteRenderer highlightArrow = null;
    [SerializeField] private Vector3 arrowOffset = Vector3.zero;
    
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        animator = button.gameObject.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(FulfillCondition);
        
        if(animator != null)
            animator.enabled = true;

        ManageArrow(true);
    }

    private void ManageArrow(bool state)
    {
        if (highlightArrow != null)
        {
            highlightArrow.transform.position = button.transform.position + arrowOffset;
            highlightArrow.enabled = state;
        }
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(FulfillCondition);
        
        if (animator != null)
            animator.enabled = false;

        ManageArrow(false);
    }
}
