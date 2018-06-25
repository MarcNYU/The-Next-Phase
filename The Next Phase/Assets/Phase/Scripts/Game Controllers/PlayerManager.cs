using System;
using UnityEngine;

[Serializable]
public class PlayerManager
{

    public Color playerColor;
    public Transform spawnPoint;
    [HideInInspector] public int playerNumber;
    [HideInInspector] public string coloredPlayerText;
    [HideInInspector] public GameObject Instance;
    [HideInInspector] public int wins;

    private PlayerMovementRB movement;
    private PlayerHealth health;
    private PlayerShooting shooting;
    private GameObject canvasGameObject;


    public void Setup()
    {
        movement = Instance.GetComponent<PlayerMovementRB>();
        health = Instance.GetComponent<PlayerHealth>();
        shooting = Instance.GetComponent<PlayerShooting>();
        canvasGameObject = Instance.GetComponentInChildren<Canvas>().gameObject;

        movement.playerNumber = playerNumber;

        coloredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(playerColor) + ">PLAYER" + playerNumber + "</color>";

        // Get all of the renderers of the tank.
        MeshRenderer[] renderers = Instance.GetComponentsInChildren<MeshRenderer>();

        // Go through all the renderers...
        for (int i = 0; i < renderers.Length; i++)
        {
            // ... set their material color to the color specific to this tank.
            renderers[i].material.color = playerColor;
        }
    }

    public void DisableControl()
    {
        health.Death();

        canvasGameObject.SetActive(false);
    }

    public void EnableControl()
    {
        health.Birth();

        canvasGameObject.SetActive(true);
    }

    public void Reset()
    {
        Instance.transform.position = spawnPoint.position;
        Instance.transform.rotation = spawnPoint.rotation;

        Instance.SetActive(false);
        Instance.SetActive(true);
    }

}
