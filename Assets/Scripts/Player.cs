using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
  [SerializeField] 
  private float runSpeed = 5f;

  bool isAlive = true;

  private Rigidbody2D myRigidBody;
  private Animator myAnimator;
  private SpriteRenderer mySpriteRenderer;

  void Start()
  {
    myRigidBody = GetComponent<Rigidbody2D>();
    myAnimator = GetComponent<Animator>();
    mySpriteRenderer = GetComponent<SpriteRenderer>();
  }

 
  void Update()
  {
    Run();
  }

  private void Run()
  {
    float controlThrow = Input.GetAxis("Horizontal");
    Vector2 playerVolecity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
    myRigidBody.velocity = playerVolecity;

    bool isPlayerRunning = Mathf.Abs(controlThrow) > Mathf.Epsilon;
    myAnimator.SetBool("isRunning", isPlayerRunning);


    FlipSprite(controlThrow);
  }

  private void FlipSprite(float controlThrow)
  {
    bool isRunningLeft = controlThrow < 0;
    mySpriteRenderer.flipX = isRunningLeft;
  }
}
