/*******
*
*     Title:数据管理工具类
*     Description:
*     Date:
*     Version:
*     Modify Recoder:
*/

using libx;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace BaseMVCFrame
{
	public static class DataGodTool
	{
        /// <summary>
        /// 加载数据表
        /// </summary>
        /// <param name="tableType"></param>
        /// <returns></returns>
        public static object LoadTable(DataTableType tableType)
        {
            object obj = null;
            using (var stream = new FileStream(GetDataConfigPath("DataGod"), FileMode.Open))
            {
                //MemoryStream stream = GetDataFileStream("DataGod");
                if (stream.IsNotNull())
                {
                    stream.Position = 0;
                    var reader = new tabtoy.TableReader(stream);
                    var tab = new DataGod.Table();
                    obj = GetDataTable(tableType, tab, reader);
                }
            }
            return obj;
        }

        /// <summary>
        /// 获取反序列化后的数据表数据
        /// </summary>
        /// <param name="tableType"></param>
        /// <param name="tab"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static object GetDataTable(DataTableType tableType,DataGod.Table tab, tabtoy.TableReader reader)
        {
            object obj = null;
            try
            {
                tab.Deserialize(reader);
                switch (tableType)
                {
                    case DataTableType.AssetPathConfig_DataTable:
                        {
                            obj = tab.AssetPathConfig;
                        }
                        break;
                    case DataTableType.RaceConfig_DataTable:
                        {
                            obj = tab.RaceConfig;
                        }
                        break;
                    case DataTableType.BloodlinesConfig_DataTable:
                        {
                            obj = tab.BloodlinesConfig;
                        }
                        break;
                    case DataTableType.EquipmentsConfig_DataTable:
                        {
                            obj = tab.EquipmentsConfig;
                        }
                        break;

                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
            return obj;
        }

        private static string GetDataConfigPath(string fileName)
        {
            string Path = "Assets/PackRes/DataFile/";
            return Path + fileName + ".bin";
        }
    }
}
