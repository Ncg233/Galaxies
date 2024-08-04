//
//                            _ooOoo_
//                           o8888888o
//                           88" . "88
//                           (| -_- |)
//                           O\  =  /O
//                        ____/`---'\____
//                      .'  \\|     |//  `.
//                     /  \\|||  :  |||//  \
//                    /  _||||| -:- |||||-  \
//                    |   | \\\  -  /// |   |
//                    | \_|  ''\---/''  |   |
//                    \  .-\__  `-`  ___/-. /
//                  ___`. .'  /--.--\  `. . __
//               ."" '<  `.___\_<|>_/___.'  >'"".
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//              \  \ `-.   \_ __\ /__ _/   .-` /  /
//         ======`-.____`-.___\_____/___.-`____.-'======
//                            `=---='
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ClientGalaxias.Client.Key
{
    public class KeyBind
    {
        public static Dictionary<string, KeyBind> keyBinds = new Dictionary<string, KeyBind>();
        public static Dictionary<Keys, bool> canExecutes = new Dictionary<Keys, bool>();
        public static KeyBind Inventory = new KeyBind("inventory", Keys.E);
        public static KeyBind Left = new KeyBind("left", Keys.A);
        public static KeyBind Right = new KeyBind("right", Keys.D);
        public static KeyBind Sprint = new KeyBind("sprint", Keys.LeftControl);
        public static KeyBind Down = new KeyBind("down", Keys.LeftShift);
        public static KeyBind Jump = new KeyBind("jump", Keys.Space);
        public static KeyBind Home = new KeyBind("home", Keys.H);
        public static KeyBind SetHome = new KeyBind("set_home", Keys.J);
        public static KeyBind FullScreen = new KeyBind("full_screen", Keys.F11, false);
        public static KeyBind DeBug = new KeyBind("debug", Keys.F3, false);
        public static KeyBind JetPack = new KeyBind("jetpack", Keys.L, false);
        public static bool Set(string name, Keys key)
        {
            if (keyBinds.ContainsKey(name))
            {
                keyBinds[name].key = key;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void Update(float dTime)
        {

        }
        public bool IsKeyDown()
        {
            if (canRepeatExecute)
            {
                return Keyboard.GetState().IsKeyDown(key);
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(key))
                {
                    if (!canExecutes[key])
                    {
                        canExecutes[key] = true;
                        return true;
                    }
                }
                else
                {
                    canExecutes[key] = false;
                }
            }
            return false;
        }
        public static Dictionary<string, KeyBind>.KeyCollection GetAvailableKeyNames()
        {
            return keyBinds.Keys;
        }
        public KeyBind(string name, Keys key, bool canRepeatExecute = true)
        {
            this.name = name;
            this.key = key;
            this.canRepeatExecute = canRepeatExecute;
            keyBinds[name] = this;
            canExecutes[key] = false;
        }

        public string name { get; set; }
        public Keys key { get; set; }

        private bool canRepeatExecute;
    }

}
