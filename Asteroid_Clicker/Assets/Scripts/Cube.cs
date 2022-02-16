using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Transform m_AlignToFloorTransform = null;

    private void Start()
    {
        RaycastHit RaycastHitInfo;
        Physics.Raycast(transform.position, -transform.up, out RaycastHitInfo);

        transform.position = RaycastHitInfo.point - m_AlignToFloorTransform.transform.localPosition;
    }
}
