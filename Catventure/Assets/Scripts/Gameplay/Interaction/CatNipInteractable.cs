using UnityEngine.SceneManagement;

namespace Gameplay.Interaction
{
    public class CatNipInteractable : ItemInteractable
    {
        public override void Interact()
        {
            if (gameObject) gameObject.SetActive(false);
            SceneManager.LoadScene("Apartment");
        }
    }
}