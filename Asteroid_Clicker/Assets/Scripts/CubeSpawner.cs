using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private int m_StartingCubesCount = 5;
    [SerializeField] private int m_MaxCubes = 10;

    [SerializeField] private GameObject m_CubePrefab = null;
    [SerializeField] private BoxCollider m_SpawnArea = null;

    private readonly int MAX_ATTEMPTS_PER_SPAWN = 100;

    private GameObject m_LoadedCube = null;
    float[] m_SpawnRanges = new float[4];

    private void Start()
    {
        UnityEngine.Assertions.Assert.IsNotNull(m_CubePrefab, "CubePrefab is null");
        UnityEngine.Assertions.Assert.IsNotNull(m_SpawnArea, "SpawnArea is null");

        SetCubePrefabToLoad();
        SetupCubesSpawning();
    }

    private void SetCubePrefabToLoad()
    {
        string PrefabPath = UnityEditor.AssetDatabase.GetAssetPath(m_CubePrefab);
        PrefabPath = PrefabPath.Substring("Assets/Resources/".Length).Split('.')[0];
        m_LoadedCube = Resources.Load(PrefabPath) as GameObject;

        UnityEngine.Assertions.Assert.IsNotNull(m_LoadedCube, "LoadedCube is null");
    }

    private void SetupCubesSpawning()
    {
        float[] CubeScaleValues = { m_LoadedCube.transform.localScale.x, m_LoadedCube.transform.localScale.y, m_LoadedCube.transform.localScale.z };
        float LoadedObjectSizeMargin = Mathf.Max(CubeScaleValues);

        Bounds ColliderBounds = m_SpawnArea.bounds;
        Vector3 ColliderCenter = ColliderBounds.center;

        m_SpawnRanges[0] = ColliderCenter.x - ColliderBounds.extents.x + LoadedObjectSizeMargin;
        m_SpawnRanges[1] = ColliderCenter.x + ColliderBounds.extents.x - LoadedObjectSizeMargin;
        m_SpawnRanges[2] = ColliderCenter.z - ColliderBounds.extents.z + LoadedObjectSizeMargin;
        m_SpawnRanges[3] = ColliderCenter.z + ColliderBounds.extents.z - LoadedObjectSizeMargin;

        for (int i = 0; i < m_StartingCubesCount; i++)
        {
            SpawnCube();
        }
    }

    public void SpawnCube()
    {
        if (GameManager.Get().m_CubesCount >= m_MaxCubes)
            return;

        Vector3 RandomPosition;
        Vector3 RandomRotation;
        bool ValidPosition = FindSpotToSpawnCube(out RandomPosition, out RandomRotation);

        if (ValidPosition)
        {
            GameObject SpawnedCube = Instantiate(m_CubePrefab, RandomPosition, Quaternion.Euler(RandomRotation), this.transform);
            ++GameManager.Get().m_CubesCount;
        }        
    }

    private bool FindSpotToSpawnCube(out Vector3 RandomPosition, out Vector3 RandomRotation)
    {
        bool ValidPosition = false;
        int SpawnAttempts = 0;

        RandomPosition = new Vector3();
        RandomRotation = new Vector3();

        while (!ValidPosition && SpawnAttempts < MAX_ATTEMPTS_PER_SPAWN)
        {
            ValidPosition = true;
            float RandomX = Random.Range(m_SpawnRanges[0], m_SpawnRanges[1]);
            float RandomZ = Random.Range(m_SpawnRanges[2], m_SpawnRanges[3]);

            RandomPosition = new Vector3(RandomX, this.transform.position.y, RandomZ);
            RandomRotation.y = Random.Range(.0f, 360.0f);

            Collider[] Colliderboxes = Physics.OverlapBox(RandomPosition, m_CubePrefab.transform.localScale, Quaternion.Euler(RandomRotation), 1 << 6);
            ValidPosition = Colliderboxes.Length == 0;

            ++SpawnAttempts;
        }

        return ValidPosition;
    }
}
