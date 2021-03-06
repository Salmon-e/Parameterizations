﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Microsoft.Xna.Framework;
namespace Parameterizations
{
    /**
     * Represents a parameterization of the motion of a point 
     */
    class Movement
    {
        ArrayList xParam = new ArrayList();
        ArrayList yParam = new ArrayList();
        public float timeUpper, timeLower, time, timeStep;
        public int timeDirection = 1;

        public TimeRoundMethod roundMethod = TimeRoundMethod.REVERSE;
        public Movement(Func<float, float> xPara, Func<float, float> yPara)
        {
            
            xParam.Add(xPara);
            yParam.Add(yPara);
            timeStep = 1;
            timeLower = 0;
            timeUpper = 1;
        }
        public Movement()
        {            
            timeStep = 1;
            timeLower = 0;
            timeUpper = 1;
        }
        public void Tick()
        {
            time += timeStep * timeDirection;

            if(time > timeUpper || time < timeLower)
            {
                RoundTime();
            }
        }
        /**
         * Handles time overflowing its bounds
         */
         
        protected void RoundTime()
        {
            
            switch (roundMethod)
            {
                
                case TimeRoundMethod.REVERSE:
                    timeDirection *= -1;
                    if (time > timeUpper)
                        time = timeUpper;
                    else
                        time = timeLower;
                    break;
                case TimeRoundMethod.WRAP:
                    if (time > timeUpper)
                        time = timeLower;
                    else
                        time = timeUpper;
                    break;
                case TimeRoundMethod.REVERSE_WRAP:
                    timeDirection *= -1;
                    if (time > timeUpper)
                        time = timeLower;
                    else
                        time = timeUpper;
                    break;

            }
        }
        
        public Vector2 GetPosition()
        {
            float x = 0, y = 0;
            foreach(Func<float, float> func in xParam)
            {
                x += func(time);
            }
            foreach(Func<float, float> func in yParam)
            {
                y += func(time);
            }
            return new Vector2(x, y);
        }
        /**
         * Add the equations of two movements.
         */
        public void AddMovement(Movement movement)
        {
            xParam.AddRange(movement.xParam);
            yParam.AddRange(movement.yParam);
        }
        /**
         * Helper function to create a circular movement.
         */

        public static Movement CreateOrbit(Vector2 center, float radius, float orbitTime)
        {
            Func<float, float> circleX = (time) => (center.X + radius * (float) Math.Cos(time*Math.PI*2/orbitTime));
            Func<float, float> circleY = (time) => (center.Y + radius * (float) Math.Sin(time*Math.PI*2/orbitTime));

            return new Movement(circleX, circleY);
        }
        /**
         * Helper function to create a linear movement
         */
        public static Movement CreateLine(Vector2 start, Vector2 end, float speed)
        {
            float dx = end.X - start.X;
            float dy = end.Y - start.Y;

            Func<float, float> x = (time) => (start.X + dx * time * speed);
            Func<float, float> y = (time) => (start.Y + dy * time * speed);

            return new Movement(x, y);
        }


        

    }
    /**
     * Different ways of handling reaching the time bounds.
     * Reverse reverses the direction of time when either bound is reached.
     * Wrap teleports back to the begining.
     * Reverse wrap teleports back to the beginning and reverses time,
     * this is useful if a movement in a chain loops back to its beginning. 
     * Once the loop is complete it will wrap back to the starting point and reverse so it goes into the previous movement of the chain.
     */
    public enum TimeRoundMethod
    {
        REVERSE,
        WRAP,
        REVERSE_WRAP
    }
}
