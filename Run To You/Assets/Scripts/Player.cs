using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    
    private float speed = 15.0f;
    private float verticalVelocity = 0.0f;
    private Vector3 moveVector;
    private CharacterController controller;
    private float gravity = 12.0f;
    private float animationDuration = 3.0f;
    private bool isDead = false;
    private float startTime;
    public TileManager tileManager;
    
   




  

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        startTime = Time.time;
        


    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (isDead)
        {
            return;
        }
        if (Time.time - startTime < animationDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }
        moveVector = Vector3.zero;

        if (controller.isGrounded)
        {
            verticalVelocity = 3.0f;
           
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
           
        }
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        moveVector.y = verticalVelocity;
        moveVector.z = speed;
        controller.Move(moveVector * Time.deltaTime);

    }
  
    



    public void SetSpeed(float modifire)
    {
        speed = 15.0f + modifire;
    }
    
   



    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Prefabs")
        {
            //Debug.Log("Ãæµ¹");
            Death();
        }
    }
    private void Death()
    {
        isDead = true;
        GetComponent<Score>().OnDeath();
        //Debug.Log("Á×À½");
    }
}
