using TMPro;
using UnityEngine;

namespace Gameplay.Systems.Menus
{
    public class ChatBubble : MonoBehaviour
    {
        private SpriteRenderer _backgroundSpriteRenderer;
        private TextMeshPro _textMeshPro;
        
        private void Awake()
        {
            _backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
            _textMeshPro = transform.Find("Text").GetComponent <TextMeshPro>();
        }

        private void Start()
        {
            Setup("Hallo Welt, wie gehts :)");

        }
        private void Setup(string text)
        {
            _textMeshPro.SetText(text);
            _textMeshPro.ForceMeshUpdate();
            var textSize = _textMeshPro.GetRenderedValues(false); // render bounds
            
            var padding = new Vector2(4f, 2f);
            _backgroundSpriteRenderer.size = textSize + padding;
        }
    }
}
