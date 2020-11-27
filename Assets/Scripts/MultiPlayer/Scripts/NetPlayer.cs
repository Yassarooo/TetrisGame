using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror
{

    public class NetPlayer : NormalPlayer
    {
        public bool isactive = false;
        private void Awake()
        {
       /*    gr = Instantiate(gr, new Vector3(15, 0, 0), Quaternion.identity);
           sp = Instantiate(sp, new Vector3(15, 0, 0), Quaternion.identity);*/
        }
        private void Update()
        {
            if (isactive)
            {
                base.Update();
            }
        }
    }
}