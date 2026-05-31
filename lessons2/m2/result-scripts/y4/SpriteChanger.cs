using UnityEngine;
using UnityEngine.UI;

namespace Lessons.M2.Y4
{
    public class SpriteChanger : MonoBehaviour
    {
        public SpriteRenderer targetRenderer;
        public Sprite newSprite;
        public Button triggerButton;

        private void Start()
        {
            triggerButton.onClick.AddListener(ChangeSprite);
        }

        public void ChangeSprite()
        {
            targetRenderer.sprite = newSprite;
        }

        private void OnDestroy()
        {
            triggerButton.onClick.RemoveListener(ChangeSprite);
        }
    }
}

