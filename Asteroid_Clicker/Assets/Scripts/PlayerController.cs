using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//change color on LMB and destroy it
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
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_movementIntensity = .0f;
    [SerializeField] private GameObject m_cameraTarget = null;
    private Rigidbody m_rigidBody = null;

    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
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

        var ForwardDirection = m_cameraTarget.transform.forward;
        var RightDirection = m_cameraTarget.transform.right;
        
        if (Input.GetKey(KeyCode.W))
        {            
            transform.position += (ForwardDirection * m_movementIntensity) *Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += (-ForwardDirection * m_movementIntensity) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.position += (RightDirection * m_movementIntensity) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position += (-RightDirection * m_movementIntensity )* Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.Space))
        {

        }
    }
}