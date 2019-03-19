using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using SuperTiled2Unity;

namespace Danger
{
    public class PlayerTerrain : MonoBehaviour
    {
        public Vector2 m_Offset;

        private void LateUpdate()
        {
            var tilemap = GameObject.FindGameObjectWithTag("TerrainMap").GetComponent<Tilemap>();
            var worldPosition = gameObject.transform.position + (Vector3)m_Offset;
            var cellPosition = tilemap.WorldToCell(worldPosition);

            // Get the tile at the given cell position
            if (tilemap.GetTile(cellPosition) is SuperTile tile)
            {
                // Report to our UI which tile we're standing on
                var text = GameObject.FindGameObjectWithTag("TileTypeText").GetComponent<TextMeshProUGUI>();
                text.text = tile.GetPropertyValueAsString("terrain-type", "unknown");
            }
        }
    }
}
