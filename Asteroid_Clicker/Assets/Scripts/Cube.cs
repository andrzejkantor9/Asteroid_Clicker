using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//assert jesli aligntofloor jest nullem
//layers at raycasts?
public class Cube : MonoBehaviour
{
    [SerializeField] private Transform m_AlignToFloorTransform = null;

    private void Awake()
    {
        RaycastHit RaycastHitInfo;
        Physics.Raycast(transform.position, -transform.up, out RaycastHitInfo);

        transform.position = RaycastHitInfo.point - m_AlignToFloorTransform.transform.localPosition;
        //transform.position = RaycastHitInfo.point;
    }
}
