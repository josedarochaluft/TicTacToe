using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tile {Empty = 0, Nought = 1, Cross = 2}

public class GridModel : MonoBehaviour
{

    Tile[,] gameGrid;

    [SerializeField]
    GridView gridView;

    Tile currentMove;

    bool firstPlayer = true;

    bool xAI = false, oAI = true;

    int moves = 0;

    bool matchEnded = false;

    int xWins = 0, oWins = 0;

    void Start()
    {
        InitializeGrid();
    }

    void Update()
    {
        if(!matchEnded)
        {
            //If its X's turn
            if(firstPlayer)
            {
                //And X is an AI
                if(xAI)
                {
                    int foundX, foundY;
                    //Randomly look for empty space for AI to play in 
                    foundX = Random.Range(0,3);
                    foundY = Random.Range(0,3);
                    if(gameGrid[foundX,foundY] == Tile.Empty)
                    {
                        SetTile(foundX, foundY);
                    }
                }   
            }
            //If its O's turn
            else
            {
                //And O is an AI
                if(oAI)
                {
                    int foundX, foundY;
                    //Randomly look for empty space for AI to play in 
                    foundX = Random.Range(0,3);
                    foundY = Random.Range(0,3);
                    if(gameGrid[foundX,foundY] == Tile.Empty)
                    {
                        SetTile(foundX, foundY);
                    }
                }
            }    
        }
    }

    void InitializeGrid()
    {
        gameGrid = new Tile[3,3];
        for(int x=0; x<3; ++x)
        {
            for(int y=0; y<3; ++y)
            {
                gameGrid[x,y] = Tile.Empty;
            }
        }
    }

    //Swaps between AI and player control
    public void ChangeInputSource(bool isX)
    {
        if(isX)
        {
            xAI = !xAI;
            gridView.ChangePlayerDisplay(xAI? "AI" : "Player", isX);
        }
        else
        {
            oAI = !oAI;
            gridView.ChangePlayerDisplay(oAI? "AI" : "Player", isX);
        }
    }

    public void ResetGrid()
    {
        moves=0;
        firstPlayer = true;
        matchEnded = false;
        InitializeGrid();
        gridView.ClearTiles();
        gridView.HideMatchResults();
        gridView.UpdateTurn(firstPlayer);
    }

    public void SetTile(int x, int y)
    {
        if(gameGrid[x,y] == Tile.Empty && !matchEnded)
        {
            currentMove = firstPlayer? Tile.Cross : Tile.Nought;
            gameGrid[x,y] = currentMove;
            gridView.DisplayTile(x,y, currentMove);
            moves++;
            
            if(CheckMatch(x,y))
            {
                matchEnded = true;
                if(firstPlayer)
                {
                    xWins++;
                }
                else
                {
                    oWins++;
                }
                gridView.RevealMatchResults(firstPlayer);
                gridView.UpdateScoreboard(xWins,oWins);
            }
            else if (moves == 9)
            {
                MatchDraw();
            }
            else
            {
                firstPlayer = !firstPlayer;
                gridView.UpdateTurn(firstPlayer);
            }
        }
    }

    void MatchDraw()
    {
        matchEnded = true;
        gridView.RevealMatchDraw();
    }

    //Check for win conditions based on the given coordinates
    bool CheckMatch(int x,int y)
    {
        bool match = false;
        //Check for a horizontal match
        for(int i = 0; i < 3; i++)
        {
            if(gameGrid[(x+i)%3,y] == currentMove)
            {
                match = true;
            }
            else
            {
                match = false;
                break;
            }
        }
        //If a match was made return true
        if(match)
        {
            return true;
        }
        //Check for a vertical match
        for(int j = 0; j < 3; j++)
        {
            if(gameGrid[x,(y+j)%3] == currentMove)
            {
                match = true;
            }
            else
            {
                match = false;
                break;
            }
        }
        //If a match was made return true
        if(match)
        {
            return true;
        }

        //Check for a diagonal match where x and y share the same increment
        if(x+y==x*2)
        {
            for(int k = 0; k < 3; k++)
            {
                if(gameGrid[(x+k)%3,(y+k)%3] == currentMove)
                {
                    match = true;
                }
                else
                {
                    match = false;
                    break;
                }
            }
            //If a match was made return true
            if(match)
            {
                return true;
            }
        }

        //Check for a diagonal match where x and y have opposing increments
        if(x+y==2)
        {
            for(int l = 0; l < 3; l++)
            {
                //assign a temporary integer to loop X to the opposite side of the grid as % won't work for a negative number
                int tempX = x-l;
                if(tempX < 0)
                {
                    tempX+=3;
                }
                if(gameGrid[tempX,(y+l)%3] == currentMove)
                {
                    match = true;
                }
                else
                {
                    match = false;
                    break;
                }
            }
        }
        return match;
    }
}
