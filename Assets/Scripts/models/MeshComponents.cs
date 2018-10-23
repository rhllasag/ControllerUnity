using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ObserverPattern
{
    

        public abstract class MeshEvents
        {
            public abstract float GetJumpForce();
        }

        public class JumpLittle : MeshEvents
        {
            public override float GetJumpForce()
            {
                return 30f;
            }
        }
    
}