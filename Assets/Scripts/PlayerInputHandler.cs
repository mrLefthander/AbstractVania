using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerInputHandler : MonoBehaviour
{
  [SerializeField]
  private float runSpeed = 5f;
  [SerializeField]
  private float jumpSpeed = 5f;

  private Vector2 movementInput;
  private Rigidbody2D myRigidbody;
  private Animator myAnimator;
  private SpriteRenderer mySpriteRenderer;

  private void Start()
  {
    myRigidbody = GetComponent<Rigidbody2D>();
    myAnimator = GetComponent<Animator>();
    mySpriteRenderer = GetComponent<SpriteRenderer>();
  }

  public void OnMoveInput(InputAction.CallbackContext context)
  {
    movementInput = context.ReadValue<Vector2>();
    myRigidbody.velocity = new Vector2(movementInput.x * runSpeed, myRigidbody.velocity.y);

    HandleRunningAnimation(movementInput.x);

    FlipSprite(movementInput.x);
  }



  public void OnJumpInput(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpSpeed);
    }
  }

  private void FlipSprite(float movingDirection)
  {
    bool isRunningLeft = movingDirection < 0;
    mySpriteRenderer.flipX = isRunningLeft;
  }

  private void HandleRunningAnimation(float movingDirection)
  {
    bool isPlayerRunning = Mathf.Abs(movingDirection) > Mathf.Epsilon;
    myAnimator.SetBool("isRunning", isPlayerRunning);
  }
}
