
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
using System.ComponentModel;

namespace Origami.Container
{
    public class PropertyInjector
    {
        public static void SetValue(object instance,string property,object value)
        {
            try
            {
                PropertyDescriptorCollection propertyDescriptor = TypeDescriptor.GetProperties(instance);
                System.ComponentModel.PropertyDescriptor myProperty = propertyDescriptor.Find(property, false);
                myProperty.SetValue(instance, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }


        public static object GetValue(object instance,string property)
        {
            object value = null;
            try
            {
                PropertyDescriptorCollection propertyDescriptor = TypeDescriptor.GetProperties(instance);
                System.ComponentModel.PropertyDescriptor myProperty = propertyDescriptor.Find(property, false);
                value = myProperty.GetValue(instance);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return value;

        }
                   
     }
}
