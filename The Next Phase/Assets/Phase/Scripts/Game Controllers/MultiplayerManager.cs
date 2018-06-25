using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MultiplayerManger : MonoBehaviour
{

    public int numOfRoundsToWin = 3;
    public float startDelay = 3f;
    public float endDelay = 3f;
    public MultiplayerCameraController cameraController;
    public Text messageText;
    public GameObject playerPrefab;
    public PlayerManager[] players;
    public string[] mapNames;
    public int mapIndex;

    private int roundNumber;
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;
    private PlayerManager roundWinner;
    private PlayerManager gameWinner;


    private void Start()
    {
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);

        SpawnAllPlayers();
        SpawnEnemies();

        SetCameraTargets();

        StartCoroutine(GameLoop());
    }

    private void SpawnAllPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            // ... create them, set their player number and references needed for control.
            players[i].Instance =
                Instantiate(playerPrefab, players[i].spawnPoint.position, players[i].spawnPoint.rotation) as GameObject;
            players[i].playerNumber = i + 1;
            players[i].Setup();
        }
    }

    private void SpawnEnemies()
    {

    }

    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[players.Length];

        for (int i = 0; i < players.Length; i++)
        {
            targets[i] = players[i].Instance.transform;
        }

        cameraController.targets = targets;
    }

    // This is called from start and will run each phase of the game one after another.
    private IEnumerator GameLoop()
    {
        // Start off by running the 'RoundStarting' coroutine but don't return until it's finished.
        yield return StartCoroutine(RoundStarting());

        // Once the 'RoundStarting' coroutine is finished, run the 'RoundPlaying' coroutine but don't return until it's finished.
        yield return StartCoroutine(RoundPlaying());

        // Once execution has returned here, run the 'RoundEnding' coroutine, again don't return until it's finished.
        yield return StartCoroutine(RoundEnding());

        // This code is not run until 'RoundEnding' has finished.  At which point, check if a game winner has been found.
        if (gameWinner != null)
        {
            // If there is a game winner, restart the level.
            SceneManager.LoadScene(mapNames[mapIndex]);
        }
        else
        {
            // If there isn't a winner yet, restart this coroutine so the loop continues.
            // Note that this coroutine doesn't yield.  This means that the current version of the GameLoop will end.
            StartCoroutine(GameLoop());
        }
    }


    private IEnumerator RoundStarting()
    {
        // As soon as the round starts reset the players and make sure they can't move.
        ResetAllPlayers();
        DisablePlayerControl();

        // Snap the camera's zoom and position to something appropriate for the reset players.
        cameraController.SetStartPositionAndSize();

        // Increment the round number and display text showing the players what round it is.
        roundNumber++;
        messageText.text = "ROUND " + roundNumber;

        // Wait for the specified length of time until yielding control back to the game loop.
        yield return startWait;
    }


    private IEnumerator RoundPlaying()
    {
        // As soon as the round begins playing let the players control the players.
        EnablePlayerControl();

        // Clear the text from the screen.
        messageText.text = string.Empty;

        // While there is not one player left...
        while (!OneplayerLeft())
        {
            // ... return on the next frame.
            yield return null;
        }
    }


    private IEnumerator RoundEnding()
    {
        DisablePlayerControl();

        // Clear the winner from the previous round.
        roundWinner = null;

        // See if there is a winner now the round is over.
        roundWinner = GetRoundWinner();

        // If there is a winner, increment their score.
        if (roundWinner != null)
            roundWinner.wins++;

        // Now the winner's score has been incremented, see if someone has one the game.
        gameWinner = GetGameWinner();

        // Get a message based on the scores and whether or not there is a game winner and display it.
        string message = EndMessage();
        messageText.text = message;

        // Wait for the specified length of time until yielding control back to the game loop.
        yield return endWait;
    }


    // This is used to check if there is one or fewer players remaining and thus the round should end.
    private bool OneplayerLeft()
    {
        // Start the count of players left at zero.
        int numPlayersLeft = 0;

        // Go through all the players...
        for (int i = 0; i < players.Length; i++)
        {
            // ... and if they are active, increment the counter.
            if (players[i].Instance.activeSelf)
                numPlayersLeft++;
        }

        // If there are one or fewer players remaining return true, otherwise return false.
        return numPlayersLeft <= 1;
    }


    // This function is to find out if there is a winner of the round.
    // This function is called with the assumption that 1 or fewer players are currently active.
    private PlayerManager GetRoundWinner()
    {
        // Go through all the players...
        for (int i = 0; i < players.Length; i++)
        {
            // ... and if one of them is active, it is the winner so return it.
            if (players[i].Instance.activeSelf)
                return players[i];
        }

        // If none of the players are active it is a draw so return null.
        return null;
    }


    // This function is to find out if there is a winner of the game.
    private PlayerManager GetGameWinner()
    {
        // Go through all the players...
        for (int i = 0; i < players.Length; i++)
        {
            // ... and if one of them has enough rounds to win the game, return it.
            if (players[i].wins == numOfRoundsToWin)
                return players[i];
        }

        // If no players have enough rounds to win, return null.
        return null;
    }


    // Returns a string message to display at the end of each round.
    private string EndMessage()
    {
        // By default when a round ends there are no winners so the default end message is a draw.
        string message = "DRAW!";

        // If there is a winner then change the message to reflect that.
        if (roundWinner != null)
            message = roundWinner.coloredPlayerText + " WINS THE ROUND!";

        // Add some line breaks after the initial message.
        message += "\n\n\n\n";

        // Go through all the players and add each of their scores to the message.
        for (int i = 0; i < players.Length; i++)
        {
            message += players[i].coloredPlayerText + ": " + players[i].wins + " WINS\n";
        }

        // If there is a game winner, change the entire message to reflect that.
        if (gameWinner != null)
            message = gameWinner.coloredPlayerText + " WINS THE GAME!";

        return message;
    }


    // This function is used to turn all the players back on and reset their positions and properties.
    private void ResetAllPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].Reset();
        }
    }


    private void EnablePlayerControl()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].EnableControl();
        }
    }


    private void DisablePlayerControl()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].DisableControl();
        }
    }

}
