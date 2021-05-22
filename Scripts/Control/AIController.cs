using Game.Core;
using System.Collections;
using UnityEngine;

namespace Game.Control
{
    public class AIController : MonoBehaviour
    {

        Health health;

        private void Start()
        {
            health = GetComponent<Health>();
        }
        private void Update()
        {
            if (health.IsDead()) return;

            //Move(xAxis);
            //Recoil();
            //Attack();
        }
    }
}