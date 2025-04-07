using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class MainMenuState : GameState
    {
        public GameState gamePlayState;
        public TMP_Text scoreText;
        public LevelContr LevelContr;

        public void PlayGame()
        {
            Exit();
            gamePlayState.Enter();
        }
        protected override void OnEnable()
        {
            base.OnEnable();

            scoreText.text = $" High Score: {LevelContr.hightScore}";
        }
    }
}
