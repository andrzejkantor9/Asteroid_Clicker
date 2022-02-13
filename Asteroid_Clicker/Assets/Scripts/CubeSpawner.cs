using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//asserty
//move input to player controller
//safeguard to not spawn within other colliders
//safeguard to one time click
//move most of update out of update
public class CubeSpawner : MonoBehaviour
{

    [SerializeField] private GameObject m_CubePrefab = null;
    [SerializeField] private BoxCollider m_FloorCollider = null;

    private Object m_LoadedObject = null;

    private void Awake()
    {
        string prefabPath = UnityEditor.AssetDatabase.GetAssetPath(m_CubePrefab);
        prefabPath = prefabPath.Substring("Assets/Resources/".Length);
        prefabPath = prefabPath.Split('.')[0];
        m_LoadedObject = Resources.Load(prefabPath);
        //Object CubeObject = Instantiate(m_LoadedObject, this.transform);


        //Vector3 spawnableItemLocalScale = spawnableItem.transform.localScale;

        //float spawnableItemSizeX = spawnableItemLocalScale.x / 2;
        //float spawnableItemSizeY = spawnableItemLocalScale.y / 2;


    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Bounds colliderBounds = m_FloorCollider.bounds;
            Vector3 colliderCenter = colliderBounds.center;

            float[] ranges = {
                (colliderCenter.x - colliderBounds.extents.x),// + spawnableItemSizeX,
                (colliderCenter.x + colliderBounds.extents.x),// - spawnableItemSizeX,
                (colliderCenter.z - colliderBounds.extents.z),// + spawnableItemSizeY,
                (colliderCenter.z + colliderBounds.extents.z)// - spawnableItemSizeY,
            };
            float randomX = Random.Range(ranges[0], ranges[1]);
            float randomZ = Random.Range(ranges[2], ranges[3]);

            Vector3 randomPos = new Vector3(randomX, this.transform.position.y, randomZ);

            Instantiate(m_CubePrefab, randomPos, Quaternion.identity);
        }
    }
}
