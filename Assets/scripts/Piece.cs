using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public TetronimoData data;
    public Board board;
    public Vector2Int[] cells;

    public Vector2Int position;
    
    bool freeze = false;

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
        if (freeze) return;

        board.Clear(this);

        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
        }
        else
        {
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
            if (Input.GetKeyDown(KeyCode.LeftArrow)) Rotate(1);
            else if (Input.GetKeyDown(KeyCode.RightArrow)) Rotate(-1);

        }
        
        board.Set(this);

        //TBD
        if (Input.GetKey(KeyCode.P))
        {
            board.CheckBoard();
        }


        //checking board must come after board.set
        if (freeze)
        {
            board.CheckBoard();

            //update score
            board.SpawnPiece();

        }
    }

    void Rotate(int direction)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 90 * direction);


        for (int i = 0; i < cells.Length; i++)
        {
            //convert cellPosition to Vec3 to work with Quatern
            Vector3 cellPosition = new Vector3(cells[i].x, cells[i].y);

            //Get the restul
            Vector3 result = rotation * cellPosition;

            //put it back in the cells data
            cells[i].x = Mathf.RoundToInt(result.x);
            cells[i].y = Mathf.RoundToInt(result.y);
        }


    }

    void HardDrop()
    {
        //algo: repeatedly move down until unable
        while (Move(Vector2Int.down))
        {
            //do nothing
        }

        freeze = true;
        board.SpawnPiece();
    }
    bool Move(Vector2Int translation)
    {
        Vector2Int newPosition = position;
        newPosition += translation;

        bool positionValid = board.IsPositionValid(this, newPosition);
        if (positionValid) position = newPosition;

        return positionValid;
    }
}
