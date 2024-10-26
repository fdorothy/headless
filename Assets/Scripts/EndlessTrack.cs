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
        } else
        {
            mp.transform.localPosition = Vector3.zero;
        }
        meshPaths.Add(mp);
    }

    void ReuseMeshPath(MeshPath meshPath)
    {
        meshPath.transform.position = meshPaths[meshPaths.Count - 1].transform.position + Vector3.right * height;
        meshPaths.Add(meshPath);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < meshPaths.Count; i++)
        {
            MeshPath mp = meshPaths[i];
            Transform obj = mp.transform;
            obj.position += Vector3.left * Time.deltaTime * Game.singleton.speed;
            if (obj.position.x < -height*2)
            {
                meshPaths.RemoveAt(i);
                ReuseMeshPath(mp);
            }
        }
    }
}
