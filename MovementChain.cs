using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Text;

namespace Parameterizations
{
    /**
     * A chain of movements to be executed back to back
     */
    class MovementChain
    {
        ArrayList movements = new ArrayList();
        Movement currentMovement;
        int currentIndex;
        public MovementChain AddMovement(Movement movement)
        {
            movements.Add(movement);
            return this;
        }
        /**
         * Updates the movement
         */
        public void Tick()
        {

            if (currentMovement == null)
            {
                currentMovement = (Movement)movements[0];
                currentIndex = 0;
            }
            if (currentMovement.time <= currentMovement.timeLower && currentIndex != 0 && currentMovement.timeDirection == -1)
            {
                currentMovement = (Movement) movements[currentIndex - 1];
                currentMovement.timeDirection = -1;
                currentIndex--;
            }
            if (currentMovement.time >= currentMovement.timeUpper && currentIndex != movements.Count - 1 && currentMovement.timeDirection == 1)
            {
                currentMovement = (Movement)movements[currentIndex + 1];
                currentMovement.timeDirection = 1;
                currentIndex++;
            }

            currentMovement.Tick();
        }
        public Vector2 GetPosition()
        {
            return currentMovement.GetPosition();
        }

    }
}
