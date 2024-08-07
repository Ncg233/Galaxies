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
        public static KeyBind D1 = new KeyBind("1", Keys.D1);
        public static KeyBind D2 = new KeyBind("2", Keys.D2);
        public static KeyBind D3 = new KeyBind("3", Keys.D3);
        public static KeyBind D4 = new KeyBind("4", Keys.D4);
        public static KeyBind D5 = new KeyBind("5", Keys.D5);
        public static KeyBind D6 = new KeyBind("6", Keys.D6);
        public static KeyBind D7 = new KeyBind("7", Keys.D7);
        public static KeyBind D8 = new KeyBind("8", Keys.D8);
        public static KeyBind D9 = new KeyBind("9", Keys.D9);
        
        public static KeyBind Left = new KeyBind("left", Keys.A);
        public static KeyBind Right = new KeyBind("right", Keys.D);
        public static KeyBind Sprint = new KeyBind("sprint", Keys.LeftControl);
        public static KeyBind Down = new KeyBind("down", Keys.LeftShift);
        public static KeyBind Jump = new KeyBind("jump", Keys.Space);
        public static KeyBind Home = new KeyBind("home", Keys.H);
        public static KeyBind SetHome = new KeyBind("set_home", Keys.J);
        public static KeyBind FullScreen = new KeyBind("full_screen", Keys.F11);
        public static KeyBind DeBug = new KeyBind("debug", Keys.F3);
        public static KeyBind JetPack = new KeyBind("jetpack", Keys.L);
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
            return Keyboard.GetState().IsKeyDown(key);
        }
        public bool IsKeyPressed()
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
            return false;
        }
        public static Dictionary<string, KeyBind>.KeyCollection GetAvailableKeyNames()
        {
            return keyBinds.Keys;
        }
        public KeyBind(string name, Keys key)
        {
            this.name = name;
            this.key = key;
            keyBinds[name] = this;
            canExecutes[key] = false;
        }

        public string name { get; set; }
        public Keys key { get; set; }

        private bool canRepeatExecute;
    }

}
