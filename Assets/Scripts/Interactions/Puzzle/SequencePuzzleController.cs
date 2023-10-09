using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencePuzzleController : PuzzleBase
{
    public List<PuzzleTrigger> puzzleTriggerSequence;

    public GameObject puzzleGoal;

    public List<PuzzleTrigger> playerTriggerSequence = new List<PuzzleTrigger>();

    private bool _isPuzzleSolved = false;
    public override bool IsSolved { get => _isPuzzleSolved; set => value = _isPuzzleSolved; }

    public override void StartPuzzle()
    {

    }

    public override void SolvePuzzle()
    {
        _isPuzzleSolved = true;
        Debug.Log("yay you unlocked the door");
        //puzzleGoal.SendMessage("Solved", SendMessageOptions.DontRequireReceiver);

    }

    public override void ResetPuzzle()
    {
        Debug.Log("resetting");

        foreach (PuzzleTrigger puzzleTrigger in puzzleTriggerSequence)
        {
            puzzleTrigger.Reset();
        }
        playerTriggerSequence.Clear();

    }
    public void SequenceTrigger(PuzzleTrigger trigger)
    {
        Debug.Log("adding to sequence");
       
        playerTriggerSequence.Add(trigger);
        
        if(playerTriggerSequence.Count == puzzleTriggerSequence.Count)
        {
            CheckSequence();
        }

    }
   
    private void CheckSequence()
    {
        bool correctSequence = true;
        for(int i=0; i< puzzleTriggerSequence.Count;i++)
        {
            if(puzzleTriggerSequence[i] != playerTriggerSequence[i])
            {
                correctSequence = false;
                break;
            }
           
        }
        if(correctSequence)
        {
            SolvePuzzle();
        }
        else
        {

            ResetPuzzle();

        }

    }

}
