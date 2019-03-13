using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using SuperTiled2Unity;

public class GetTileProperties : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            var tilemap = GameObject.FindGameObjectWithTag("TerrainMap").GetComponent<Tilemap>();
            var worldPosition = (Vector2)camera.ScreenToWorldPoint(Input.mousePosition);
            var cell = tilemap.WorldToCell(worldPosition);

            if (tilemap.GetTile(cell) is SuperTile tile)
            {
                if (tile.m_CustomProperties.TryGetProperty("tile-type", out CustomProperty property))
                {
                    Debug.LogFormat("Tile type: {0}", property.m_Value);
                }
            }
        }
    }
}

