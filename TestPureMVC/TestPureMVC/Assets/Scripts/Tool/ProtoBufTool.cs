/*******
*
*     Title:
*     Description:
*      ProtoBuf工具类
*     Date:
*     Version:
*     Modify Recoder:
*/

using BaseMVCFrame.Manager;
using libx;
using ProtoBuf;
using ProtoData;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace BaseMVCFrame
{
	public static class ProtoBufTool
	{
        ///// <summary>
        ///// 资源路径配置文件
        ///// </summary>
        //private const string ResourcePathConfig = "";

        /// <summary>
        /// 读取一个配置表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static T ReadOneDataConfig<T>(string FileName)
        {
            MemoryStream stream;
            stream = GetDataFileStream(FileName);
            if (null != stream)
            {
                T t = Serializer.Deserialize<T>(stream);
                stream.Close();
                return t;
            }
            return default(T);
        }
        private static MemoryStream GetDataFileStream(string fileName)
        {
            //获得配置表的路径
            //string filePath = GetDataConfigPath(fileName);
            //using (UnityWebRequest webRequest = UnityWebRequest.Get(filePath))
            //{
            //    //发送web请求
            //    webRequest.SendWebRequest();
            //    //等待下载数据完成
            //    while (!webRequest.isDone){}
            //    //将下载的数据转换成流
            //    byte[] array = Encoding.ASCII.GetBytes(webRequest.downloadHandler.text);
            //    MemoryStream stream = new MemoryStream(array);
            //    return stream;
            //}
            //TextAsset textAsset = ResourceMgr.Instance.LoadAsset<TextAsset>(fileName);

            AssetRequest assetRequest = Assets.LoadAsset(GetDataConfigPath(fileName),typeof(TextAsset));
            
            TextAsset textAsset = assetRequest.asset as TextAsset;
            byte[] array = Encoding.UTF8.GetBytes(textAsset.text);
            MemoryStream stream = new MemoryStream(array);
            return stream;
        }
        private static string GetDataConfigPath(string fileName)
        {
            string Path = "Assets/PackRes/ProtoData/";
            return Path + fileName+".txt";
        }


    }
}
