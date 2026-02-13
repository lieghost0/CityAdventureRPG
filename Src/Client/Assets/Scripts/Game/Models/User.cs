using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;

namespace Models
{
    public class User
    {
        static User instance;

        public static User Instance
        {
            get
            {
                if (instance == null)
                    instance = new User();
                return instance;
            }
        }

        public Player player;

        public void Init()
        {
            player = new Player(1, "玩家");
        }
    }
}