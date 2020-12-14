using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Keyboardmoverbytiles_q5 : Keyboardmover_q5
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] AllowedTiles allowedTiles;
    [SerializeField] TileBase[] AllowedCut;
    [SerializeField] TileBase afterCut;
    [SerializeField] float delay = 1f;
    private float curr = 0f;
    private bool flag = false;


    private TileBase TileOnPosition(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    void Update()
    {
        Vector3 newPosition = NewPosition();
        TileBase tileOnNewPosition = TileOnPosition(newPosition);
        if (allowedTiles.Contain(tileOnNewPosition))
        {
            transform.position = newPosition;
        }
        else
        {
            Debug.Log("You cannot walk on " + tileOnNewPosition + "!");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
             TileBase tileOnDirPosition = TileOnPosition(transform.position + saveStep); 
            if (Time.time > curr + delay) //check if delay was completed.
            {
                curr = Time.time;
                flag = true;
            }
            else
            {
                flag = false;
            }

            if (AllowedCut.Contains(tileOnDirPosition)&&flag) 
            {
                Vector3 playerPos = transform.position + saveStep;
                tilemap.SetTile(tilemap.WorldToCell(playerPos), afterCut);
            }

            flag = false; //for the next frame, flag=false as default.
        }

        
    }




}

