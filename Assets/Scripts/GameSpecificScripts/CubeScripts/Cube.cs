using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cube : MonoBehaviour
{
    public enum CubeType
    {
        Normal,
        Destroyer,
        Affectible,
        Bottom
    }

    [SerializeField] protected CubeType Type;
    
    [SerializeField] protected bool IsClickable;
    [SerializeField] Animator CubeAnimator;
    [SerializeField] private TileGrid ParentTileGrid;
    [SerializeField] private bool CanFall=true;
    Rigidbody2D RB;

    private SpriteRenderer CubeImage;

    Collider2D collider;


    public void SetCubeImageOrder()
    {
        //int order = 3;
        //Point point = ParentTileGrid.GetMatrixPoint();
        //order = point.GetMatrixIndexX() + 3;
        //CubeImage.sortingOrder = order;

    }

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
        //CubeImage = transform.GetChild(0).GetComponent<SpriteRenderer>();
        //CubeImage.enabled = false;
    }

    


}
