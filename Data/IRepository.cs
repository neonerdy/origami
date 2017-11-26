
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

using System.Collections.Generic;

namespace Origami.Data
{
    public interface IRepository<T>
    {
        T FindById(object id);
        List<T> FindAll();
        int Save(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}
