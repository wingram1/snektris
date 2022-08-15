using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Block : MonoBehaviour
{
    // set direction
    private Vector2 _direction = Vector2.down;
    bool falling = true;

    private void Start()
    {
        Debug.Log("Block script is running.");
    }

    private void FixedUpdate()
    {
        if (falling == true)
        {
            this.transform.position = new Vector3(
                this.transform.position.x,
                this.transform.position.y + _direction.y,
                0.0f
            );
        }
    }

    // if collision with obstacle, stop
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            falling = false;

            // reset position one space earlier
            this.transform.position = new Vector3(
                this.transform.position.x,
                this.transform.position.y + 1,
                0.0f
            );
            // TODO: see if there's a way to group blocks together so you can tell them all to stop
        }
    }

}
