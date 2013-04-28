using UnityEngine;
using System.Collections;

public class Alex : MonoBehaviour
{
	public BlockingSystem pillars;
	public TypingPuzzle typingPuzzle;

	public float timeTillObstacle = 3f;
	public float timeTillObstacleRefill = 3f;
	bool puzzleInPlay = false;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if (!puzzleInPlay) {
			timeTillObstacle -= Time.deltaTime;
			if (timeTillObstacle < 0) {
				timeTillObstacle = timeTillObstacleRefill;
				puzzleInPlay = true;
				SendPillars();
				typingPuzzle.GeneratePuzzle(this);
			}
		}
	}

	public void Cleanup()
	{
		pillars.Cleanup();
		typingPuzzle.Cleanup();
		timeTillObstacle = timeTillObstacleRefill;
		puzzleInPlay = false;
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
	}
}
