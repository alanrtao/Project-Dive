﻿using Helpers;

namespace Player
{
    public partial class PlayerStateMachine
    {
        public class Dogoing : PlayerState
        {
            public override void Enter(PlayerStateInput i)
            {
                print("Transition to dogoing");
                i.oldVelocity = PlayerAction.Dogo();
                i.dogoXVBufferTimer = GameTimer.StartNewTimer(PlayerInfo.DogoConserveXVTime);
            }

            public override void JumpPressed()
            {
                base.JumpPressed();
                MySM.Transition<DogoJumping>();
            }

            public override void FixedUpdate()
            {
                GameTimer.FixedUpdate(Input.dogoXVBufferTimer);

                base.FixedUpdate();
            }

            public override void MoveX(int moveDirection)
            {
                UpdateSpriteFacing(moveDirection);
            }
        }
    }
}