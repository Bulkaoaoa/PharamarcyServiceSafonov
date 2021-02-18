using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharamarcyService.Entities
{
    public class AppData
    {
        public static PharamarcyServiceDBEntities Context = new PharamarcyServiceDBEntities();
        public static User CurrUser;
    }
}
