﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class SecondPlayer : Player2
    {

        public void CheckInput()
        {
            if ((Input.GetKeyDown("s")))
            {
                move2 = mf.PerformMove("Down", sp.activeBlock, gr, sp, Block2.fallTime / 10);
            }
            else
                move2 = mf.PerformMove("Down", sp.activeBlock, gr, sp, Block2.fallTime);
            if ((Input.GetKeyDown("a")) && Time.timeScale != 0)
            {
                move2 = mf.PerformMove("Left", sp.activeBlock, gr, sp);
            }
            else if ((Input.GetKeyDown("d")) && Time.timeScale != 0)
            {

                move2 = mf.PerformMove("Right", sp.activeBlock, gr, sp);
            }
            else if ((Input.GetKeyDown("w")) && Time.timeScale != 0)
            {

                move2 = mf.PerformMove("Up", sp.activeBlock, gr, sp);
            }
        }

        private void Update()
        {
            base.Update();
                CheckInput();
        }
    }
