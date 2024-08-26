namespace Gameplay.Systems
{
    public enum GameState
    {
        StartMenu,           // The initial menu before the game starts
        Cutscene,            // A cutscene is playing
        Exploring,           // The player is free to explore the world
        Quest,               // The player is actively pursuing a quest
        Dialogue,            // The player is in a dialogue
        Transitioning,       // Transition between scenes or major game phases
        Paused,              // The game is paused
        EndGame,             // The game is in the ending phase or credits
    }

}