using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main class of cube classes. Every other cube classes are inhetired from this Script
public class Cube : MonoBehaviour
{
    public enum CubeType
    {
        Normal,
        Destroyer,
        Affectible,
        Bottom
    }

    [SerializeField] protected CubeType cubeType;
    
    [SerializeField] protected bool IsClickable;
    [SerializeField] protected Animator CubeAnimator;
    [SerializeField] private TileGrid ParentTileGrid;
    [SerializeField] private bool CanFall=true;
    Rigidbody2D RB;

    private SpriteRenderer CubeImage;

    protected Collider2D SelfCollider;
    [SerializeField] ParticleSystem ParticleEffect;
    [SerializeField] protected GameObject ParticleEffectPrefab;

    protected bool CanPlay=true;



  

    public GameObject GetParticleEffect()
    {
        return ParticleEffectPrefab;
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
        return cubeType;
    }

   

    protected  void OnMouseDown()
    {
        if (CanPlay)
        {
            if (!IsClickable)
            {
                //PlayDITDIT
                return;
            }
        }
        else
        {
            return;
        }
       
    }

    private void Awake()
    {
        CubeAnimator = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        
        SelfCollider = GetComponent<Collider2D>();
        
    }

    public void DestroyCube(Transform particle_parent)
    {
        if (cubeType == CubeType.Normal)
        {
            GameObject particle = Instantiate(ParticleEffectPrefab, transform.position, Quaternion.identity);
            particle.transform.parent = transform;

            particle.transform.localScale =Vector2.one/2;

            particle.transform.parent = particle_parent;
            Destroy(gameObject);
           
            
            

        }

        else { Destroy(gameObject); }
    }

    public void ActivateCube()
    {
        CubeImage.enabled = true;
    }

   
}
