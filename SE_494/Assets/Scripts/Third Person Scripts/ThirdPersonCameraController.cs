using Cinemachine;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public CinemachineFreeLook cinemachineFreeLook;
    public float maxSpeedXaxis;
    public float maxSpeedYaXis;
    
    
    void Update()
    {
        
        //When player pressed mouse button 1, player can change camera axis with moving mouse 
        if (Input.GetMouseButton(1))
        {
            cinemachineFreeLook.m_YAxis.m_MaxSpeed = maxSpeedYaXis;
            cinemachineFreeLook.m_XAxis.m_MaxSpeed = maxSpeedXaxis;

        }
        else
        {
            cinemachineFreeLook.m_YAxis.m_MaxSpeed = 0;
            cinemachineFreeLook.m_XAxis.m_MaxSpeed = 0;
        }
        
    }

    
}
