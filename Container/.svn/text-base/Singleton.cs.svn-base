
/*===========================================================
 * 
 * Origami (Object Relational Gateway Microarchitecture)
 * 
 * Lightweight Enterprise Application Framework
 *
 * Version  : 3.0
 * Author   : Ariyanto
 * E-Mail   : neonerdy@yahoo.com
 *  
 * 
 * © 2009, Under Apache Licence 
 * 
 *==========================================================
 */

using System;
using System.Collections.Generic;

namespace Origami.Container
{
    public class Singleton
    {
        static Dictionary<string, object> instanceRepository = new Dictionary<string, object>();
        
        private Singleton()
        {
        }

        public static object CreateInstance(Type t)
        {
            return Instantiate(t, null);
        }

        public static object CreateInstance(Type t,object[] depedency)
        {
            return Instantiate(t, depedency);
        }

        static object Instantiate(Type t, object[] depedency)
        {
            if (!instanceRepository.ContainsKey(t.FullName))
            {
                object instance = null;

                if(depedency==null)
                    instance = Activator.CreateInstance(t);
                else
                    instance = Activator.CreateInstance(t, depedency);

                instanceRepository.Add(t.FullName, instance);
            }

            return instanceRepository[t.FullName];
        }
    }
}
