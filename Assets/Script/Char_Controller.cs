using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Controller : MonoBehaviour
{
    private Rigidbody rb;
    Vector3 dir;
    float jumpPower;
    float moveSpeed;
    private bool IsJumping;
    float rotateSpeed;
    Vector3 rotation;

    void Start()
    {
        //리지드바디 컴포넌트를 받아옴
        rb = GetComponent<Rigidbody>();
        IsJumping = false;
        
    }

    void Update()
    {
        float x = Input.GetAxis("Mouse X");
        
        jumpPower = 5f;
        moveSpeed = 2f;
        dir = Vector3.zero;
        rotateSpeed = 80f;
        rotation = transform.localEulerAngles;

        //WASD로 기본이동
        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            dir.z = 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir.z = -1f;
        }
        //LeftShift를 누르는 동안 달리기
        //뒤로 갈때는 달리기 안됨
        if(Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = 3.5f;
                jumpPower = 7f;
            }
        }        
        
        Vector3 tmp = transform.position;
        tmp += dir * Time.deltaTime * moveSpeed;
        transform.position = tmp;
        rotation.y += x * Time.deltaTime * rotateSpeed;
        
        transform.localEulerAngles = rotation;
        Jump();
    }
   
    void Jump()
    {
        //스페이스바를 누를시 점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //IsJumping이 false일때 true로 바뀌면서 점프 가능해짐
            if(!IsJumping)
            {                
                IsJumping = true;
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
            //true일때는 점프 불가
            else
            {
                return;
            }
        }
    }
    //바닥과 충돌시 IsJumping이 false로 바뀌면서 점프가능
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Terrain"))
        {
            IsJumping = false;
        }
    }
}
