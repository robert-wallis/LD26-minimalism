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
	FLabel labelAlexWord;
	FLabel labelAaronWord;
	string[] buttonNames = {
			"puzzle-up",
			"puzzle-right",
			"puzzle-down",
			"puzzle-left",
	};
	float nextX = 0f;
	uint currentChar;
	bool inPuzzle = false;
	Alex.Puzzle currentPuzzle;
	Color onColor = new Color(251.0f / 255.0f, 169.0f / 255.0f, 25.0f / 255.0f);

	bool started = false;

	void Start()
	{
		puzzleUI = new FContainer();
		puzzleUI.x = Futile.screen.width * .1f;
		Futile.stage.AddChild(puzzleUI);
	}

	void GameStarting()
	{
		started = true;
	}

	void GameEnding()
	{
		started = false;
		Cleanup();
	}

	public void Cleanup()
	{
		if (null != puzzleUI)
			puzzleUI.RemoveAllChildren();
		puzzleSequence = null;
		currentChar = 9001;
		inPuzzle = false;
	}


	public void GeneratePuzzle(Alex owner, Alex.Puzzle puzzle)
	{
		this.owner = owner;
		this.currentPuzzle = puzzle;
		Cleanup();
		currentChar = 0;
		if (puzzle.arrow) {
			GenerateArrowPuzzle(puzzle);
		} else if (puzzle.words) {
			GenerateTextPuzzle(puzzle);
		} else {
			throw new System.Exception("Puzzle type unknown " + puzzle);
		}
		inPuzzle = true;
	}

	void GenerateArrowPuzzle(Alex.Puzzle puzzle)
	{
		// generate a new sequence of buttons to solve
		puzzleSequence = new int[puzzle.arrowLength];
		labelsOn = new FSprite[puzzle.arrowLength];
		labelsOff = new FSprite[puzzle.arrowLength];
		nextX = 0f;
		for (int i = 0; i < puzzle.arrowLength; i++) {
			puzzleSequence[i] = Random.Range(0, 4);
			labelsOn[i] = new FSprite(buttonNames[puzzleSequence[i]] + "-on");
			labelsOff[i] = new FSprite(buttonNames[puzzleSequence[i]] + "-off");
			labelsOn[i].x = nextX; 
			labelsOff[i].x = nextX; 
			// next button to the right
			nextX += labelsOn[i].width + 5f;
			puzzleUI.AddChild(labelsOff[i]);
		}
	}

	void ArrowPuzzleCorrectKey()
	{
		puzzleUI.RemoveChild(labelsOff[currentChar]);
		puzzleUI.AddChild(labelsOn[currentChar]);
	}

	void ArrowPuzzleWrongKey()
	{
	}

	void WordPuzzleCorrectKey()
	{
	}

	void WordPuzzleWrongKey()
	{
	}

	void GenerateTextPuzzle(Alex.Puzzle puzzle)
	{
		puzzleSequence = new int[puzzle.wordAaron.Length];
		// all lower, we aren't checking uppercase shift keys
		string keyedWord = puzzle.wordAaron.ToLower();
		for (int i = 0; i < puzzle.wordAaron.Length; i++) {
			puzzleSequence[i] = (int)keyedWord[i];
		}
		labelAlexWord = new FLabel("comfortaa32", puzzle.wordAlex);
		labelAlexWord.color = onColor;
		labelAlexWord.anchorX = 0f;
		labelAlexWord.y = 50f;
		puzzleUI.AddChild(labelAlexWord);
		labelAaronWord = new FLabel("comfortaa32", puzzle.wordAaron);
		labelAaronWord.color = Color.white;
		labelAaronWord.anchorX = 0f;
		puzzleUI.AddChild(labelAaronWord);
	}

	void Update()
	{
		if (!started) {
			return;
		}
		// check what characters someone typed
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
			if (currentPuzzle.arrow) {
				ArrowPuzzleCorrectKey();
			} else if (currentPuzzle.words) {
				WordPuzzleCorrectKey();
			}
			currentChar++;
			audio.PlayOneShot(typeSuccess);
			if (currentChar >= puzzleSequence.Length) {
				PuzzleSuccess();
			}
		} else {
			if (currentPuzzle.arrow) {
				ArrowPuzzleWrongKey();
			} else if (currentPuzzle.words) {
				WordPuzzleWrongKey();
			}
			if (sequenceId != (uint)' ') {
				audio.PlayOneShot(typeFail);
			}
		}
	}

	// hooray you figured out the puzzle
	public void PuzzleSuccess()
	{
		Cleanup();
		owner.PuzzleSuccessCB();
	}
}
