using UnityEngine;

namespace Gameplay.Systems.Managers
{
    public class MusicManager : MonoBehaviour
    {
        private static MusicManager _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);
        }
    }
}