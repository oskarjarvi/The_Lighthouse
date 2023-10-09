using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzleBase
{
    public string PuzzleName;
    public bool IsSolved {  get; protected set; }

    public abstract void StartPuzzle();
    public abstract void SolvePuzzle();
}
