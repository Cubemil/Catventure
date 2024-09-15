using TMPro;
using UnityEngine;

namespace Gameplay.Systems.Quests
{
    public class ReturnToRealWorldQuest : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI questLogText;
        public bool questStarted;
        private bool _questCompleted;

        private void Start()
        {
            questLogText.gameObject.SetActive(false);
            questLogText.text = $"Start flying behind Britt Bird to go home";
        }

        public void StartQuest()
        {
            questLogText.gameObject.SetActive(true);
            questStarted = true;
        }
    }
}