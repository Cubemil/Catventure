using System;
using UnityEngine;

namespace Gameplay.Systems.Managers
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public GameState state;

        private void Awake()
        {
            // Ensure this is a singleton instance
            if (_instance == null)
            {
                _instance = this;
                //DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void UpdateGameState(GameState newState)
        {
            state = newState;

            switch (newState)
            {
                case GameState.StartMenu:
                    HandleStartMenu();
                    break;
                case GameState.Cutscene:
                    HandleCutscene();
                    break;
                case GameState.Exploring:
                    HandleExploring();
                    break;
                case GameState.Quest:
                    HandleQuest();
                    break;
                case GameState.Dialogue:
                    HandleDialogue();
                    break;
                case GameState.Transitioning:
                    HandleTransitioning();
                    break;
                case GameState.Paused:
                    HandlePaused();
                    break;
                case GameState.EndGame:
                    HandleEndGame();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private void HandleStartMenu()
        {
            // Logic to handle the start menu
        }

        private void HandleCutscene()
        {
            // Logic to start a cutscene
        }

        private void HandleExploring()
        {
            // Logic for exploration phase
        }

        private void HandleQuest()
        {
            // Logic for quest handling
        }

        private void HandleDialogue()
        {
            // Logic for dialogues
        }

        private void HandleTransitioning()
        {
            // Logic for transitions between scenes
        }

        private void HandlePaused()
        {
            // Logic for pausing the game
        }

        private void HandleEndGame()
        {
            // Logic for ending the game (e.g., show credits)
        }
    }
}