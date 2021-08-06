using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using TMPro;

public class Controller : MonoBehaviour
{
    public float speed = 0;
    private int count;
    public TextMeshProUGUI countText;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private float jump;
    public GameObject winTextObject;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }


    void OnMove(InputValue movementValue)
    {

        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 134)
        {
            winTextObject.SetActive(true);
        }
    }


    void FixedUpdate()
    {


         if(Input.GetKey(KeyCode.Space))
        {
            jump = 10f;
        }
        else
        {
            jump = 0;
        }

        Vector3 movement = new Vector3(movementX, jump, movementY);

        rb.AddForce(movement * speed);

        if(Input.GetKey(KeyCode.B))
        {
            Vector3 boost = new Vector3((movementX * 30), 0.0f, (movementY * 30));

            rb.AddForce(boost);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }
}
