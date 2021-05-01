using PlayerControl;
using System.Collections;
using UnityEngine;

namespace Item
{
    public class Interactable : MonoBehaviour
    {
        [field: SerializeField] public float radius { get; set; } = .6f;
        [field: SerializeField] public string interactableText { get; set; }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, radius);
        }
        public virtual void Interact(PlayerManager playerManager)
        {
            Debug.Log("You interacted with an object");
        }

    }
}