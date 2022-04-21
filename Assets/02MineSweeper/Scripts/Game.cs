using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineSweeper
{
    public class Game : MonoBehaviour
    {
        [SerializeField] int width = 16, height = 16, mineCount = 32;
        Board board;
        Cell[,] state;
        bool gameOver = false;
        private void OnValidate()
        {
            mineCount = Mathf.Clamp(mineCount, 0, width * height);
        }
        private void Awake()
        {
            Application.targetFrameRate = 60;
            board = GetComponentInChildren<Board>();
        }
        private void Start()
        {
            NewGame();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                NewGame();
            }
            else if (!gameOver)
            {
                if (Input.GetMouseButtonDown(1))
                    Flag();

                else if (Input.GetMouseButtonDown(0))
                    Reveal();
            }
        }
        void NewGame()
        {
            state = new Cell[width, height];
            gameOver = false;

            GenerateCells();
            GenerateMines();
            GenerateNumbers();

            Camera.main.transform.position = new Vector3(width / 2f, height / 2f, -10);
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
        void GenerateMines()
        {
            for (int i = 0; i < mineCount; i++)
            {
                int x = UnityEngine.Random.Range(0, width);
                int y = UnityEngine.Random.Range(0, height);
                while (state[x, y].type == Cell.Type.Mine)
                {
                    x++;
                    if (x >= width)
                    {
                        y++;
                        if (y >= height)
                        {
                            y = 0;
                        }
                    }
                }
                state[x, y].type = Cell.Type.Mine;
            }
        }
        void GenerateNumbers()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Cell cell = state[x, y];

                    if (cell.type == Cell.Type.Mine)
                        continue;

                    cell.numberValue = CountMines(x, y);

                    if (cell.numberValue > 0)
                        cell.type = Cell.Type.Number;

                    state[x, y] = cell;
                }
            }
        }
        private int CountMines(int cellx, int celly)
        {
            int count = 0;

            for (int xoffset = -1; xoffset <= 1; xoffset++)
            {
                for (int yoffset = -1; yoffset <= 1; yoffset++)
                {
                    if (yoffset == 0 && xoffset == 0)
                        continue;
                    int x = cellx + xoffset;
                    int y = celly + yoffset;

                    if (GetCell(x, y).type == Cell.Type.Mine)
                        count++;
                }
            }
            return count;
        }

        void Flag()
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
            Cell cell = GetCell(cellPosition.x, cellPosition.y);
            if (cell.type == Cell.Type.Invalid || cell.revealed)
                return;

            cell.flagged = !cell.flagged;
            state[cellPosition.x, cellPosition.y] = cell;
            board.Draw(state);
        }
        private void Reveal()
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
            Cell cell = GetCell(cellPosition.x, cellPosition.y);
            if (cell.type == Cell.Type.Invalid || cell.revealed || cell.flagged)
            {
                return;
            }

            switch (cell.type)
            {
                case Cell.Type.Empty:
                    Flood(cell);
                    break;
                case Cell.Type.Mine:
                    Explode(cell);
                    break;
                default:
                    cell.revealed = true;
                    state[cell.position.x, cellPosition.y] = cell;
                    break;
            }
            board.Draw(state);
        }
        void Explode(Cell cell)
        {
            Debug.Log("Game Over!");
            gameOver = true;
            cell.revealed = true;
            cell.exploded = true;
            state[cell.position.x, cell.position.y] = cell;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    cell = state[x, y];
                    if (cell.type == Cell.Type.Mine)
                    {
                        cell.revealed = true;
                        state[x, y] = cell;
                    }
                }
            }
        }

        private void Flood(Cell cell)
        {
            if (cell.revealed) return;
            if (cell.type == Cell.Type.Mine || cell.type == Cell.Type.Invalid) return;
            cell.revealed = true;
            state[cell.position.x, cell.position.y] = cell;
            if (cell.type == Cell.Type.Empty)
            {
                Flood(GetCell(cell.position.x - 1, cell.position.y));
                Flood(GetCell(cell.position.x + 1, cell.position.y));
                Flood(GetCell(cell.position.x, cell.position.y - 1));
                Flood(GetCell(cell.position.x, cell.position.y + 1));
            }
        }
        Cell GetCell(int x, int y)
        {
            if (isValid(x, y))
                return state[x, y];

            else
            {
                return new Cell();//it will marked invalid 
            }
        }

        bool isValid(int x, int y)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }
        private void CheckWinCondition()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Cell cell = state[x, y];

                    // All non-mine cells must be revealed to have won
                    if (cell.type != Cell.Type.Mine && !cell.revealed)
                    {
                        return; // no win
                    }
                }
            }

            Debug.Log("Winner!");
            gameOver = true;

            // Flag all the mines
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Cell cell = state[x, y];

                    if (cell.type == Cell.Type.Mine)
                    {
                        cell.flagged = true;
                        state[x, y] = cell;
                    }
                }
            }
        }

    }
}