/**************************
 **序列化二进制文件的工具类 
 * 
 * 
 **************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//namespace BaseMVCFrame
    public static class BinaryTool
    {
        /// <summary>
        /// 序列化保存对象
        /// </summary>
        /// <typeparam name="T"> 可序列化的类名 </typeparam>
        /// <param name="dataClass"> 可序列化的对象 </param>
        /// <param name="path"> 保存的路径 </param>
        public static void SaveBinaryData<T>(T dataClass, string path)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, dataClass);
                stream.Close();
                stream.Dispose();
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            } // end try
        }

        /// <summary>
        /// 读取对象
        /// </summary>
        /// <typeparam name="T"> 类名 </typeparam>
        /// <param name="path"> 路径 </param>
        /// <returns> 读取到的对象 </returns>
        public static T ReadBinaryData<T>(string path)
        {
            T t = default(T);
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

                if (stream.Length == 0)
                {
                    return t;

                }
                else
                {
                    t = (T)formatter.Deserialize(stream);
                } // end if
                stream.Close();
                stream.Dispose();
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                t = default(T);
            } // end try
            return t;
        }

    }

