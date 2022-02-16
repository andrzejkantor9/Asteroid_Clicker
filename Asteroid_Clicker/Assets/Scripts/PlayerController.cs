using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//gamestate class?

//scalable ui
//getting points of cube destroy
//_______________________________________
//block input on collision till released
//setup properly in input settings
//move all input to player controller
//rotating camera with mouse

//encapsulate
//same case everywhere
//asserts everywhere
//comments
//layermasks
//interfejsy, dziedziczenie?
//uzyc get component gdzie ma sens i nie uzywac gdzie nie ma
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_MovementSpeed = .0f;
    [SerializeField] private GameObject m_CameraObject = null;

    private Collider m_CurrentlyTriggeredCubeCollider = null;

    private void Start()
    {
        GameManager.Get().ResetGameState();
    }

    private void Update()
    {        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * 100, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 100, Space.World);
        }

        var ForwardDirection = m_CameraObject.transform.forward;
        var RightDirection = m_CameraObject.transform.right;
        
        if (Input.GetKey(KeyCode.W))
        {            
            transform.position += (ForwardDirection * m_MovementSpeed) *Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += (-ForwardDirection * m_MovementSpeed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.position += (RightDirection * m_MovementSpeed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position += (-RightDirection * m_MovementSpeed )* Time.deltaTime;
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {

        }

        if(Input.GetKeyUp(KeyCode.R))
        {
            IncrementCubeColor();            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        m_CurrentlyTriggeredCubeCollider = other;
    }

    private void OnTriggerExit(Collider other)
    {
        m_CurrentlyTriggeredCubeCollider = null;
    }

    //red, green, blue, destroy
    private void IncrementCubeColor()
    {
        if (!m_CurrentlyTriggeredCubeCollider)
            return;

        m_CurrentlyTriggeredCubeCollider.GetComponent<Cube>().IncrementCubeColor();
    }
}