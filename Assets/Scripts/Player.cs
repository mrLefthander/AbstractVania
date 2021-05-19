using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
  [SerializeField] 
  private float runSpeed = 5f;
  private Rigidbody2D myRigidBody;

  void Start()
  {
    myRigidBody = GetComponent<Rigidbody2D>();
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
  }
}
