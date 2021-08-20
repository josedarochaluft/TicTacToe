using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridView : MonoBehaviour
{
    [SerializeField]
    GameObject tilePrefab;

    [SerializeField]
    Sprite oSprite, xSprite;

    [SerializeField]
    Text oScore, xScore, matchResult;

    [SerializeField]
    Image oHighlight, xHighlight;

    [SerializeField]
    Text oInputSource, xInputSource;

    float offset = 2.5f;
    public void DisplayTile(int x, int y, Tile tile)
    {
        GameObject newTile = Instantiate(tilePrefab, new Vector3((x * offset) - offset, (y * offset) - offset,0), Quaternion.identity, transform);
        SpriteRenderer newRenderer = newTile.GetComponent<SpriteRenderer>();
        switch(tile)
        {
            default:
                newRenderer.color = Color.red;
            break;
            case Tile.Cross:
                newRenderer.color = Color.blue;
                newRenderer.sprite = xSprite;
            break;
        }
    }

    public void ClearTiles()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void UpdateTurn(bool firstPlayer)
    {
        xHighlight.enabled = firstPlayer;
        oHighlight.enabled = !firstPlayer;
    }

    public void UpdateScoreboard(int xValue, int oValue)
    {
        xScore.text = xValue.ToString();
        oScore.text = oValue.ToString();
    }

    public void RevealMatchResults(bool firstPlayer)
    {
        matchResult.text = firstPlayer? "X won the match!" : "O won the match!";

        matchResult.enabled = true;
    }

    public void RevealMatchDraw()
    {
        matchResult.text = "Draw!";

        matchResult.enabled = true;
    }

    public void HideMatchResults()
    {
        matchResult.enabled = false;
    }

    public void ChangePlayerDisplay(string inputSource, bool isX)
    {
        if(isX)
        {
            xInputSource.text = inputSource;
        }
        else
        {
            oInputSource.text = inputSource;
        }
    }
}
