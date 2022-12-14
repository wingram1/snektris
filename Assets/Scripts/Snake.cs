using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // controls direction
    private Vector2 _direction = Vector2.down;

    // controls segments
    private List<Transform> _segments;
    public Transform segmentPrefab;

    private List<Transform> _blocks;
    public Transform blockPrefab;

    private void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
    }

    private void Update()
    {
        // get WASD direction
        // TODO: bug where if inputting side=>back in quick succession, snake runs into itself immediately
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

        // Grow until default length of 4
        if (_segments.Count <= 3)
        {
           Grow(); 
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

        // Debug.Log(this.transform.position);
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
        _blocks = new List<Transform>();
        _blocks.Add(this.transform);
        

        // loop through segments to convert snake to block
        for (int i = 1; i < _segments.Count; i++)
        {
            // generate block
            Transform block = Instantiate(this.blockPrefab);
            block.position = _segments[i].position;

            // destroy segment
            Destroy(_segments[i].gameObject);

            _blocks.Add(block);
        }
        _blocks.Add(this.transform);

        _segments.Clear();
        _segments.Add(this.transform);

        Debug.Log(_blocks.Count);

        // Snake respawn at top middle
        this.transform.position = new Vector3(
            0.5f,
            9.5f,
            0.0f
        );
        _direction = Vector2.down;
    }

    // Collision check
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle") {
            Debug.Log("Collision with obstacle");
            

            // generate tetromino using snake blocks
            // when tetronimo has touched ground, reset snake

            ResetState();
        }
    }
}
