using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTrack : MonoBehaviour
{
    List<MeshPath> meshPaths = new List<MeshPath>();
    public MeshPath meshPathPrefab;
    float height = 20f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<5; i++)
        {
            AddMeshPath();
        }
    }

    void AddMeshPath()
    {
        MeshPath mp = Instantiate<MeshPath>(meshPathPrefab, transform);
        mp.height = height;
        if (meshPaths.Count > 0)
        {
            mp.transform.position = meshPaths[meshPaths.Count - 1].transform.position + Vector3.right * height;
            mp.offset = meshPaths[meshPaths.Count - 1].offset + height;
        } else
        {
            mp.transform.localPosition = Vector3.zero;
        }
        meshPaths.Add(mp);
    }

    void ReuseMeshPath(MeshPath meshPath)
    {
        MeshPath previousPath = meshPaths[meshPaths.Count - 1];
        meshPath.transform.position = previousPath.transform.position + Vector3.right * height;
        meshPath.offset = previousPath.offset + height;
        meshPath.RecalculateVertexPositions(true);
        meshPaths.Add(meshPath);
    }

    // Update is called once per frame
    void Update()
    {
        // move the paths
        for (int i = 0; i < meshPaths.Count; i++)
        {
            MeshPath mp = meshPaths[i];
            Transform obj = mp.transform;
            obj.position += Vector3.left * Time.deltaTime * Game.singleton.speed;
        }

        // reuse old paths
        for (int i = 0; i < meshPaths.Count; i++)
        {
            MeshPath mp = meshPaths[i];
            if (mp.transform.position.x < -height * 2)
            {
                meshPaths.RemoveAt(i);
                //Destroy(mp.gameObject);
                //AddMeshPath();
                ReuseMeshPath(mp);
            }
        }
    }
}
