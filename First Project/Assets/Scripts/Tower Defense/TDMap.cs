using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDMap : MonoBehaviour
{
    public HexGPUMesh meshPrefab;
    public List<HexGPUMesh> layers;

    private float layerSpacing = -0.1f;

    public void Start()
    {
        CreateLayers();
    }

    private void CreateLayers()
    {
        for (int i = 0; i < 5; i++)
        {
            var mesh = Instantiate(meshPrefab);

            mesh.transform.position = transform.position + new Vector3(0, 0, layerSpacing * i);
            mesh.transform.parent = transform;
            mesh.name = "Layer " + i;

            layers.Add(mesh);
        }
    }

    private void AddLayer()
    {
        var mesh = Instantiate(meshPrefab);

        mesh.transform.position = transform.position + new Vector3(0, 0, layerSpacing * layers.Count);
        mesh.transform.parent = transform;
        mesh.name = "Layer " + layers.Count;

        layers.Add(mesh);
    }

    private void SetTexture(Vector2Int position, int layerIndex, Texture2D texture)
    {
        int loops = 0;
        while(layers.Count <= layerIndex || loops > 100)
        {
            AddLayer();
            loops++;
        }

        layers[layerIndex].SetTexture(position, texture);

    }
}
