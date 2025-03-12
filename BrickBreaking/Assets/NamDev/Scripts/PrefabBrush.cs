/*using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
namespace NamDev
{
    [CreateAssetMenu(menuName = "Brushes/Prefab Brush")]
    public class PrefabBrush : GridBrush
    {
        public GameObject prefab;

        public override void Paint(GridLayout grid, GameObject layer, Vector3Int position)
        {
            Instantiate(prefab, grid.CellToWorld(position), Quaternion.identity, layer.transform);
        }
    }
}*/