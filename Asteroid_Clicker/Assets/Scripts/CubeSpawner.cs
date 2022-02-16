using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private readonly int MAX_ATTEMPTS_PER_SPAWN = 100;
    private readonly int STARTING_CUBES = 5;
    private readonly int MAX_CUBES = 10;

    [SerializeField] private GameObject m_CubePrefab = null;
    [SerializeField] private BoxCollider m_FloorCollider = null;

    private GameObject m_LoadedObject = null;
    private float m_LoadedObjectSizeMargin = .0f;
    float[] m_Ranges = new float[4];

    int m_SpawnedCubesCount = 0;

    private void Start()
    {
        string prefabPath = UnityEditor.AssetDatabase.GetAssetPath(m_CubePrefab);
        prefabPath = prefabPath.Substring("Assets/Resources/".Length).Split('.')[0];
        m_LoadedObject = Resources.Load(prefabPath) as GameObject;

        float[] CubeScaleValues = { m_LoadedObject.transform.localScale.x, m_LoadedObject.transform.localScale.y, m_LoadedObject.transform.localScale.z};        
        m_LoadedObjectSizeMargin = Mathf.Max(CubeScaleValues);

        Bounds ColliderBounds = m_FloorCollider.bounds;
        Vector3 ColliderCenter = ColliderBounds.center;

        m_Ranges[0] = ColliderCenter.x - ColliderBounds.extents.x + m_LoadedObjectSizeMargin;
        m_Ranges[1] = ColliderCenter.x + ColliderBounds.extents.x - m_LoadedObjectSizeMargin;
        m_Ranges[2] = ColliderCenter.z - ColliderBounds.extents.z + m_LoadedObjectSizeMargin;
        m_Ranges[3] = ColliderCenter.z + ColliderBounds.extents.z - m_LoadedObjectSizeMargin;

        for (int i = 0; i < STARTING_CUBES; i++)
        {
            SpawnCube();
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SpawnCube();
        }
    }

    private void SpawnCube()
    {
        if (m_SpawnedCubesCount >= MAX_CUBES)
            return;

        bool ValidPosition = false;
        int SpawnAttempts = 0;

        Vector3 RandomPosition = new Vector3();
        Vector3 RandomRotation = new Vector3();
        while (!ValidPosition && SpawnAttempts < MAX_ATTEMPTS_PER_SPAWN)
        {
            ValidPosition = true;
            float RandomX = Random.Range(m_Ranges[0], m_Ranges[1]);
            float RandomZ = Random.Range(m_Ranges[2], m_Ranges[3]);

            RandomPosition = new Vector3(RandomX, this.transform.position.y, RandomZ);
            RandomRotation.y = Random.Range(.0f, 360.0f);

            //Bounds SpawnedCubeBounds = m_CubePrefab.GetComponent<BoxCollider>().bounds;
            var Colliderboxes = Physics.OverlapBox(RandomPosition, new Vector3(1f, 1f, 1f), Quaternion.Euler(RandomRotation), 1 << 6);
            //var Colliderboxes = Physics.OverlapBox(RandomPosition, SpawnedCubeBounds.size, Quaternion.identity, 1 << 6);
            ValidPosition = Colliderboxes.Length <= 0;
            
            ++SpawnAttempts;
            //Debug.Log($", colliderboxes count: {Colliderboxes.Length.ToString()}");
        }
        //Debug.Log($"spawnattempts: {SpawnAttempts.ToString()}");

        if (ValidPosition)
        {
            GameObject SpawnedCube = Instantiate(m_CubePrefab, RandomPosition, Quaternion.Euler(RandomRotation), this.transform);
        }        
    }
}
