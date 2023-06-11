using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    ComputeBuffer buffer;
    // Start is called before the first frame update
    void Start()
    {
        buffer = ComputeHelper.CreateAndSetBuffer<float>(256, null, "name", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        ComputeHelper.Release(buffer);
    }
}
