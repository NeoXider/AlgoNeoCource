using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lessons.M2.Y5
{
    public class ClickerGame : MonoBehaviour, IPointerDownHandler
    {
        public float score = 0;
        public int clickPower = 1;
        public int penaltyAmount = 5;
        public TMP_Text textScore;

        public AudioClip clickClip;
        public ParticleSystem clickFxPrefab;

        private AudioSource clickAudioSource;

        private void Awake()
        {
            clickAudioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            UpgradeClickPower(2);
            UpdateScoreText();
        }

        private void Update()
        {
            UpdateScore();
            UpdateScale();
            UpdateScoreText();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            score += clickPower;
            PlayClickFeedback();
        }

        private void UpdateScore()
        {
            score -= penaltyAmount * Time.deltaTime;
        }

        private void UpdateScale()
        {
            transform.localScale = Vector3.one * (1 + score / 100f);
        }

        private void UpdateScoreText()
        {
            if (textScore != null)
            {
                textScore.text = ((int)score).ToString();
            }
        }

        private void UpgradeClickPower(int bonus)
        {
            clickPower += bonus;
            Debug.Log("Усиление куплено! Сила клика: " + clickPower);
        }

        private void PlayClickFeedback()
        {
            if (clickAudioSource != null && clickClip != null)
            {
                clickAudioSource.PlayOneShot(clickClip);
            }

            if (clickFxPrefab != null)
            {
                ParticleSystem fx = Instantiate(clickFxPrefab, transform.position, Quaternion.identity);
                fx.Play();
                Destroy(fx.gameObject, 1f);
            }
        }
    }
}

