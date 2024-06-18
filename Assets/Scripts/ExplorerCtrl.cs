using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExplorerCtrl : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    
    PlayerInput playerInput;
    CharacterController charCtrl;

    Vector2 currentMovimentInput;
    Vector3 currentMoviment;
    bool isMovimentPressed;
    bool isWalking;

    public float movementSpeed = 5.0f;
    public float rotationFactorPerFrame = 1.0f;

    public float timeToAlive = 1.0f;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        charCtrl = GetComponent<CharacterController>();

        playerInput.CharControl.Move.started += OnMovementInput;
        playerInput.CharControl.Move.canceled += OnMovementInput;
        playerInput.CharControl.Move.performed += OnMovementInput;

        //vai ser o que controla a interação com o ambiente.
        playerInput.CharControl.Interact.started += HandleInteraction;
        playerInput.CharControl.Interact.canceled += HandleInteraction;
    }

    void HandleInteraction(InputAction.CallbackContext context)
    {
        context.ReadValueAsButton();
        Debug.Log("rolou");
    }
    void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMoviment.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMoviment.z;

        Quaternion currentRotation = transform.rotation;

        if (isMovimentPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }
    
    void OnMovementInput (InputAction.CallbackContext context)
    {
        currentMovimentInput = context.ReadValue<Vector2>();
        currentMoviment.x = currentMovimentInput.x;
        currentMoviment.z = currentMovimentInput.y;
        isMovimentPressed = currentMovimentInput.x != 0 || currentMovimentInput.y != 0;
    }

    void HandleAnimation()
    {
        isWalking = animator.GetBool("isWalking");

        if (isMovimentPressed && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }

        else if (isMovimentPressed && isWalking)
        {
            animator.SetBool("isWalking", false);
        }
    }

    IEnumerator Start()
    {
        if (!isMovimentPressed && !isWalking)
        {
            yield return new WaitForSeconds(timeToAlive);
            animator.Play("Explorer_RIG_keepAlive");
        }        
    }

    void HandleGravity()
    {
        if (charCtrl.isGrounded)
        {
            float groundedGravity = -0.05f;
            currentMoviment.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            currentMoviment.y += gravity;
        }

    }
    void Update()
    {
        HandleRotation();
        HandleAnimation();
        charCtrl.Move(currentMoviment * movementSpeed * Time.deltaTime); 
    }

    private void OnEnable()
    {
        playerInput.CharControl.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharControl.Disable();
    }

}
