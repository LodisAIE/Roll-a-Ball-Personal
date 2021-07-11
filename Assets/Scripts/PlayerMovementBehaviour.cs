using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Include the namespace required to use Unity UI and Input System
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;

public class PlayerMovementBehaviour : MonoBehaviour
{
    // Create public variables for player speed, and for the Text UI game objects
    public float speed = 0;
    public Text countText;
    public GameObject winTextObject;

    private Rigidbody _rigidbody;
    private float _movementX;
    private float _movementY;
    private int _count;

    // Start is called before the first frame update
    void Start()
    {
        // Assign the Rigidbody component to our private _rigidbody variable
        _rigidbody = GetComponent<Rigidbody>();
        
        // Set the count to zero
        _count = 0;

        SetCountText();

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winTextObject.SetActive(false);
    }

    private void SetCountText()
    {
        countText.text = "Count: " + _count.ToString();

        if (_count >= 10)
        {
            //Set the text value of your 'winText'
            winTextObject.SetActive(true);
        }
    }

    // Update is called once per frame
    private void OnMove(InputValue movementValue)
    {
        Vector2 moveDirection = movementValue.Get<Vector2>();
        _movementX = moveDirection.x;
        _movementY = moveDirection.y;
    }

    private void FixedUpdate()
    {
        // Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
        Vector3 movement = new Vector3(_movementX, 0.0f, _movementY);

        _rigidbody.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the GameObject you intersect has the tag 'Pick Up' assigned to it..
        if (other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);

            // Add one to the score variable 'count'
            _count = _count + 1;

            // Run the 'SetCountText()' function (see below)
            SetCountText();
        }
    }
}
