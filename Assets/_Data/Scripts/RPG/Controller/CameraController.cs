using UnityEngine;
using Cinemachine;


namespace RPG.Controller
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] GameObject freeLookCamera;
        CinemachineFreeLook freeLookComponent;
        PlayerController playerControllerScript;

        private void Awake()
        {
            freeLookComponent = freeLookCamera.GetComponent<CinemachineFreeLook>();
            playerControllerScript = GetComponent<PlayerController>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (playerControllerScript.InteractWithUI()) return;

                // use the following line for mouse control of zoom instead of mouse wheel
                // be sure to change Input Axis Name on the Y axis to "Mouse Y"

                //freeLookComponent.m_YAxis.m_MaxSpeed = 10;
                freeLookComponent.m_XAxis.m_MaxSpeed = 500;
            }
            if (Input.GetMouseButtonUp(1))
            {
                // use the following line for mouse control of zoom instead of mouse wheel
                // be sure to change Input Axis Name on the Y axis from to "Mouse Y"

                //freeLookComponent.m_YAxis.m_MaxSpeed = 0;
                freeLookComponent.m_XAxis.m_MaxSpeed = 0;
            }

            // wheel zoom //
            // comment out the below if condition if you are using mouse control for zoom
            if (Input.mouseScrollDelta.y != 0)
            {
                freeLookComponent.m_YAxis.m_MaxSpeed = 10;
            }
        }
    }
}