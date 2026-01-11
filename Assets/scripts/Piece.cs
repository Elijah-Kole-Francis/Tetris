using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public TetronimoData data;
    public Board board;
    public Vector2Int[] cells;

    public Vector2Int position;

    public void Initialize(Board board, Tetronimo tetronimo)
    {
        //set ref to board object
        this.board = board;

        //search for tetronimo data and assign
        for (int i = 0; i < board.tetronimos.Length; i++)
        {
            if (board.tetronimos[i].Tetronimo == tetronimo)
            { 
                this.data = board.tetronimos[i];
                break;
            }

        }
        //copy of tetronimo cell locations
        cells = new Vector2Int[data.cells.Length];
        for (int i = 0; i < data.cells.Length; i++) cells[i] = data.cells[i];

        //start position of piece
        position = board.startPosition;
    }
    private void Update()
    {
        board.Clear(this);

        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector2Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector2Int.right);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Move(Vector2Int.up);
        }

        board.Set(this);
    }
    private void Move(Vector2Int translation)
    {
        Vector2Int newPosition = position;
        newPosition += translation;


        if (board.IsPositionValid(this,newPosition))position += translation;
    }
}
