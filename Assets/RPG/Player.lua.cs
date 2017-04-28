using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG
{

    [MoonSharpUserData]
    public class Lua_Player : Lua_Object
    {
        public override Object New()
        {
            return new Player();
        }

        public Lua_Player()
        {



        }

    }
}
