using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

    public abstract class Player2 : MonoBehaviour
    {
        private Block2 CurrentBlock;
        public Spawner2 sp;
        protected MoveFactory2 mf = new MoveFactory2();
        protected Move2 move2;
        public Grid2 gr;
        public int score=0;
        public TextMeshProUGUI point;
        public TextMeshProUGUI finalscore;
        public GameObject gameOver;
        public GameObject win;
        public GameObject lose;
        public GameObject draw;
        

        private void Awake()
        {
            if (gameOver != null)
            gameOver.SetActive(false);
            if (win != null)
                win.SetActive(false);
            if (lose != null)
                lose.SetActive(false);
            if (draw != null)
                draw.SetActive(false);
        }

        protected void Update()
        {            
           SetPoint ();
        }

        public void SetPoint () {
        if (point != null)
            point.text = "SCORE \n" +score;
}
        public void disable()
        {
            sp.enabled = false;
            gr.enabled = false;
            if (gameOver != null)
            {
                gameOver.SetActive(true);
                finalscore.text = "Your Score " + score;
            }
            this.enabled = false;
        }
    }