using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Core;
using Ninject.Conditions;
using Di_Ninject.Combat;

namespace Di_Ninject.Turtles {
    public class TurtleModel : StandardModule {

        public override void Load() {
            Bind<IWeapon>().To<Sword>();
            Bind<IWeapon>().To<Nunchucks>().ForMembersOf<Mike>();

            Bind<String>().ToConstant("Warrior");
            Bind<String>().ToConstant("Michelangelo").ForMembersOf<Mike>();
            Bind<String>().ToConstant("Foot Soldier").ForMembersOf<Foot>();
            Bind<String>().ToConstant("Leonardo").ForMembersOf<Leo>();

        }
    }

    public class Mike : Warrior { }
    public class Foot : Warrior { }
    public class Leo : Warrior { }
}