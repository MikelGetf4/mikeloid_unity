using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    internal interface IDamagable
    {
        //Obligamos a declarar el metodo que tenga la logica del objetivo
        void TakeDamage();
    }
}
