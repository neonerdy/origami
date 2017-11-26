
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
using System.Linq;
using System.Xml.Linq;

namespace Origami.Container
{
    public class ConfigurationHandler
    {
        private XDocument xmlDoc;

        public ConfigurationHandler(string configFile)
        {
            xmlDoc = XDocument.Load(configFile);

        }
        
        private void InjectProperty(string objectId, object instance)
        {
            if (IsObjectHasProperty(objectId))
            {
                foreach (KeyValuePair<string, object> prop in GetPropertyDepedency(objectId))
                {
                    PropertyInjector.SetValue(instance, prop.Key, prop.Value);
                }
            }
        }

        private object CreateDepedencyInstance(string depedencyRef)
        {
            object depedencyInstance = null;

            Type t = System.Type.GetType(GetObjectType(depedencyRef));
            if (t == null)
            {
                throw new Exception("Invalid object type in configuration");
            }


            if (IsObjectHasConstructor(depedencyRef))
            {
                object[] d = GetConstructorDependency(depedencyRef);

                if (IsObjectSingleton(depedencyRef))
                {
                    depedencyInstance = Singleton.CreateInstance(t, d);
                }
                else
                {
                    depedencyInstance = Activator.CreateInstance(t, d);
                }
            }
            else
            {
                if (IsObjectSingleton(depedencyRef))
                {
                    depedencyInstance = Singleton.CreateInstance(t);
                }
                else
                {
                    depedencyInstance = Activator.CreateInstance(t);
                }
            }

            InjectProperty(depedencyRef, depedencyInstance);

            return depedencyInstance;
        }


        private object CastDepedencyValue(string type, string value)
        {
            object depedencyType = null;

            switch (type)
            {
                case "int":
                    depedencyType = Convert.ToInt32(value);
                    break;
                case "double":
                    depedencyType = Convert.ToDouble(value);
                    break;
                case "byte":
                    depedencyType = Convert.ToByte(value);
                    break;
                case "bool":
                    depedencyType = Convert.ToBoolean(value);
                    break;
                case "decimal":
                    depedencyType = Convert.ToDecimal(value);
                    break;
                case "datetime":
                    depedencyType = Convert.ToDateTime(value);
                    break;
                case "string":
                    depedencyType = value.ToString();
                    break;
                case "char":
                    depedencyType = Convert.ToChar(value);
                    break;
                case "single":
                    depedencyType = Convert.ToSingle(value);
                    break;
                case "guid" :
                    depedencyType = new Guid(value);
                    break;               
            }
            return depedencyType;
        }


        public bool IsObjectSingleton(string objectId)
        {
            var obj = from o in xmlDoc.Elements("container").Elements("objects").Elements("object")
                      where o.Attribute("id").Value == objectId &&
                      o.Attribute("singleton")!=null
                      select o;

            bool found=obj.Count() > 0;

            return found;
        }


        public bool IsObjectExist(string objectId)
        {
            var obj = from o in xmlDoc.Elements("container").Elements("objects").Elements("object")
                      where o.Attribute("id").Value == objectId
                      select o;

            bool found=obj.Count()>0;
            
            return found;
        }


        public string GetObjectType(string objectId)
        {
            var obj = from o in xmlDoc.Elements("container").Elements("objects").Elements("object")
                      where o.Attribute("id").Value == objectId
                      select o;

            string type = string.Empty;
            foreach (var o in obj)
            {
                type = o.Attribute("type").Value;
            }

            return type;

        }
        

        public bool IsObjectHasConstructor(string objectId)
        {
            var obj = from o in xmlDoc.Elements("container").Elements("objects").Elements("object")
                      where o.Attribute("id").Value == objectId
                      select o;

            var ctors = from c in obj.Elements("constructor-arg")
                        select c;

            bool found = ctors.Count() > 0;
            
            return found;
        }


        public bool IsObjectHasProperty(string objectId)
        {
            var obj = from o in xmlDoc.Elements("container").Elements("objects").Elements("object")
                      where o.Attribute("id").Value == objectId
                      select o;

            var props = from c in obj.Elements("property")
                        select c;

            int count = props.Count();

            return count > 0;
        }

        

        public object[] GetConstructorDependency(string objectId)
        {
            var obj = from o in xmlDoc.Elements("container").Elements("objects").Elements("object")
                    where o.Attribute("id").Value == objectId
                    select o;

            var ctors = from c in obj.Elements("constructor-arg")
                    select c;
            
            List<object> depedencies = new List<object>();
            
            foreach (var c in ctors)
            {

                object depedencyRef = null;
                object depedencyType = null;

                if (c.Attribute("ref")!=null)
                {
                    string ctorRef = c.Attribute("ref").Value;

                    depedencyRef = CreateDepedencyInstance(ctorRef);
                    depedencies.Add(depedencyRef);
                }
                else
                {
                    string type = c.Attribute("type").Value;
                    string value = c.Attribute("value").Value;

                    depedencyType = CastDepedencyValue(type, value);
                    depedencies.Add(depedencyType); 
                }         
             }
           
            return depedencies.ToArray();
        }

               

        public IDictionary<string, object> GetPropertyDepedency(string objectId)
        {
            var obj = from o in xmlDoc.Elements("container").Elements("objects").Elements("object")
                      where o.Attribute("id").Value == objectId
                      select o;

            var props = from c in obj.Elements("property")
                        select c;

            IDictionary<string, object> dic = new Dictionary<string, object>();

            foreach (var p in props)
            {
                string name = p.Attribute("name").Value;
               
                if (p.Attribute("ref")!=null)
                {
                    string propRef = p.Attribute("ref").Value;     

                    object depedencyRef = CreateDepedencyInstance(propRef);
                    dic.Add(name, depedencyRef);
                }
                else
                {
                    string value = p.Attribute("value").Value;
                    dic.Add(name, value);
                }
            }

            return dic;
        }        

    }
}
