using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzleBase : MonoBehaviour {
    public abstract bool IsSolved { get; set; }

    public abstract void StartPuzzle();
    public abstract void SolvePuzzle();

    public abstract void ResetPuzzle();
}
