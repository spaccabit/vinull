using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DI_ByHand.Combat;

namespace DI_ByHand.Turtles {
    public class Foot : Warrior {

        public Foot() : base("Foot Soldier", new Sword()) { }

    }
}
