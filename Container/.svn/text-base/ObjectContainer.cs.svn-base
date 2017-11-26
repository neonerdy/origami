
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
using System.Reflection;
using System.Collections.Generic;

namespace Origami.Container
{
    public class ObjectContainer
    {
        private static Dictionary<object, Type> repositories = new Dictionary<object, Type>();
        private static IDictionary<string, object> propertyDependency;
        private static object[] constructorDependency;
        private static string configFile;
        private static bool isSingleton;
         
        private static T GetObjectFromConfig<T>(string objectId, T instance)
        {
            if (configFile == null)
            {
                throw new Exception("Please configure application config");
            }

            ConfigurationHandler configurationHandler = new ConfigurationHandler(configFile);

            if (!configurationHandler.IsObjectExist(objectId))
            {
                throw new Exception("Object '" + objectId + "' is not exist in configuration");
            }
            else
            {
                if (configurationHandler.IsObjectHasConstructor(objectId))
                {
                    object[] d = configurationHandler.GetConstructorDependency(objectId);
                    instance = ConstructObject<T>(configurationHandler, objectId, d);
                }
                else
                {
                    instance = ConstructObject<T>(configurationHandler, objectId,
                         constructorDependency);
                }

                InjectProperty<T>(objectId, instance, configurationHandler);
            }
            return instance;
        }


        private static T ConstructObject<T>(ConfigurationHandler configurationHandler,
            string objectId, object[] depedency)
        {
            string type = configurationHandler.GetObjectType(objectId);
            
            Type t = System.Type.GetType(type);
            if (t == null)
            {
                throw new Exception("Invalid object type in configuration");
            }

            T instance = default(T);

            if (depedency != null)
            {
                if (configurationHandler.IsObjectSingleton(objectId))
                {
                    instance = (T)Singleton.CreateInstance(t, depedency);
                }
                else
                {
                    instance = (T)Activator.CreateInstance(t, depedency);
                }
            }
            else
            {
                if (configurationHandler.IsObjectSingleton(objectId))
                {
                    instance = (T)Singleton.CreateInstance(t);
                }
                else
                {
                    instance = (T)Activator.CreateInstance(t);
                }

            }

            return instance;
        }


        private static void InjectProperty<T>(string objectId, T instance,
            ConfigurationHandler configurationHandler)
        {
            if (propertyDependency != null)
            {
                InjectPropertyInRepository<T>(instance);
            }
            else
            {
                InjectPropertyInConfig<T>(objectId, instance, configurationHandler);
            }
        }


        private static T GetObjectFromRepository<T>(string objectId, T instance)
        {
            if (constructorDependency != null)
            {
                if (isSingleton)
                {
                    instance = (T)Singleton.CreateInstance(repositories[objectId],
                        constructorDependency);
                }
                else
                {                   
                    
                    instance = (T)Activator.CreateInstance(repositories[objectId],
                        constructorDependency);
                }
                InjectPropertyInRepository<T>(instance);
            }
            else
            {
                if (isSingleton)
                {
                    instance = (T)Singleton.CreateInstance(repositories[objectId]);
                }
                else
                {
                    instance = (T)Activator.CreateInstance(repositories[objectId]);
                }
                InjectPropertyInRepository<T>(instance);
            }
            return instance;
        }



        private static void InjectPropertyInRepository<T>(T instance)
        {
            if (propertyDependency != null)
            {
                foreach (KeyValuePair<string, object> prop in propertyDependency)
                {
                    PropertyInjector.SetValue(instance, prop.Key, prop.Value);
                }
            }
        }


        private static void InjectPropertyInConfig<T>(string objectId, T instance,
            ConfigurationHandler configurationHandler)
        {
            if (configurationHandler.IsObjectHasProperty(objectId))
            {
                foreach (KeyValuePair<string, object> prop in configurationHandler
                    .GetPropertyDepedency(objectId))
                {
                    PropertyInjector.SetValue(instance, prop.Key, prop.Value);
                }
            }
        }            

        public static void RegisterObject(string objectId, Type type)
        {
            repositories.Add(objectId, type);
        }

        public static void RegisterObject(string objectId, Type type, object[] constructorDependency)
        {
            repositories.Add(objectId, type);
            ObjectContainer.constructorDependency = constructorDependency;
        }
                

        public static void RegisterObject(string objectId, Type type,
            IDictionary<string, object> propertyDependency)
        {
            repositories.Add(objectId, type);
            ObjectContainer.propertyDependency = propertyDependency;
        }
        
        public static void RegisterObject(string objectId, Type type,
            object[] constructorDependency,IDictionary<string, object> propertyDependency)
        {
            repositories.Add(objectId, type);
            ObjectContainer.constructorDependency = constructorDependency;
            ObjectContainer.propertyDependency = propertyDependency;
        }

        public static void RegisterObjectAsSingleton(string objectId, Type type)
        {
            repositories.Add(objectId, type);
            ObjectContainer.isSingleton = true;
        }

        public static void RegisterObjectAsSingleton(string objectId, Type type, 
            object[] constructorDependency)
        {
            repositories.Add(objectId, type);
            ObjectContainer.constructorDependency = constructorDependency;
            ObjectContainer.isSingleton = true;
        }

        
        public static void RegisterObjectAsSingleton(string objectId, Type type,
          IDictionary<string, object> propertyDependency)
        {
            repositories.Add(objectId, type);
            ObjectContainer.propertyDependency = propertyDependency;
            ObjectContainer.isSingleton = true;
        }
        

        public static void RegisterObjectAsSingleton(string objectId, Type type,
            object[] constructorDependency, IDictionary<string, object> propertyDependency)
        {
            repositories.Add(objectId, type);
            ObjectContainer.constructorDependency = constructorDependency;
            ObjectContainer.propertyDependency = propertyDependency;
            ObjectContainer.isSingleton = true;
        }


        public static T GetObject<T>(string objectId)
        {
            T instance = default(T);
            try
            {              
                if (repositories.ContainsKey(objectId))
                {
                    instance = GetObjectFromRepository<T>(objectId, instance);
                }
                else
                {
                    instance = GetObjectFromConfig<T>(objectId, instance);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return instance;
        }
 
        public static void RegisterDepedency(object[] contructorDepedency)
        {
            ObjectContainer.constructorDependency = contructorDepedency;
        }

        public static void RegisterDepedency(IDictionary<string,object> propertyDepedency)
        {
            ObjectContainer.propertyDependency = propertyDepedency;
        }

       
        public static void AddRegistry(IRegistry registry)
        {
            registry.Configure();
        }

        public static void SetApplicationConfig(string configFile)
        {
            ObjectContainer.configFile = configFile;
        }

    }
}
