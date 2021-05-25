using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyMovement : MonoBehaviour
{
  [SerializeField]
  private float moveSpeed = 1f;

  private Rigidbody2D myRigidBody;

  void Start()
  {
    myRigidBody = GetComponent<Rigidbody2D>();
    myRigidBody.velocity = new Vector2(moveSpeed, 0f);
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    transform.localScale = new Vector2(-(Mathf.Sign(transform.localScale.x)), 1f);
    myRigidBody.velocity *= Vector2.left;
  }

}
