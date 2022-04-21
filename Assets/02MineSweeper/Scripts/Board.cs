using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using System;


namespace MineSweeper
{
    [RequireComponent(typeof(Tilemap))]
    public class Board : MonoBehaviour
    {
        public Tilemap tilemap { get; private set; }

        [SerializeField] Tile tileUnknown, tileEmpty, tileMine, tileExploded, tileFlag, num1, num2, num3, num4, num5, num6, num7, num8;
        private void Awake()
        {
            tilemap = GetComponent<Tilemap>();
        }
        public void Draw(Cell[,] state)
        {
            int width = state.GetLength(0);
            int height = state.GetLength(1);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Cell cell = state[x, y];
                    tilemap.SetTile(cell.position, GetTile(cell));
                }
            }
        }

        Tile GetTile(Cell cell)
        {
            if (cell.revealed)
            {
                return GetRevealedTile(cell);
            }
            else if (cell.flagged)
            {
                return tileFlag;
            }
            else
            {
                return tileUnknown;
            }
        }
        Tile GetRevealedTile(Cell cell)
        {
            switch (cell.type)
            {
                case Cell.Type.Empty: return tileEmpty;
                case Cell.Type.Mine: return cell.exploded ? tileExploded : tileMine;
                case Cell.Type.Number: return GetNumberTile(cell);
                default: return null;
            }
        }
        Tile GetNumberTile(Cell cell)
        {
            switch (cell.numberValue)
            {
                case 1: return num1;
                case 2: return num2;
                case 3: return num3;
                case 4: return num4;
                case 5: return num5;
                case 6: return num6;
                case 7: return num7;
                case 8: return num8;
                default: return null;
            }
        }
    }
}