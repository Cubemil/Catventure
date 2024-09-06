using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay.Systems.Managers
{
    public class Quest
    {
        public readonly string QuestName;
        public bool IsCompleted;

        public Quest(string questName)
        {
            QuestName = questName;
            IsCompleted = false;
        }
    }
    
    public sealed class QuestManager : MonoBehaviour
    {
        private readonly List<Quest> _quests = new();

        private void Start()
        {
            _quests.Add(new Quest("Explore Apartment"));
            _quests.Add(new Quest("Collect Apples"));
            _quests.Add(new Quest("Catch Frogs"));
            _quests.Add(new Quest("Maze Escape"));
            _quests.Add(new Quest("Catch Fish"));
            _quests.Add(new Quest("Flappy Cat"));
        }

        public void CompleteQuest(string questName)
        {
            var quest = _quests.Find(q => q.QuestName == questName);
            if (quest != null)
            {
                quest.IsCompleted = true;
                TriggerNextQuest(questName);
            }
        }

        private void TriggerNextQuest(string questName)
        {
            switch (questName)
            {
                case "Explore Apartment":
                    SceneManager.LoadScene("ApartmentFreeRoam");
                    break;
                case "Collect Apples":
                    SceneManager.LoadScene("Garden");
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}