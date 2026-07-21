using UnityEngine;
using Mirror;
using UnityEngine.Events;
namespace Eloi.UnicodeWatch
{
    [RequireComponent(typeof(NetworkIdentity))]
    public class UnicodeWatchMirrorMono_BasicPlayer : NetworkBehaviour
    {


        public float m_randomSpotAtStart = 2f;

        public override void OnStartClient()
        {
            base.OnStartClient();
            if (!isOwned)
                return;
            transform.position = new Vector3(Random.Range(0f, m_randomSpotAtStart), Random.Range(0f, m_randomSpotAtStart), Random.Range(0f, m_randomSpotAtStart));
        }

        public UnityEvent<Color> m_onColorChanged;

        [ContextMenu("Set new Random Color")]
        public void SetNewRandomColor()
        {
            Color c = new Color(Random.value, Random.value, Random.value);
            CmdSetColor(c);
        }

        [Command]
        private void CmdSetColor(Color c)
        {
            RpcSetColor(c);
        }

        [ClientRpc]
        private void RpcSetColor(Color c)
        {
            m_onColorChanged?.Invoke(c);
        }


    }
}
