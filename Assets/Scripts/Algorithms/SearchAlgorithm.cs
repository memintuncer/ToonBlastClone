using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAlgorithm : MonoBehaviour
{

    List<TileGrid> TraversedTiles = new List<TileGrid>();
    List<AffectedByExplosionCube> AffectedByExplosionCubesToBeDeleted = new List<AffectedByExplosionCube>();
    List<Cube> ColorCubesToBeDeleted = new List<Cube>();
    
    private void OnEnable()
    {
        EventManager.StartListening(GameConstants.GameEvents.COLOR_CUBE_SELECTED,StartSearching);   
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameConstants.GameEvents.COLOR_CUBE_SELECTED,StartSearching);
    }

    

    void StartSearching(EventParam param)
    {
        Cube selected_cube = param.GetSelectedCube();

        switch (selected_cube.GetCubeType())
        {
            case Cube.CubeType.Normal:
                
                SearchForNeighbours(selected_cube);
                break;
           
        }
    }


    void SearchForNeighbours(Cube selected_cube)
    {
        List<List<TileGrid>> tile_matrix = GridManager.GetTileMatrix();
        Point start_point = selected_cube.GetParentTile().GetMatrixPoint();
        ColorCubesToBeDeleted.Add(selected_cube);
        TraversedTiles.Add(selected_cube.GetParentTile());
        TraverseNew(tile_matrix,TraversedTiles,ColorCubesToBeDeleted,AffectedByExplosionCubesToBeDeleted, ((ColorCube)selected_cube).GetColor());
        

        SendSearchIsFinishedMessage(selected_cube);

        





    }


    void SendSearchIsFinishedMessage(Cube selected_cube)
    {
        EventParam param = new EventParam();
        param.SetAffectedByExplosionCubesToBeDeleted(AffectedByExplosionCubesToBeDeleted);
        param.SetColorCubesToBeDeleted(ColorCubesToBeDeleted);
        param.SetSelectedCube((ColorCube)selected_cube);
        EventManager.TriggerEvent(GameConstants.GameEvents.COLOR_CUBE_SEARCH_COMPLETED, param);
        
    }
    void TraverseNew(List<List<TileGrid>> tile_matrix, List<TileGrid> elementsToBeTraversed, List<Cube> color_cubes_to_be_deleted,
        List<AffectedByExplosionCube> affectible_cubes_to_be_deleted, ColorCube.ColorType selected_color)
    {
        while (elementsToBeTraversed.Count > 0)
        {
            int curX = elementsToBeTraversed[0].GetMatrixPoint().GetMatrixIndexX();
            int curY = elementsToBeTraversed[0].GetMatrixPoint().GetMatrixIndexY();
            CheckElement(curX - 1, curY);
            CheckElement(curX + 1, curY);
            CheckElement(curX, curY + 1);
            CheckElement(curX, curY - 1);

            elementsToBeTraversed.Remove(elementsToBeTraversed[0]);
        }

        void CheckElement(int x, int y)
        {
            if (x > -1 && x < GridManager.GetMatrixHeight() && y > -1 && y < GridManager.GetMatrixWidth())
            {
                if (!(tile_matrix[x][y].CheckTileIsEmpty())){
                    if (tile_matrix[x][y].GetCurrentCube().GetCubeType().Equals(Cube.CubeType.Normal))
                    {
                        if (!ReferenceEquals(tile_matrix[x][y], null) && 
                            ((ColorCube)tile_matrix[x][y].GetCurrentCube()).GetColor().Equals(selected_color))
                        
                        {
                           


                            if (!color_cubes_to_be_deleted.Contains(((ColorCube)tile_matrix[x][y].GetCurrentCube())) &&
                                !color_cubes_to_be_deleted.Contains((ColorCube)tile_matrix[x][y].GetCurrentCube()))
                            {

                                color_cubes_to_be_deleted.Add((ColorCube)tile_matrix[x][y].GetCurrentCube());
                                elementsToBeTraversed.Add(tile_matrix[x][y]);

                            }



                        }


                    }

                    if (tile_matrix[x][y].GetCurrentCube().GetCubeType().Equals(Cube.CubeType.Affectible))
                    {
                        if (!affectible_cubes_to_be_deleted.Contains((AffectedByExplosionCube)tile_matrix[x][y].GetCurrentCube()))
                        {
                            ((AffectedByExplosionCube)tile_matrix[x][y].GetCurrentCube()).SendNeighbourType(TraversedTiles[0].GetCurrentCube().GetCubeType());
                            affectible_cubes_to_be_deleted.Add((AffectedByExplosionCube)tile_matrix[x][y].GetCurrentCube());
                            
                        }

                    }
                }
   



            }

        }
    }
    

}
