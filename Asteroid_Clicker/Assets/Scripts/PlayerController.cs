using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//rotating camera with mouse

//sprawdzic czy da sie uruchomic projekt z repo
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_MovementSpeed = .0f;
    [SerializeField] private GameObject m_CameraObject = null;

    private readonly float CAMERA_ROTATION_SPEED = 150f;
    private Cube m_CurrentlyTriggeredCubeScript = null;

    private void Start()
    {
        UnityEngine.Assertions.Assert.IsNotNull(m_CameraObject, "CameraObject is null");

        GameManager.Get().ResetGameState();
    }

    private void Update()
    {
        ProcessInput();

        if (m_CurrentlyTriggeredCubeScript)
            m_CurrentlyTriggeredCubeScript.FaceTextMeshToPlayer(this.gameObject);  
    }

    private void OnTriggerEnter(Collider other)
    {
        m_CurrentlyTriggeredCubeScript = other.GetComponent<Cube>();
        if (m_CurrentlyTriggeredCubeScript)
            m_CurrentlyTriggeredCubeScript.SetTextMeshActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_CurrentlyTriggeredCubeScript)
            m_CurrentlyTriggeredCubeScript.SetTextMeshActive(false);
        m_CurrentlyTriggeredCubeScript = null;
    }
    
    private void IncrementCubeColor()
    {
        if (!m_CurrentlyTriggeredCubeScript)
            return;

        m_CurrentlyTriggeredCubeScript.IncrementCubeColor();
    }

    private void ProcessInput()
    {
        ProcessCameraRotation();
        ProcessMovement();
    }

    private void ProcessCameraRotation()
    {
        if (Input.GetAxis("Mouse X") != 0)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * CAMERA_ROTATION_SPEED, 0);
        }
        else if (Input.GetButton("Rotation"))
        {
            transform.Rotate(0, Input.GetAxis("Rotation") * Time.deltaTime * CAMERA_ROTATION_SPEED, 0);
        }
    }

    private void ProcessMovement()
    {
        Vector3 ForwardDirection = m_CameraObject.transform.forward;
        Vector3 RightDirection = m_CameraObject.transform.right;

        if(Input.GetButton("Vertical"))
        {
            transform.localPosition += Input.GetAxis("Vertical") * ForwardDirection * m_MovementSpeed * Time.deltaTime;
        }
        if (Input.GetButton("Horizontal"))
        {
            transform.localPosition += Input.GetAxis("Horizontal") * RightDirection * m_MovementSpeed * Time.deltaTime;
        }

        if (Input.GetButtonUp("Spawn"))
        {
            GameManager.Get().SpawnCube();
        }

        if (Input.GetButtonUp("IncrementColor"))
        {
            IncrementCubeColor();
        }        
    }
}