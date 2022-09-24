using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Camera_Move : MonoBehaviour
{
    public Char_Controller player;
    float xmove = 2; // X축 누적 이동량 
    float ymove = 25; // Y축 누적 이동량 
    float distance = 3f;    
    private int toggleView = 3; // 1=1인칭, 3=3인칭 
    private float wheelspeed = 10.0f;
    private Vector3 Player_Height;
    private Vector3 Player_Side;
    void Start()
    {
        //캐릭터의 크기 설정을 통해 카메라 시점 조정
        Player_Height = new Vector3(0, 1.1f, 0f);
        Player_Side = new Vector3(-1f, 0f, 0f);
    }
        
    void Update()
    {
        // 마우스 우클릭 중에만 카메라 무빙 적용 
        if (Input.GetMouseButton(1))
        {
            xmove += Input.GetAxis("Mouse X");
            // 마우스의 좌우 이동량을 xmove 에 누적
            ymove -= Input.GetAxis("Mouse Y");
            // 마우스의 상하 이동량을 ymove 에 누적 
        }
        // 이동량에 따라 카메라의 바라보는 방향을 조정
        transform.rotation = Quaternion.Euler(ymove, xmove, 0);  
        //Q버튼으로 시점변경
        if (Input.GetKeyDown(KeyCode.Q))
            toggleView = 4 - toggleView;
        //3인칭일때
        if (toggleView == 3)
        {
            //-를 통해 위로 굴렸을때 가까워지고, 아래로 굴렸을때 멀어짐
            distance -= Input.GetAxis("Mouse ScrollWheel") * wheelspeed;
            //카메라 거리의 최소값과 최대값 설정
            if (distance < 3f) distance = 3f;
            if (distance > 15.0f) distance = 15.0f;
        }
        //1인칭일때
        if (toggleView == 1)
        {
            Vector3 Eye = player.transform.position + Player_Height;
            Vector3 reverseDistance = new Vector3(0.0f, 0.0f, 0.5f);
            // 카메라가 바라보는 앞방향은 Z 축
            // 이동량에 따른 Z 축방향의 벡터 
            transform.position = Eye + transform.rotation * reverseDistance;
            // 플레이어의 위치에서 카메라가 바라보는 방향에 벡터값을 적용한 상대 좌표를 표시 
        }
        else if (toggleView == 3)
        {
            Vector3 Eye = player.transform.position
                + transform.rotation * Player_Side + Player_Height;
            Vector3 reverseDistance = new Vector3(0.0f, 0.0f, distance);            
            transform.position = Eye - transform.rotation * reverseDistance;
        }
    }
}
