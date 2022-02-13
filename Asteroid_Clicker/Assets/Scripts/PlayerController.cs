using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//setup properly in input settings
//rotating camera with mouse
//separate cube spawner class
//block input on collision till released
//asserty
//design better variables
//gamestate class?
//start vs awake
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_movementIntensity = .0f;
    [SerializeField] private GameObject m_cameraTarget = null;
    //[SerializeField] private Transform m_CubeSpawnTransform = null;
    //[SerializeField] private BoxCollider m_FloorCollider = null;
    private Rigidbody m_rigidBody = null;

    private void Awake()
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