using UnityEngine;
using Gameplay.Systems.Quests;
using System.Collections.Generic;

namespace Gameplay.Systems.Managers
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private AppleCollectorQuest appleCollectorQuest;
        [SerializeField] private TadpoleCatcherQuest tadpoleCatcherQuest;

        private Queue<IQuest> _questQueue;
        private IQuest _currentQuest;

        private void Start()
        {
            _questQueue = new Queue<IQuest>();

            _questQueue.Enqueue(appleCollectorQuest);
            _questQueue.Enqueue(tadpoleCatcherQuest);
        }

        private void Update()
        {
            if (_currentQuest != null && _currentQuest.IsQuestCompleted())
            {
                CompleteCurrentQuest();
            }
        }

        private void StartNextQuest()
        {
            if (_questQueue.Count <= 0) return;
            
            _currentQuest = _questQueue.Dequeue();
            _currentQuest.StartQuest();
        }

        private void CompleteCurrentQuest()
        {
            _currentQuest.CompleteQuest();
            _currentQuest = null;
            StartNextQuest();
        }
    }

    public interface IQuest
    {
        void StartQuest();
        void CompleteQuest();
        bool IsQuestCompleted();
    }
}