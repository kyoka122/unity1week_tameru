using System;

namespace Tameru.Application
{
    public static class EnumHelper
    {
        /// <summary>
        /// Enumを指定しない場合、-1を返す
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int Num<T>()
        {
            if (typeof(T).IsEnum)
            {
                return Enum.GetValues(typeof(T)).Length;
            }

            return -1;
        }
        
        /// <summary>
        /// Enumを指定しない場合、-1を返す
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int MaxIndex<T>()
        {
            if (typeof(T).IsEnum)
            {
                return Enum.GetValues(typeof(T)).Length-1;
            }

            return -1;
        }
        
        

    }
}