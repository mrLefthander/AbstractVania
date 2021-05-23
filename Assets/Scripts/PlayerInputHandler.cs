using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerInputHandler : MonoBehaviour
{
  [SerializeField]
  private float runSpeed = 5f;
  [SerializeField]
  private float jumpSpeed = 10f;
  [SerializeField]
  private float climbSpeed = 5f;

  private Vector2 movementInput;
  private Rigidbody2D myRigidbody;
  private Animator myAnimator;
  private SpriteRenderer mySpriteRenderer;
  private CapsuleCollider2D myCollider;

  private void Start()
  {
    myRigidbody = GetComponent<Rigidbody2D>();
    myAnimator = GetComponent<Animator>();
    mySpriteRenderer = GetComponent<SpriteRenderer>();
    myCollider = GetComponent<CapsuleCollider2D>();
  }

  public void OnMoveInput(InputAction.CallbackContext context)
  {
    movementInput = context.ReadValue<Vector2>();
    ClimbLadder(context);
    Run(movementInput);
  }

  private void ClimbLadder(InputAction.CallbackContext context)
  {
    movementInput = context.ReadValue<Vector2>();
    bool isTouchingLadder = myCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    if (!isTouchingLadder)
    {
      myAnimator.SetBool("isClimbing", false);
      myRigidbody.gravityScale = 3f;
      myAnimator.speed = 1f;
      return;
    }
    if (Mathf.Abs(movementInput.y) > Mathf.Epsilon && isTouchingLadder)
    {
      myRigidbody.gravityScale = 0f;
      myRigidbody.velocity = new Vector2(0f, movementInput.y * climbSpeed);
      myAnimator.SetBool("isClimbing", Mathf.Abs(movementInput.y) > Mathf.Epsilon);
      myAnimator.speed = 1f;
    }
    if (context.canceled)
    {
      myRigidbody.velocity = Vector2.zero;
      myAnimator.speed = 0f;
    }
  }

  private void Run(Vector2 movementInput)
  {
    myRigidbody.velocity = new Vector2(movementInput.x * runSpeed, myRigidbody.velocity.y);

    RunningAnimation(movementInput.x);

    FlipSprite(movementInput.x);
  }

  private void FlipSprite(float movingDirection)
  {
    bool isRunningLeft = movingDirection < 0;
    mySpriteRenderer.flipX = isRunningLeft;
  }

  private void RunningAnimation(float movingDirection)
  {
    bool isPlayerRunning = Mathf.Abs(movingDirection) > Mathf.Epsilon;
    myAnimator.SetBool("isRunning", isPlayerRunning);
  }

  public void OnJumpInput(InputAction.CallbackContext context)
  {
    bool isGrounded = myCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Ladder"));

    if (!context.performed || !isGrounded)
    {
      return;
    }

    myRigidbody.gravityScale = 3f;
    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpSpeed);
    //myAnimator.SetBool("isJumping", true);

  }


}
