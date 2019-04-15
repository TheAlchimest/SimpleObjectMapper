using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SoSimple
{
    /// <summary>
    /// Class ObjectMapper.
    /// </summary>
    public class ObjectMapper
    {
        #region --------------FillObject--------------
        //---------------------------------------------------------------------
        //FillObject
        //---------------------------------------------------------------------
        /// <summary>
        /// Fills the taregeted object with the source object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        public static void FillObject(object source, object target)
        {
            if (source == null) return;

            Type sourceType = source.GetType();
            Type targetType = target.GetType();

            if (target == null)
            {
                target = Activator.CreateInstance(targetType);
            }

            int sourcePrpertiesCount = sourceType.GetProperties().Count();
            int targetPrpertiesCount = targetType.GetProperties().Count();

            if (sourcePrpertiesCount <= targetPrpertiesCount)
            {
                PropertyInfo targetP = null;
                foreach (PropertyInfo sourceProp in sourceType.GetProperties())
                {
                    targetP = targetType.GetProperty(sourceProp.Name);
                    if (targetP != null && targetP.CanWrite && (sourceProp.PropertyType.Namespace == "System") && (targetP.PropertyType.Namespace == "System"))
                    {
                        targetP.SetValue(target, sourceProp.GetValue(source));
                    }
                }
            }
            else
            {
                foreach (PropertyInfo targetProp in targetType.GetProperties())
                {
                    PropertyInfo sourceP = null;
                    sourceP = sourceType.GetProperty(targetProp.Name);
                    if (sourceP != null && (targetProp.PropertyType.Namespace == "System") && (sourceP.PropertyType.Namespace == "System"))
                    {
                        if (targetProp.CanWrite)
                        {
                            targetProp.SetValue(target, sourceP.GetValue(source));
                        }
                    }
                }
            }

        }
        //---------------------------------------------------------------------
        #endregion

        #region --------------GetEntity--------------
        //---------------------------------------------------------------------
        //GetEntity
        //---------------------------------------------------------------------
        /// <summary>
        /// return a genirec object after we assigned the data from the source object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns>T.</returns>
        public static T GetEntity<T>(object source) where T : class
        {
            if (source == null) return null;

            Type sourceType = source.GetType();
            Type targetType = typeof(T);

            T target = (T)Activator.CreateInstance(targetType);
            PropertyInfo targetProp = null;
            foreach (PropertyInfo sourceProp in sourceType.GetProperties())
            {
                targetProp = targetType.GetProperty(sourceProp.Name);
                if (targetProp != null && (sourceProp.PropertyType.Namespace == "System") && (targetProp.PropertyType.Namespace == "System"))
                {
                    object obg = sourceProp.GetValue(source);
                    if (targetProp.CanWrite)
                    {
                        targetProp.SetValue(target, obg);
                    }
                }
            }

            return (T)target;

        }
        //---------------------------------------------------------------------
        #endregion

        #region --------------GetEntityList--------------
        //---------------------------------------------------------------------
        //GetEntityList
        //---------------------------------------------------------------------
        /// <summary>
        /// Gets the entity list that mapped to the source obeject.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceList">The source list.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public static List<T> GetEntityList<T>(object sourceList) where T : class
        {
            if (!(sourceList.GetType().IsGenericType)) return null;


            if (sourceList == null) return null;

            List<T> targetList = new List<T>();
            Type targetType = typeof(T);



            foreach (var item in (IEnumerable)sourceList)
            {
                targetList.Add(GetEntity<T>(item));
            }
            return targetList;
        }
        //---------------------------------------------------------------------
        #endregion

    }
}
