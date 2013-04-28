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
		puzzleUI.x = Futile.screen.width * .1f;
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
			// next button to the right
			buttonWidth = labelsOn[i].width + 5f;
			puzzleUI.AddChild(labelsOn[i]);
		}
		currentChar = 0;
		inPuzzle = true;
	}

	void Update()
	{
		if (inPuzzle && currentChar < puzzleSequence.Length) {
			if (Input.GetKeyDown(KeyCode.UpArrow)) { KeyHit(0); }
			if (Input.GetKeyDown(KeyCode.RightArrow)) { KeyHit(1); }
			if (Input.GetKeyDown(KeyCode.DownArrow)) { KeyHit(2); }
			if (Input.GetKeyDown(KeyCode.LeftArrow)) { KeyHit(3); }
			if (Input.GetKeyDown(KeyCode.A)) { KeyHit((uint)'a'); }
			if (Input.GetKeyDown(KeyCode.B)) { KeyHit((uint)'b'); }
			if (Input.GetKeyDown(KeyCode.C)) { KeyHit((uint)'c'); }
			if (Input.GetKeyDown(KeyCode.D)) { KeyHit((uint)'d'); }
			if (Input.GetKeyDown(KeyCode.E)) { KeyHit((uint)'e'); }
			if (Input.GetKeyDown(KeyCode.F)) { KeyHit((uint)'f'); }
			if (Input.GetKeyDown(KeyCode.G)) { KeyHit((uint)'g'); }
			if (Input.GetKeyDown(KeyCode.H)) { KeyHit((uint)'h'); }
			if (Input.GetKeyDown(KeyCode.I)) { KeyHit((uint)'i'); }
			if (Input.GetKeyDown(KeyCode.J)) { KeyHit((uint)'j'); }
			if (Input.GetKeyDown(KeyCode.K)) { KeyHit((uint)'k'); }
			if (Input.GetKeyDown(KeyCode.L)) { KeyHit((uint)'l'); }
			if (Input.GetKeyDown(KeyCode.M)) { KeyHit((uint)'m'); }
			if (Input.GetKeyDown(KeyCode.N)) { KeyHit((uint)'n'); }
			if (Input.GetKeyDown(KeyCode.O)) { KeyHit((uint)'o'); }
			if (Input.GetKeyDown(KeyCode.P)) { KeyHit((uint)'p'); }
			if (Input.GetKeyDown(KeyCode.Q)) { KeyHit((uint)'q'); }
			if (Input.GetKeyDown(KeyCode.R)) { KeyHit((uint)'r'); }
			if (Input.GetKeyDown(KeyCode.S)) { KeyHit((uint)'s'); }
			if (Input.GetKeyDown(KeyCode.T)) { KeyHit((uint)'t'); }
			if (Input.GetKeyDown(KeyCode.U)) { KeyHit((uint)'u'); }
			if (Input.GetKeyDown(KeyCode.V)) { KeyHit((uint)'v'); }
			if (Input.GetKeyDown(KeyCode.W)) { KeyHit((uint)'w'); }
			if (Input.GetKeyDown(KeyCode.X)) { KeyHit((uint)'x'); }
			if (Input.GetKeyDown(KeyCode.Y)) { KeyHit((uint)'y'); }
			if (Input.GetKeyDown(KeyCode.Z)) { KeyHit((uint)'z'); }
			if (Input.GetKeyDown(KeyCode.Space)) { KeyHit((uint)' '); }
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
