using DG.Tweening;
using UnityEngine;
using Managers;
using Characters;

namespace Collectibles
{
    public class CoinController : MonoBehaviour
    {
        private Vector3 _uiScreenPosition;
        private Vector3 _uiWorldPosition;

        private void Start()
        {
            //Constant UI position in screen space
            _uiScreenPosition = CanvasManager.Instance.CoinTransform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Player player))
            {
                CollectCoin();
            }
        }

        private void CollectCoin()
        {
            VFXManager.PlayVFX?.Invoke("CoinCollect", this.transform.position, Quaternion.identity);

            //UI screen position to world position
            _uiWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(_uiScreenPosition.x, _uiScreenPosition.y, Camera.main.nearClipPlane));


            //Here I use Dotween as requested.
            transform.DOMove(_uiWorldPosition, 0.2f).SetEase(Ease.InCubic).OnComplete(() =>
            {
                GameManager.CoinCollected?.Invoke();
                Destroy(this.gameObject,1f);
            });



        }
    }
}

