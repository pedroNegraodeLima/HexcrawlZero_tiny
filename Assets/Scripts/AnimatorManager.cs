using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    [SerializeField] 
    Animator animator;

    int horizontal;
    int vertical;

    private void Awake()
    {
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        //Animation snapping
        float snappedHorizontal;
        float snappedVertical;

        #region Snapped Horizontal
        if (horizontalMovement > 0 && horizontalMovement < 0.9f)
        {
            snappedHorizontal = 1.0f;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.9f)
        {
            snappedHorizontal = -1.0f;
        }
        else
        {
            snappedHorizontal = 0;
        }
        #endregion

        #region Snapped Vertical
        if (verticalMovement > 0 && verticalMovement < 0.9f)
        {
            snappedVertical = 1.0f;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.9f)
        {
            snappedVertical = -1.0f;
        }
        else
        {
            snappedVertical = 0;
        }
        #endregion

        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }

}
