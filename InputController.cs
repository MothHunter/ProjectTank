using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTank
{
    internal class InputController
    {
        // state variables for keyboard and mouse in the current and previous frame
        // previous frame states are used to distinguish between initial key/button press
        // and holding down a key/button
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;

        private MouseState currentMouseState;
        private MouseState previousMouseState;

        private static InputController instance;

        public InputController()
        {
            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
            previousKeyboardState = currentKeyboardState;
            previousMouseState = currentMouseState;
        }

        public static InputController GetInstance()
        {
            if (instance == null)
                instance = new InputController();
            return instance;
        }

        public void Update()
        {
            previousKeyboardState = currentKeyboardState;
            previousMouseState = currentMouseState;

            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
        }

        /// <summary>
        /// Returns whether a key is currently down; independednt of previous state
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>true if key is down, false otherwise</returns>
        public bool GetKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Returns whether a key press has occured in this frame; only evaluates to true once
        /// until the key gets released again
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>true if the key has been pressed this frame, false if it is up or was 
        /// already down in the previous frame</returns>
        public bool GetKeyPressed(Keys key)
        {
            return (!previousKeyboardState.IsKeyDown(key) && currentKeyboardState.IsKeyDown(key));            
        }

        /// <summary>
        /// Method <c>GetChar</c> returns the first currently pressed char or null if none are pressed
        /// </summary>
        /// <returns>The char as a String</returns>
        public String GetChar()
            
        {
            if (currentKeyboardState.GetPressedKeys().Length > 0 && 
                currentKeyboardState.GetPressedKeys()[0].ToString().Length == 1
                && !previousKeyboardState.IsKeyDown(currentKeyboardState.GetPressedKeys()[0]))
            {
                
                return currentKeyboardState.GetPressedKeys()[0].ToString();
            }
            return null;
        }

        /// <summary>
        /// Returns whether the left mouse button has been pressed during this frame;
        /// only evaluates to true once until the button gets released again
        /// </summary>
        /// <returns></returns>
        public bool GetLeftClick()
        {
            return (previousMouseState.LeftButton != ButtonState.Pressed &&
                currentMouseState.LeftButton == ButtonState.Pressed);
        }

        /// <summary>
        /// Returns whether the right mouse button has been pressed during this frame;
        /// only evaluates to true once until the button gets released again
        /// </summary>
        /// <returns></returns>
        public bool GetRightClick()
        {
            return (previousMouseState.RightButton != ButtonState.Pressed &&
                currentMouseState.RightButton == ButtonState.Pressed);
        }

        /// <summary>
        /// Returns the coordínates of the mouse cursor
        /// </summary>
        /// <returns></returns>
        public Vector2 GetCursorPosition()
        {
            return new Vector2(currentMouseState.X, currentMouseState.Y);
        }
    }
}
