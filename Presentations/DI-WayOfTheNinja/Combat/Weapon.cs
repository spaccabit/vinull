using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DI_WayOfTheNinja.Combat {
    public class Weapon {

        public String Name { get; set; }

        public virtual String Hit(String Target) {
            return String.Format("Hit {0}", Target);
        }
    }
}
