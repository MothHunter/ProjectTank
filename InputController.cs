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
        private MouseState prviousMouseState;

        private static InputController instance;
        public static InputController GetInstance()
        {
            if (instance == null)
                instance = new InputController();
            return instance;
        }

        public void Update(KeyboardState kState, MouseState mState)
        {
            previousKeyboardState = currentKeyboardState;
            prviousMouseState = currentMouseState;

            currentKeyboardState = kState;
            currentMouseState = mState;
        }

        public bool GetKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public bool GetKeyPressed(Keys key)
        {
            return (!previousKeyboardState.IsKeyDown(key) && currentKeyboardState.IsKeyDown(key));            
        }

    }
}
