using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Transform m_AlignToFloorTransform = null;
    [SerializeField] private Color[] m_ColorsOrder;

    private int m_CurrentColorIndex = -1;
    private MeshRenderer m_CubeMeshRenderer = null;

    private void Start()
    {
        RaycastHit RaycastHitInfo;
        Physics.Raycast(transform.position, -transform.up, out RaycastHitInfo);

        transform.position = RaycastHitInfo.point - m_AlignToFloorTransform.transform.localPosition;

        m_CubeMeshRenderer = GetComponent<MeshRenderer>();
    }

    public void IncrementCubeColor()
    {
        ++m_CurrentColorIndex;
        if (m_CurrentColorIndex < m_ColorsOrder.Length)
            m_CubeMeshRenderer.material.color = m_ColorsOrder[m_CurrentColorIndex];
        else
        {
            GameManager.Get().IncrementPoints();
            --GameManager.Get().m_CubesCount;
            Destroy(this.gameObject);
        }
            
    }
}
