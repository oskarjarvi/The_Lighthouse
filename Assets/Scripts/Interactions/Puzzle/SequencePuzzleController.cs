using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencePuzzleController : PuzzleBase
{
    public List<PuzzleTrigger> puzzleTriggerSequence;

    public List<int> puzzleTriggerIndexList;

    public GameObject puzzleGoal;

    public List<PuzzleTrigger> playerTriggerSequence = new List<PuzzleTrigger>();

    private bool _isPuzzleSolved = false;

    public bool IsSequenceSensitive;

    public string _failPrompt;

    private AudioSource _audioSource;
    
    public override bool IsSolved { get => _isPuzzleSolved; set => value = _isPuzzleSolved; }

    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public override void StartPuzzle()
    {

    }

    public override void SolvePuzzle()
    {
        _isPuzzleSolved = true;
        puzzleGoal.GetComponent<Animator>().SetBool("puzzleSolved", _isPuzzleSolved);
    }
    public void PlayAudio()
    {
        _audioSource.Play();
    }
    private void StopSound()
    {
        _audioSource.Stop();
    }
    public override void ResetPuzzle()
    {
        GameObject playerObject = GameObject.Find("Player");

        if (playerObject != null)
        {
            PopupSystem ppSystem = playerObject.GetComponent<PopupSystem>();
            ppSystem.PopUp(_failPrompt);
        }


        foreach (PuzzleTrigger puzzleTrigger in playerTriggerSequence)
        {
            puzzleTrigger.Reset();
        }

        playerTriggerSequence.Clear();

    }
    public void SequenceTrigger(PuzzleTrigger trigger)
    {
       
        playerTriggerSequence.Add(trigger);
        
        if(playerTriggerSequence.Count == puzzleTriggerSequence.Count ||playerTriggerSequence.Count == puzzleTriggerIndexList.Count)
        {
            CheckSequence();
        }

    }
    private IEnumerator CheckSequenceCoroutine()
    {
        yield return new WaitForSeconds(1.5f);

        bool correctSequence = true;
        if (IsSequenceSensitive)
        {
            for (int i = 0; i < puzzleTriggerSequence.Count; i++)
            {

                if (puzzleTriggerSequence[i].triggerIndex != playerTriggerSequence[i].triggerIndex)
                {
                    correctSequence = false;
                    break;
                }
            }
        }
        else
        {

            for (int i = 0;i < playerTriggerSequence.Count;i++)
            {
               
                if (!puzzleTriggerIndexList.Contains(playerTriggerSequence[i].triggerIndex))
                {

                    correctSequence = false;
                    break;
                }
            }
        }
        if (correctSequence)
        {
            SolvePuzzle();
        }
        else
        {
            ResetPuzzle();
        }

    }

    private void CheckSequence()
    {
        StartCoroutine(CheckSequenceCoroutine());
    }

}
