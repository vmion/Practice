using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Controller : MonoBehaviour
{
    private Rigidbody rb;
    Vector3 end;
    float jumpPower;
    float moveSpeed;
    private int IsJumping;
    float rotateSpeed;       

    void Start()
    {        
        //리지드바디 컴포넌트를 받아옴
        rb = GetComponent<Rigidbody>();
        IsJumping = 0;
        end = transform.position;
        jumpPower = 7f;
        moveSpeed = 2f;
        rotateSpeed = 50f;
       
    }
    private void FixedUpdate()
    {       
        Walk();
        Jump();
        Run();
                
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position), Time.deltaTime * rotateSpeed);        
        
    }

    void Update()
    {
       
    }
    void Walk()
    {        
        //WASD로 기본이동
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * moveSpeed * Time.deltaTime;
        }
    }
    void Run()
    {
        //LeftShift를 누르는 동안 달리기
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = 3.5f;
                jumpPower = 15f;
            }
            else
            {
                moveSpeed = 2f;
                jumpPower = 7f;
            }
        }                  
    }
    void Jump()
    {
        //스페이스바를 누를시 점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //IsJumping이 0일때 점프
            if(IsJumping < 2)
            {                 
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                IsJumping++;
            }
            //2이상일 때는 점프 불가
            else if(IsJumping > 2)
            {
                return;
            }
        }
    }
    //바닥과 충돌시 IsJumping이 0으로 바뀌면서 점프가능
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Terrain"))
        {
            IsJumping = 0;
        }
    }
}
