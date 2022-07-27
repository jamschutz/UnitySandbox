using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jsch.Editor.LevelCreator
{
    public class Level : MonoBehaviour
    {
        public int totalTime = 60;
        public float gravity = -30;

        private int totalColumns = 60;
        private int totalRows = 10;
        public const float GridSize = 1.28f;

        private readonly Color normalColor = Color.gray;
        private readonly Color selectedColor = Color.yellow;


        // EDITOR
        private void OnDrawGizmos()
        {
            // cache gizmo color
            Color oldColor = Gizmos.color;
            
            // draw grid
            Gizmos.color = normalColor;
            GridGizmo(totalColumns, totalRows);
            GridFrameGizmo(totalColumns, totalRows);

            // reset gizmo color
            Gizmos.color = oldColor;
        }

        private void OnDrawGizmosSelected()
        {
            // cache gizmo color
            Color oldColor = Gizmos.color;

            // draw outline selected
            Gizmos.color = selectedColor;
            GridFrameGizmo(totalColumns, totalRows);

            // reset gizmo color
            Gizmos.color = oldColor;
        }

        // draw grid's outline
        private void GridFrameGizmo(int cols, int rows)
        {
            // draw horizontal lines
            Gizmos.DrawLine(new Vector3(0,0,0), new Vector3(0, rows * GridSize, 0));
            Gizmos.DrawLine(new Vector3(0, rows * GridSize, 0), new Vector3(cols * GridSize, rows * GridSize, 0));
            
            // draw vertical lines
            Gizmos.DrawLine(new Vector3(0,0,0), new Vector3(cols * GridSize, 0));
            Gizmos.DrawLine(new Vector3(cols * GridSize, 0, 0), new Vector3(cols * GridSize, rows * GridSize, 0));
        }

        // draw grid
        private void GridGizmo(int cols, int rows)
        {
            for(int i = 1; i < cols; i++) {
                Gizmos.DrawLine(new Vector3(i * GridSize, 0, 0), new Vector3(i * GridSize, rows * GridSize, 0));
            }

            for(int i = 1; i < rows; i++) {
                Gizmos.DrawLine(new Vector3(0, i * GridSize, 0), new Vector3(cols * GridSize, i * GridSize, 0));
            }
        }
    }
}