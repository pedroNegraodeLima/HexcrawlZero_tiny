using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public Vector2 directionalInput;
    public bool confirmButton;
    public bool jumpButton;
    public bool interactButton;
    public bool leftClickButton;

    public bool testJumpButton;

    //AnimatorManager animatorManager;
    //private float moveAmout;
    [SerializeField]
    Animator animator;
    //bool isWalking;

    private void Awake()
    {
        animator.SetBool("isWalking", false);

        //animatorManager = GetComponent<AnimatorManager>();
    }

    public void HandleAllInputs()
    {
        HandleMovimentInput();
    }
    private void HandleMovimentInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
        directionalInput = new Vector2(horizontalInput, verticalInput).normalized;

        jumpButton = Input.GetButtonDown("Jump");
        interactButton = Input.GetKeyDown(KeyCode.E);
        leftClickButton = Input.GetMouseButtonDown(0);
        confirmButton = jumpButton || interactButton || leftClickButton;

        testJumpButton = Input.GetKeyDown(KeyCode.L);

        //moveAmout = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        //animatorManager.UpdateAnimatorValues(0, moveAmout);

        if (directionalInput != new Vector2(0, 0) )
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
