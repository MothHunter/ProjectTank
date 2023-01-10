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
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;

        private MouseState currentMouseState;
        private MouseState previousMouseState;

        private static InputController instance;

        public InputController()
        {
            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
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

        public bool GetKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public bool GetKeyPressed(Keys key)
        {
            return (!previousKeyboardState.IsKeyDown(key) && currentKeyboardState.IsKeyDown(key));            
        }

        /// <summary>
        /// Method <c>GetChar</c> returns the first currently pressed char or null if none are pressed
        /// </summary>
        /// <returns></returns>
        public String GetChar()
        {
            if (currentKeyboardState.GetPressedKeys().Length > 0 && 
                currentKeyboardState.GetPressedKeys()[0].ToString().Length == 1) 
            {
                return currentKeyboardState.GetPressedKeys()[0].ToString();
            }
            return null;
        }

        public bool GetLeftClick()
        {
            return (previousMouseState.LeftButton != ButtonState.Pressed &&
                currentMouseState.LeftButton == ButtonState.Pressed);
        }
        public bool GetRightClick()
        {
            return (previousMouseState.RightButton != ButtonState.Pressed &&
                currentMouseState.RightButton == ButtonState.Pressed);
        }
    }
}
