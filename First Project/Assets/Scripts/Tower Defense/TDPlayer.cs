using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDPlayer : MonoBehaviour
{
    public Camera cam;
    public HexGPUMesh gpuMesh;
    public Transform cursor;

    public Texture2D drawTexture;
    [Range(1, 5)]
    public int drawRadius = 1;

    [System.Serializable]
    public class HoverInfo
    {
        public Vector3 lastMousePosition;
        public Vector3 worldPosition;
        public Vector2Int lastPosition;
        public Vector2Int positon;
        public bool newPositionThisFrame;
        public bool inBounds;
        public int visibleIndex;
    }

    public HoverInfo hoverInfo = new HoverInfo();
    private Plane hitPlane = new Plane(Vector3.back, Vector3.zero);

    private void Start()
    {
    }

    private void Update()
    {
        UpdateHoverInfoOnNewMousePos();
        ModifyMesh();
    }

    private void UpdateHoverInfoOnNewMousePos()
    {
        if (Input.mousePosition != hoverInfo.lastMousePosition)
        {
            hoverInfo.lastMousePosition = Input.mousePosition;
            UpdateHoverInfo();
        }
    }

    private void UpdateHoverInfo()
    {
        Ray inRay = cam.ScreenPointToRay(Input.mousePosition);


        if (hitPlane.Raycast(inRay, out float enter))
        {
            Vector3 hitPoint = inRay.GetPoint(enter);
            Vector2Int position = gpuMesh.WorldToHexPosition(hitPoint);

            cursor.transform.position = gpuMesh.HexCenter(position) + new Vector3(0, 0, -0.1f);

            hoverInfo.worldPosition = hitPoint;
            hoverInfo.positon = position;
            hoverInfo.newPositionThisFrame = hoverInfo.lastPosition != hoverInfo.positon;
            hoverInfo.lastPosition = position;
            hoverInfo.inBounds = gpuMesh.PositionInBounds(position);
            //hoverInfo.visibleIndex = hoverInfo.inBounds ? GetVisibleHexIndex(position.x, position.y) : -1;
        }
    }

    private void ModifyMesh()
    {
        if (!hoverInfo.inBounds)
        {
            return;
        }

        if (Input.GetKey(KeyCode.F) && Input.GetMouseButtonDown(0))
        {
            UpdateHoverInfo();

            if (gpuMesh.SetAllHexTextures(drawTexture))
            {
                gpuMesh.UpdateMesh();
            }
        }
        else if (Input.GetMouseButtonDown(0) || (hoverInfo.newPositionThisFrame && Input.GetMouseButton(0)))
        {
            UpdateHoverInfo();

            var positions = Hex.HexSpiral(gpuMesh.orientation, hoverInfo.positon, drawRadius);

            if(gpuMesh.SetTextures(positions, drawTexture))
            {
                gpuMesh.UpdateMesh();
            }
        }
        else if (Input.GetMouseButtonDown(1) || (hoverInfo.newPositionThisFrame && Input.GetMouseButton(1)))
        {
            UpdateHoverInfo();

            if (gpuMesh.SetIndex(hoverInfo.positon, 0))
            {
                gpuMesh.UpdateMesh();
            }
        }
    }
}
