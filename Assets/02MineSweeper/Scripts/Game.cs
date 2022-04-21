using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineSweeper
{
    public class Game : MonoBehaviour
    {
        [SerializeField] int width = 16, height = 16;
        Board board;
        Cell[,] state;
        private void Awake()
        {
            board = GetComponentInChildren<Board>();
        }
        private void Start()
        {
            NewGame();
        }
        void NewGame()
        {
            state = new Cell[width, height];

            GenerateCells();
            Camera.main.transform.position = new Vector3(width / 2f, height / 2f, 0);
            board.Draw(state);
        }

        private void GenerateCells()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Cell cell = new Cell();
                    cell.position = new Vector3Int(x, y, 0);
                    cell.type = Cell.Type.Empty;
                    state[x, y] = cell;
                }
            }
        }
    }
}