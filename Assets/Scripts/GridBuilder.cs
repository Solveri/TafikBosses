using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridBuilder : MonoBehaviour
{
    public Bricks brickPrefab;
    public int rows = 5;
    public int columns = 5;
    public float tileSize = 1f;
    public float spacing = 0.1f;

    private Bricks[,] gridBricks;
    private Vector3 gridOrigin;

    void Start()
    {
        gridBricks = new Bricks[columns, rows];
        gridOrigin = transform.position; // Set the origin of the grid to the position of the GridBuilder GameObject
    }

    [ContextMenu("CreateGrid")]
    public void CreateGrid()
    {
        ClearGrid();

        gridBricks = new Bricks[columns, rows];
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Vector3 position = gridOrigin + new Vector3(i * (tileSize + spacing), j * (tileSize + spacing), 0);
                Bricks brick = Instantiate(brickPrefab, position, Quaternion.identity, this.transform);
                brick.gameObject.AddComponent<BoxCollider2D>(); // Ensure each brick has a collider
                gridBricks[i, j] = brick;
            }
        }
    }

    [ContextMenu("ClearGrid")]
    public void ClearGrid()
    {
        if (gridBricks != null)
        {
            foreach (Bricks brick in gridBricks)
            {
                if (brick != null)
                {
                    DestroyImmediate(brick.gameObject);
                }
            }
        }

        gridBricks = new Bricks[columns, rows];
    }

    public void PlaceOrRemoveBrick(int x, int y)
    {
        if (gridBricks[x, y] != null)
        {
            DestroyImmediate(gridBricks[x, y].gameObject);
            gridBricks[x, y] = null;
        }
        else
        {
            Vector3 position = gridOrigin + new Vector3(x * (tileSize + spacing), y * (tileSize + spacing), 0);
            Bricks brick = Instantiate(brickPrefab, position, Quaternion.identity, this.transform);
            brick.gameObject.AddComponent<BoxCollider2D>(); // Ensure each brick has a collider
            gridBricks[x, y] = brick;
        }
    }

    public bool GetGridCoordinates(Vector2 worldPosition, out int x, out int y)
    {
        Vector2 localPosition = worldPosition - (Vector2)gridOrigin;
        x = Mathf.FloorToInt(localPosition.x / (tileSize + spacing));
        y = Mathf.FloorToInt(localPosition.y / (tileSize + spacing));

        if (x >= 0 && x < columns && y >= 0 && y < rows)
        {
            return true;
        }
        return false;
    }
}
