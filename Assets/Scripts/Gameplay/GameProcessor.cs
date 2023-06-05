using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class GameProcessor : MonoBehaviour
    {
        [SerializeField] private SignalBus _signalBus;
        [SerializeField] private ParticleSystem _destroyVFX;

        private void OnEnable()
        {
            _signalBus.PlayerKilled += OnPlayerKilled;
        }

        private void OnPlayerKilled(Vector3 killPosition)
        {
            _destroyVFX.transform.position = killPosition;
            _destroyVFX.gameObject.SetActive(true);
            _destroyVFX.Play(true);

            StartCoroutine(WaitForSeconds());
        }

        private IEnumerator WaitForSeconds()
        {
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnDisable()
        {
            _signalBus.PlayerKilled -= OnPlayerKilled;
        }
    }
}