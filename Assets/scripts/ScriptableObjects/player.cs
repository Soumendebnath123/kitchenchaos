using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour, IKitchenObjectParent
{
    public static player Instance { get; private set;}


    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    


    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform KitchenObjectHoldPoint;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;
    private void Awake()
    {
        if (Instance != null)      //instance of a class, you are essentially creating a concrete object that can hold data and perform operations defined by the class.
        {
            Debug.LogError("There are more than one Player instance ");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
        
    }
    private void GameInput_OnInteractAlternateAction(object sender,EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        if(selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender,System.EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
        
    }
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }
    public bool IsWalking()
    {
        return isWalking;
    }
    private void HandleInteractions()
    {
        
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        float interactDistance = 1f;
        if (Physics.Raycast(transform.position, moveDir, out RaycastHit raycastHit, interactDistance,counterLayerMask)) 
        {
            if(raycastHit.transform.TryGetComponent(out BaseCounter BaseCounter))
            {
                //has clear couter
                if(BaseCounter != selectedCounter)
                {
                    SetSelectedCounter(BaseCounter);
                    
                }
                else
                {
                    SetSelectedCounter (null);
                    
                }
            }
            else
            {
                SetSelectedCounter (null);
                
            }
        }
        Debug.Log(selectedCounter);
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;  // Fixed typo in playerRadius initialization
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);  // Removed duplicate playerRadius parameter

        if (!canMove)
        {
            // cannot move towards moveDir

            //attempts only on the x
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);  // Fixed parameter name

            if (!canMove)
            {
                // cannot move only on the x

                //attempt only z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);  // Fixed parameter name

                if (canMove)
                {
                    // cannot move in any direction
                    // Handle collision or blocked movement here
                }
                else
                {
                    //can move only on the z
                    moveDir = moveDirZ;
                }
            }
            else
            {
                // cannot move only on the z
                moveDir = moveDirX;
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        if (isWalking)  // Only rotate when moving
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs        //the ?. operator is called the "null conditional" 
                    {
                        selectedCounter = selectedCounter 
                    });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return KitchenObjectHoldPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this,EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearkitchenObject()
    {
        kitchenObject = null;
    }
    public bool HaskitchenObject()
    {
        return kitchenObject != null;
    }
}