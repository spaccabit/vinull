using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Di_Ninject.Combat {
    public class Nunchucks : IWeapon {

        #region IWeapon Members

        public string Name { get; set; }

        public string Hit(string Target) {
            return String.Format("wacks {0}", Target);
        }

        #endregion
    }
}
