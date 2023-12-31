using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomEventSys
{
    public class BrickParticleListener : MonoBehaviour
    {
        public GameObject particlePrefab;

        private void OnEnable()
        {
            BallFeedBackSource.BrickParticleNotify += Play;
        }

        private void OnDisable()
        {
            BallFeedBackSource.BrickParticleNotify -= Play;
        }

        private void Play(Vector2 position)
        {
            StartCoroutine(P_Play(position));
        }

        private IEnumerator P_Play(Vector2 position)
        {
            var efx = Instantiate(particlePrefab, position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            Destroy(efx);
        }
    }
}