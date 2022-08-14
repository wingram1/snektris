using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // controls direction
    private Vector2 _direction = Vector2.right;

    // controls segments
    private List<Transform> _segments;
    public Transform segmentPrefab;

    private void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && _direction != Vector2.down)
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.A) && _direction != Vector2.right)
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.S) && _direction != Vector2.up)
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D) && _direction != Vector2.left)
        {
            _direction = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        // iterate in reverse order to move segments up
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            // assign new position to the one in front
            _segments[i].position = _segments[i - 1].position;
        }

        // move head forward
        this.transform.position = new Vector3(
            // xyz change
            this.transform.position.x + _direction.x,
            this.transform.position.y + _direction.y,
            0.0f
        );

        if (_segments.Count <= 3)
        {
           Grow(); 
        }
        
    }

    private void Grow()
    {
        // create segment object
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    private void ResetState()
    {
        // loop through segments to destroy
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        this.transform.position = Vector3.zero;
    }

    // Collision check
    private void OnTiggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle") {
            // Freeze & fall
            ResetState();
        }
    }
}
