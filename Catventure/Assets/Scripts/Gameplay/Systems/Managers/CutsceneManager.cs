using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace Gameplay.Systems.Managers
{
    public class CutsceneManager
    {
        [SerializeField]
        private readonly PlayableDirector _director;
        [SerializeField]
        private readonly string _nextSceneName;

        public CutsceneManager(PlayableDirector director, string nextSceneName)
        {
            this._director = director;
            _nextSceneName = nextSceneName;
        }

        public void Start()
        {
            _director.stopped += OnCutsceneEnd;
        }

        protected virtual void OnCutsceneEnd(PlayableDirector pd)
        {
            SceneManager.LoadScene(_nextSceneName);
        }
    }
}