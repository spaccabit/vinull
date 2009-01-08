using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Di_Ninject.Combat {
    public class Sword : IWeapon {

        #region IWeapon Members

        public String Name { get; set; }

        public string Hit(string Target) {
            return String.Format("slices {0}", Target);
        }

        #endregion
    }
}
