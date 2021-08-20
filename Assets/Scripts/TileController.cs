using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField]
    int x, y;

    [SerializeField]
    GridModel grid;

    void OnValidate()
    {
        grid = transform.parent.GetComponent<GridModel>();
    }

    void OnMouseDown()
    {
        grid.SetTile(x, y);
    }
}
