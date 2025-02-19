using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Systems.Managers
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] 
        private Slider volumeSlider;
        
        // Start is called before the first frame update
        private void Start()
        {
            if (!PlayerPrefs.HasKey("musicVolume"))
            {
                PlayerPrefs.SetFloat("musicVolume", 1);
                Load();
            }
            else Load();
        }

        public void ChangeVolume()
        {
            AudioListener.volume = volumeSlider.value;
            Save();
        }

        private void Load()
        {
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }

        private void Save()
        {
            PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        }
    }
}