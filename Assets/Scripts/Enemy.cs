using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{
    [SerializeField] private float maxspeed;
    private int direction = 1;
    [SerializeField] private int hurtPower;


    private RaycastHit2D rightLedgeRaycastHit;
    private RaycastHit2D lefttLedgeRaycastHit;
    private RaycastHit2D rightWallRaycastHit;
    private RaycastHit2D leftWallRaycastHit;
    [SerializeField] private Vector2 rayCastOffset;
    [SerializeField] private float rayCastLength;
    [SerializeField] LayerMask raycastLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(maxspeed*direction, 0);
        rightLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x + rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down, rayCastLength);
        Debug.DrawRay(new Vector2(transform.position.x + rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down * rayCastLength, Color.blue);
        if (rightLedgeRaycastHit.collider == null)
        {
            Debug.Log("Oh No!");
            direction = -1;
        }
        rightWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, rayCastLength, raycastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.right * rayCastLength, Color.blue);
        if (rightWallRaycastHit.collider != null)
        {
            Debug.Log("Oh No!");
            direction = -1;
        }

        lefttLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x - rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down, rayCastLength);
        Debug.DrawRay(new Vector2(transform.position.x - rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down * rayCastLength, Color.green);
        if (lefttLedgeRaycastHit.collider == null)
        {
            Debug.Log("Oh No!");
            direction = 1;
        }

        leftWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, rayCastLength, raycastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.left * rayCastLength, Color.green);
        if (leftWallRaycastHit.collider != null)
        {
            Debug.Log("Oh No!");
            direction = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == NewPlayer.Instance.gameObject)
        {
            Debug.Log("Hurting Player");
            NewPlayer.Instance.health -= hurtPower;
            NewPlayer.Instance.UpdateUI();
        }
    }
}
