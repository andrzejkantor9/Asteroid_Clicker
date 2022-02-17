using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Transform m_AlignToFloorTransform = null;
    [SerializeField] private Transform m_TextMeshTransform = null;

    [SerializeField] private Color[] m_ColorsOrder;

    private readonly float MAX_SPAWN_RAYCAST_DISTANCE = 100f;

    private int m_CurrentColorIndex = -1;
    private MeshRenderer m_CubeMeshRenderer = null;

    private void Start()
    {
        UnityEngine.Assertions.Assert.IsNotNull(m_AlignToFloorTransform, "AlignToFloorTransform is null");
        UnityEngine.Assertions.Assert.IsNotNull(m_TextMeshTransform, "TextMesh is null");

        m_CubeMeshRenderer = GetComponent<MeshRenderer>();
        UnityEngine.Assertions.Assert.IsNotNull(m_CubeMeshRenderer, "CubeMeshRenderer is null");

        AlignToFloor();
    }

    private void AlignToFloor()
    {
        RaycastHit RaycastHitInfo;
        Physics.Raycast(transform.position, -transform.up, out RaycastHitInfo, MAX_SPAWN_RAYCAST_DISTANCE, 1 << 7);

        transform.position = RaycastHitInfo.point - m_AlignToFloorTransform.transform.localPosition;
    }

    public void FaceTextMeshToPlayer(GameObject PlayerObject)
    {
        m_TextMeshTransform.LookAt(PlayerObject.transform);
    }

    public void SetTextMeshActive(bool Active)
    {
        m_TextMeshTransform.gameObject.SetActive(Active);
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
