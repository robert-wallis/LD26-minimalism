using UnityEngine;
using System.Collections;

public class TypingPuzzle : MonoBehaviour
{
	Alex owner;
	public AudioClip typeSuccess;
	public AudioClip typeFail;
	FContainer puzzleUI;
	int[] puzzleSequence;
	FSprite[] labelsOn;
	FSprite[] labelsOff;
	string[] buttonNames = {
			"puzzle-up",
			"puzzle-right",
			"puzzle-down",
			"puzzle-left",
	};
	float buttonWidth = 25f;
	uint sequenceLength = 3;
	uint currentChar;
	bool inPuzzle = false;

	void Start()
	{
		puzzleUI = new FContainer();
		puzzleUI.x = Futile.screen.halfWidth * .5f;
		Futile.stage.AddChild(puzzleUI);
	}

	public void Cleanup()
	{
		if (null != puzzleUI)
			puzzleUI.RemoveAllChildren();
		currentChar = 9001;
		inPuzzle = false;
	}


	public void GeneratePuzzle(Alex owner)
	{
		this.owner = owner;
		Cleanup();
		// generate a new sequence of buttons to solve
		puzzleSequence = new int[sequenceLength];
		labelsOn = new FSprite[sequenceLength];
		labelsOff = new FSprite[sequenceLength];
		for (int i = 0; i < sequenceLength; i++) {
			puzzleSequence[i] = Random.Range(0, 3);
			labelsOn[i] = new FSprite(buttonNames[puzzleSequence[i]] + "-on");
			labelsOff[i] = new FSprite(buttonNames[puzzleSequence[i]] + "-off");
			labelsOn[i].x = i * buttonWidth; 
			labelsOff[i].x = i * buttonWidth; 
			puzzleUI.AddChild(labelsOn[i]);
		}
		currentChar = 0;
		inPuzzle = true;
	}

	void Update()
	{
		if (inPuzzle && currentChar < puzzleSequence.Length) {
			if (Input.GetKeyDown(KeyCode.UpArrow)) {
				KeyHit(0);
			}
			if (Input.GetKeyDown(KeyCode.RightArrow)) {
				KeyHit(1);
			}
			if (Input.GetKeyDown(KeyCode.DownArrow)) {
				KeyHit(2);
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow)) {
				KeyHit(3);
			}
		}
	}

	// check the sequence to see if they did it correct
	// returns correct or incorrect
	void KeyHit(uint sequenceId)
	{
		if (puzzleSequence[currentChar] == sequenceId) {
			puzzleUI.RemoveChild(labelsOn[currentChar]);
			puzzleUI.AddChild(labelsOff[currentChar]);
			currentChar++;
			audio.PlayOneShot(typeSuccess);
			if (currentChar >= puzzleSequence.Length) {
				PuzzleSuccess();
			}
		} else {
			audio.PlayOneShot(typeFail);
		}
	}

	// hooray you figured out the puzzle
	public void PuzzleSuccess()
	{
		Cleanup();
		owner.PuzzleSuccessCB();
	}
}
