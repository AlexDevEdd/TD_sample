using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LoadingTree
{
    public sealed class LoadingScreen : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _errorText;

        [SerializeField]
        private ProgressBar _progressBar;

        public void Show(bool reset = true)
        {
            if (reset)
            {
                ResetProgress();
                ResetError();
            }
            
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetProgress(float progress)
        {
            _progressBar.SetProgress(progress);
        }

        public void ResetProgress()
        {
            _progressBar.SetProgress(0.0f);
        }

        public void SetError(string message)
        {
            _errorText.text = message;
        }

        public void ResetError()
        {
            _errorText.text = string.Empty;
        }

        [Serializable]
        private sealed class ProgressBar
        {
            [SerializeField]
            private TMP_Text _text;

           [SerializeField]
            private Image _fill;

            public void SetProgress(float progress)
            {
                _fill.fillAmount = progress;
                _text.text = $"{progress * 100:F0}%";
            }
        }
    }
}