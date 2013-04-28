using UnityEngine;
using System.Collections;

public class Alex : MonoBehaviour
{
	public BlockingSystem pillars;
	public TypingPuzzle typingPuzzle;

	public float timeTillObstacle = 3f;
	public float timeTillObstacleRefill = 3f;
	bool puzzleInPlay = false;

	public AudioClip[] alexWordPuzzle;
	public AudioClip[] aaronWordPuzzle;
	public AudioClip[] alexNo;
	GameObject alexObject;
	GameObject aaronObject;

	public class Puzzle
	{
		public bool arrow;
		public int arrowLength;
		public bool words;
		public string wordAlex;
		public string wordAaron;
		public int alexWordClipId;
		public int aaronWordClipId;
		public Puzzle(bool arrow, int arrowLength, bool words, string wordAlex, string wordAaron, int alexWordClipId, int aaronWordClipId)
		{
			this.arrow = arrow;
			this.arrowLength = arrowLength;
			this.words = words;
			this.wordAlex = wordAlex;
			this.wordAaron = wordAaron;
			this.alexWordClipId = alexWordClipId;
			this.aaronWordClipId = aaronWordClipId;
		}
	}

	Puzzle[] puzzles = {
		new Puzzle(false, 0, true, "destroy", "build", 0, 0),
		new Puzzle(true, 1, false, "", "", 0, 0),
		new Puzzle(true, 2, false, "", "", 0, 0),
		new Puzzle(true, 3, false, "", "", 0, 0),
		new Puzzle(false, 0, true, "destroy", "build", 0, 0),
		new Puzzle(true, 4, false, "", "", 0, 0),
		new Puzzle(false, 0, true, "sincere", "sarcastic", 1, 1),
		new Puzzle(false, 0, true, "Alex", "Aaron", 2, 2),
		new Puzzle(true, 5, false, "", "", 0, 0),
		new Puzzle(false, 0, true, "kill", "revive", 4, 4),
		new Puzzle(false, 0, true, "good", "wicked", 10, 10),
		new Puzzle(true, 5, false, "", "", 0, 0),
		new Puzzle(false, 0, true, "potato", "fries", 5, 5),
		new Puzzle(true, 6, false, "", "", 0, 0),
		new Puzzle(false, 0, true, "fall", "spring", 6, 6),
		new Puzzle(false, 0, true, "stupid", "intelligent", 7, 7),
		new Puzzle(true, 6, false, "", "", 0, 0),
		new Puzzle(false, 0, true, "exciting", "EVE online", 8, 8),
		new Puzzle(false, 0, true, "productive", "lazy", 9, 9),
		new Puzzle(true, 7, false, "", "", 0, 0),
		new Puzzle(false, 0, true, "synchronous", "asynchronous", 3, 3),
		new Puzzle(true, 8, false, "", "", 0, 0),
	};
	uint currentPuzzle;

	// Use this for initialization
	void Start()
	{
		alexObject = GameObject.Find("/game/alex");
		aaronObject = GameObject.Find("/game/aaron");
	}

	// Update is called once per frame
	void Update()
	{
		if (!puzzleInPlay && currentPuzzle < puzzles.Length) {
			timeTillObstacle -= Time.deltaTime;
			if (timeTillObstacle < 0) {
				timeTillObstacle = timeTillObstacleRefill;
				puzzleInPlay = true;
				SendPillars();
				if (puzzles[currentPuzzle].words) {
					alexObject.audio.PlayOneShot(alexWordPuzzle[puzzles[currentPuzzle].alexWordClipId]);
				}
				typingPuzzle.GeneratePuzzle(this, puzzles[currentPuzzle]);
			}
		}
	}

	public void Cleanup()
	{
		pillars.Cleanup();
		typingPuzzle.Cleanup();
		timeTillObstacle = timeTillObstacleRefill;
		puzzleInPlay = false;
		currentPuzzle = 0;
	}

	public void SendPillars()
	{
		pillars.SendBlocks();
	}

	// the puzzle finished successfully
	public void PuzzleSuccessCB()
	{
		pillars.Cleanup();
		puzzleInPlay = false;
		if (puzzles[currentPuzzle].words) {
			aaronObject.audio.PlayOneShot(aaronWordPuzzle[puzzles[currentPuzzle].aaronWordClipId]);
		} else {
			if (Random.Range(0, 2) > 0) {
				alexObject.audio.PlayOneShot(alexNo[Random.Range(0, alexNo.Length-1)]);
			}
		}
		currentPuzzle++;
		if (currentPuzzle >= puzzles.Length) {
			GameState.instance.GameWin();
		}
	}
}
