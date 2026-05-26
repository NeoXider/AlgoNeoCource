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
            if (triggerButton != null)
            {
                triggerButton.onClick.AddListener(ChangeSprite);
            }
        }

        public void ChangeSprite()
        {
            if (targetRenderer != null && newSprite != null)
            {
                targetRenderer.sprite = newSprite;
            }
        }

        private void OnDestroy()
        {
            if (triggerButton != null)
            {
                triggerButton.onClick.RemoveListener(ChangeSprite);
            }
        }
    }
}

