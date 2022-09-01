using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cube : MonoBehaviour
{
    public enum CubeType
    {
        Normal,
        Destroyer,
        Affectible
    }

    [SerializeField] protected CubeType Type;
    protected Vector2 Position;
    protected int MatrixIndexX, MatrixY;
    [SerializeField] protected bool IsClickable;
    [SerializeField] Animator CubeAnimator;
    [SerializeField] private TileGrid ParentTileGrid;
    [SerializeField] private bool CanFall=true;
    Rigidbody2D RB;

    Collider2D collider;
    public bool GetCanFall()
    {
        return CanFall;
    }

    public void SetParentTile(TileGrid tile_grid )
    {
        ParentTileGrid = tile_grid;
    }
    public TileGrid GetParentTile()
    {
        return ParentTileGrid;
    }

    public CubeType GetCubeType()
    {
        return Type;
    }

   

    protected  void OnMouseDown()
    {
        
        if (!IsClickable)
        {
            //PlayDITDIT
            return;
        }
       
    }

    private void Start()
    {
        CubeAnimator = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        RB.bodyType = RigidbodyType2D.Kinematic;
        collider = GetComponent<Collider2D>();   
    }

    public void FallToTargetTile(float fall_time,TileGrid target_tile)
    {
        ////ParentTileGrid = target_tile;
        Vector2 old_position = transform.localPosition;
        //this.SetParentTile(target_tile);
        //target_tile.AddCubeToTile(this);
        //transform.parent = target_tile.transform.parent;
        ////transform.localPosition = Vector2.Lerp(old_position, Vector2.zero, fall_time*Time.deltaTime);

        this.SetParentTile(target_tile);


        target_tile.AddCubeToTile(this);
        this.transform.parent = target_tile.transform;
        this.transform.localPosition = Vector2.zero;
        //transform.localPosition = Vector2.Lerp(transform.localPosition, Vector2.zero, fall_time * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tile")
        {
            if(collision.transform.childCount >= 1)
            {
                Debug.Log(gameObject.name + collision.name);
            }
            
            TileGrid new_tile = collision.GetComponent<TileGrid>();
            
            if (new_tile.CheckTileIsEmpty())
            {
                if (ParentTileGrid == null)
                {
                    ParentTileGrid = new_tile;
                    SetParentTile(new_tile);
                    new_tile.AddCubeToTile(this);
                    //new_cube.AddCubeToTile(this);
                    transform.parent = new_tile.transform;

                }
                else
                {
                    if (ParentTileGrid.gameObject != collision.gameObject)
                    {
                        ParentTileGrid.RemoveCubeFromTile();
                        ParentTileGrid = new_tile;
                        SetParentTile(new_tile);
                        new_tile.AddCubeToTile(this);
                        transform.parent = new_tile.transform;
                    }
                }
            }
            
        }
       
        



    }

}
