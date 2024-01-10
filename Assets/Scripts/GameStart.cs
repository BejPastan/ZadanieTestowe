using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField]
    TeamControl teamController;
    [SerializeField]
    UIManager UIManager;

    void Start()
    {
        StartPrototype();
    }

    private void StartPrototype()
    {
        teamController.FindCharacters();
        UIManager.GetCharacters(ref teamController);
    }
}
