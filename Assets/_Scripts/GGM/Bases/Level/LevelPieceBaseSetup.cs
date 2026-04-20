using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelPieceBaseSetup", 
                 menuName = "ScriptableObjects/HYPER CASUAL MOBILE GAME/LevelPieceBaseSetup", 
                 order = 0)]
public class LevelPieceBaseSetup : ScriptableObject 
{
    public ArtManager.ArtType artType;

    [Header("Level Pieces")]
    public List<LevelPieceBase> levelPiecesStartPrefabs;
    public List<LevelPieceBase> levelPiecesPrefabs;
    public List<LevelPieceBase> levelPiecesEndPrefabs;

    public int piecesStartCount = 2;
    public int piecesCount = 5;
    public int piecesEndCount = 1;
}
