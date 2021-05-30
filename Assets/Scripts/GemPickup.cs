using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPickup : MonoBehaviour
{
  [SerializeField]
  private int gemsAmount = 1;
  [SerializeField]
  private AudioClip gemPickupSound;



  private void OnTriggerEnter2D(Collider2D collision)
  {
    GameSession.instance.AddGems(gemsAmount);
    AudioSource.PlayClipAtPoint(gemPickupSound, Camera.main.transform.position);
    Destroy(gameObject);
  }
}
