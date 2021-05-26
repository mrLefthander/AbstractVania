using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerInputHandler : MonoBehaviour
{
  [SerializeField]
  private float runSpeed = 5f;
  [SerializeField]
  private float jumpSpeed = 12f;
  [SerializeField]
  private float climbSpeed = 5f;
  [SerializeField]
  private SceneLoader sceneLoader;

  private Vector2 movementInput;
  private Rigidbody2D myRigidbody;
  private Animator myAnimator;
  private SpriteRenderer mySpriteRenderer;
  private CapsuleCollider2D myBodyCollider;
  private BoxCollider2D myFeetCollider;
 

  private bool isDead = false;

  private void Start()
  {
    myRigidbody = GetComponent<Rigidbody2D>();
    myAnimator = GetComponent<Animator>();
    mySpriteRenderer = GetComponent<SpriteRenderer>();
    myBodyCollider = GetComponent<CapsuleCollider2D>();
    myFeetCollider = GetComponent<BoxCollider2D>();

    if (sceneLoader != null) { return; }
    sceneLoader = FindObjectOfType<SceneLoader>();
  }

  private void Update()
  {
    Die();
  }

  public void OnMoveInput(InputAction.CallbackContext context)
  {
    if(isDead) { return; }
    
    movementInput = context.ReadValue<Vector2>();
    ClimbLadder(context);
    Run(movementInput);
  }

  private void ClimbLadder(InputAction.CallbackContext context)
  {
    movementInput = context.ReadValue<Vector2>();
    bool isTouchingLadder = myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    if (!isTouchingLadder)
    {
      myAnimator.SetBool("isClimbing", false);
      myRigidbody.gravityScale = 3f;
      myAnimator.speed = 1f;
      return;
    }
    if (Mathf.Abs(movementInput.y) > 0f && isTouchingLadder)
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
    if (isDead) 
    {
      sceneLoader.ReloadCurrentScene();
    }
    bool isGrounded = myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Ladder"));

    if (!context.performed || !isGrounded) { return; }

    myRigidbody.gravityScale = 3f;
    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpSpeed);
  }

  private void Die()
  {
    if (!IsTouchingDeadlyObject()) { return; }
    isDead = true;
    myAnimator.SetBool("isDead", isDead);
    myRigidbody.velocity = Vector2.up * 2f;
    myRigidbody.bodyType = RigidbodyType2D.Kinematic;
    myBodyCollider.enabled = false;
    myFeetCollider.enabled = false;
  }

  private bool IsTouchingDeadlyObject()
  {
    return myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")) ||
          myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard"));
  }
}
