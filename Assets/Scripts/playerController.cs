using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    public float speed = 0;

    //create a slider to adjust the variable
    //only affect the varible bellow this line
    [Range(0.01f, 0.99f)]
    public float smoothness = 0.5f;
    private Rigidbody r_body;
    private float move_x;
    private float move_y;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        //connect the r_body to the player object's rigidbody component
        r_body = GetComponent<Rigidbody>();
        count = 0;
    }

    //Send data to the game and move the object
    //InputValue is the type of the variable
    //movement is the name I set for the variable 
    void OnMove(InputValue movement)
    {
        //save a 2D position in 'movementVector' variable
        //Vector2 has 2 value (x,y)
        Vector2 movementVector = movement.Get<Vector2>();
        move_x = movementVector.x;
        move_y = movementVector.y;

    }

    void FixedUpdate()
    {
        //if(x==1 && y==0)
        //use Approximately because move variable are float number
        //
        if(Mathf.Approximately(move_x,0f) && Mathf.Approximately(move_y,0f) )
        {
            r_body.velocity = r_body.velocity * smoothness;
            return;//stop the function
        }

        //the player move on x&z aix in 3D space so y=0
        Vector3 movement = new Vector3(move_x, 0.0f, move_y);
        r_body.AddForce(movement * speed);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count ++;
        }
    }
}
