


namespace BaseMVCFrame
{

    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;
    using Object = UnityEngine.Object;

    /// <summary>
    /// 常用类通用扩展方法
    /// </summary>
    public static class Extension
    {
        public static bool IsNull<T>(this T selfObj) where T : class
        {
            return null == selfObj;
        }

        public static bool IsNotNull<T>(this T selfObj) where T : class
        {
            return null != selfObj;
        }
    }

    /// <summary>
    /// 数组扩展
    /// </summary>
    public static class ArrayExtension
    {
        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <returns>The each.</returns>
        /// <param name="selfArray">Self array.</param>
        /// <param name="action">Action.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T[] ForEach<T>(this T[] selfArray, Action<T> action)
        {
            Array.ForEach(selfArray, action);
            return selfArray;
        }

        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <returns>The each.</returns>
        /// <param name="selfArray">Self array.</param>
        /// <param name="action">Action.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> selfArray, Action<T> action)
        {
            if (action == null) throw new ArgumentException();
            foreach (var item in selfArray)
            {
                action(item);
            }

            return selfArray;
        }
    }

    /// <summary>
    /// List扩展方法
    /// </summary>
    public static class ListExtension
    {
        /// <summary>
        /// 遍历列表
        /// </summary>
        /// <typeparam name="T">列表类型</typeparam>
        /// <param name="list">目标表</param>
        /// <param name="action">行为</param>
        public static void ForEach<T>(this List<T> list, Action<int, T> action)
        {
            for (var i = 0; i < list.Count; i++)
            {
                action(i, list[i]);
            }
        }

        /// <summary>
        /// 拷贝到
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        public static void CopyTo<T>(this List<T> from, List<T> to, int begin = 0, int end = -1)
        {
            if (begin < 0)
            {
                begin = 0;
            }

            var endIndex = Math.Min(from.Count, to.Count) - 1;

            if (end != -1 && end < endIndex)
            {
                endIndex = end;
            }

            for (var i = begin; i < end; i++)
            {
                to[i] = from[i];
            }
        }

        /// <summary>
        /// 将List转为Array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfList"></param>
        /// <returns></returns>
        public static T[] ToArraySavely<T>(this List<T> selfList)
        {
            var res = new T[selfList.Count];

            for (var i = 0; i < selfList.Count; i++)
            {
                res[i] = selfList[i];
            }

            return res;
        }

        /// <summary>
        /// 尝试获取，如果没有该数则返回null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfList"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T TryGet<T>(this List<T> selfList, int index)
        {
            return selfList.Count > index ? selfList[index] : default(T);
        }
    }

    /// <summary>
    /// Dictionary扩展方法
    /// </summary>
    public static class DictionaryExtension
    {
        /// <summary>
        /// 合并两个字典
        /// Merge the specified dictionary and dictionaries.
        /// </summary>
        /// <returns>The merge.</returns>
        /// <param name="dictionary">Dictionary.</param>
        /// <param name="dictionaries">Dictionaries.</param>
        /// <typeparam name="TKey">The 1st type parameter.</typeparam>
        /// <typeparam name="TValue">The 2nd type parameter.</typeparam>
        public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this Dictionary<TKey, TValue> dictionary,
            params Dictionary<TKey, TValue>[] dictionaries)
        {
            return dictionaries.Aggregate(dictionary,
                (current, dict) => current.Union(dict).ToDictionary(kv => kv.Key, kv => kv.Value));
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="dict"></param>
        /// <param name="action"></param>
        public static void ForEach<K, V>(this Dictionary<K, V> dict, Action<K, V> action)
        {
            var dictE = dict.GetEnumerator();

            while (dictE.MoveNext())
            {
                var current = dictE.Current;
                action(current.Key, current.Value);
            }

            dictE.Dispose();
        }
    }
    /// <summary>
    /// 字节数组扩展
    /// </summary>
    public static class ByteArrayExtention
    {
        /// <summary>
        /// 获得UTF-8格式的string
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string GetString_UTF8(this byte[] self)
        {
            return System.Text.Encoding.UTF8.GetString(self);
        }
    }

    /// <summary>
    /// String扩展方法
    /// </summary>
    public static class StringExtention
    {
        /// <summary>
        /// Check Whether string is null or empty
        /// </summary>
        /// <param name="selfStr"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string selfStr)
        {
            return string.IsNullOrEmpty(selfStr);
        }

        /// <summary>
        /// Check Whether string is null or empty
        /// </summary>
        /// <param name="selfStr"></param>
        /// <returns></returns>
        public static bool IsNotNullAndEmpty(this string selfStr)
        {
            return !string.IsNullOrEmpty(selfStr);
        }

        /// <summary>
        /// Check Whether string trim is null or empty
        /// </summary>
        /// <param name="selfStr"></param>
        /// <returns></returns>
        public static bool IsTrimNotNullAndEmpty(this string selfStr)
        {
            return !string.IsNullOrEmpty(selfStr.Trim());
        }

        /// <summary>
        /// 避免每次都用.
        /// </summary>
        private static readonly char[] mCachedSplitCharArray = { '.' };

        public static string[] Split(this string selfStr, char splitSymbol)
        {
            mCachedSplitCharArray[0] = splitSymbol;
            return selfStr.Split(mCachedSplitCharArray);
        }

        public static string UppercaseFirst(this string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        public static string LowercaseFirst(this string str)
        {
            return char.ToLower(str[0]) + str.Substring(1);
        }

        public static string ToUnixLineEndings(this string str)
        {
            return str.Replace("\r\n", "\n").Replace("\r", "\n");
        }

        public static string ToCSV(this string[] values)
        {
            return string.Join(", ", values
                .Where(value => !string.IsNullOrEmpty(value))
                .Select(value => value.Trim())
                .ToArray()
            );
        }

        public static string[] ArrayFromCSV(this string values)
        {
            return values
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(value => value.Trim())
                .ToArray();
        }

        public static string ToSpacedCamelCase(this string text)
        {
            var sb = new StringBuilder(text.Length * 2);
            sb.Append(char.ToUpper(text[0]));
            for (var i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                {
                    sb.Append(' ');
                }

                sb.Append(text[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 有点不安全,编译器不会帮你排查错误。
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FillFormat(this string selfStr, params object[] args)
        {
            return string.Format(selfStr, args);
        }

        public static StringBuilder Append(this string selfStr, string toAppend)
        {
            return new StringBuilder(selfStr).Append(toAppend);
        }

        public static string AddPrefix(this string selfStr, string toPrefix)
        {
            return new StringBuilder(toPrefix).Append(selfStr).ToString();
        }

        public static StringBuilder AppendFormat(this string selfStr, string toAppend, params object[] args)
        {
            return new StringBuilder(selfStr).AppendFormat(toAppend, args);
        }

        public static string LastWord(this string selfUrl)
        {
            return selfUrl.Split('/').Last();
        }

        public static int ToInt(this string selfStr, int defaulValue = 0)
        {
            var retValue = defaulValue;
            return int.TryParse(selfStr, out retValue) ? retValue : defaulValue;
        }

        public static DateTime ToDateTime(this string selfStr, DateTime defaultValue = default(DateTime))
        {
            var retValue = defaultValue;
            return DateTime.TryParse(selfStr, out retValue) ? retValue : defaultValue;
        }


        public static float ToFloat(this string selfStr, float defaulValue = 0)
        {
            var retValue = defaulValue;
            return float.TryParse(selfStr, out retValue) ? retValue : defaulValue;
        }

        /// <summary>
        /// 是否存在中文字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool HasChinese(this string input)
        {
            return Regex.IsMatch(input, @"[\u4e00-\u9fa5]");
        }

        /// <summary>
        /// 是否存在空格
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool HasSpace(this string input)
        {
            return input.Contains(" ");
        }

        /// <summary>
        /// 删除特定字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string RemoveString(this string str, params string[] targets)
        {
            return targets.Aggregate(str, (current, t) => current.Replace(t, string.Empty));
        }
    }

    /// <summary>
    /// 脚本扩展方法
    /// </summary>
    public static class BehaviourExtension
    {
        public static T Enable<T>(this T selfBehaviour) where T : Behaviour
        {
            selfBehaviour.enabled = true;
            return selfBehaviour;
        }

        public static T Disable<T>(this T selfBehaviour) where T : Behaviour
        {
            selfBehaviour.enabled = false;
            return selfBehaviour;
        }
    }

    /// <summary>
    /// GameObject's Util/Static This Extension
    /// </summary>
    public static class GameObjectExtension
    {

        #region CEGO001 Show

        public static GameObject Show(this GameObject selfObj)
        {
            selfObj.SetActive(true);
            return selfObj;
        }

        public static T Show<T>(this T selfComponent) where T : Component
        {
            if (!selfComponent.gameObject.activeInHierarchy)
            {
                selfComponent.gameObject.Show();
            }
            return selfComponent;
        }

        #endregion

        #region CEGO002 Hide

        public static GameObject Hide(this GameObject selfObj)
        {
            selfObj.SetActive(false);
            return selfObj;
        }

        public static T Hide<T>(this T selfComponent) where T : Component
        {
            if (selfComponent.gameObject.activeInHierarchy)
            {
                selfComponent.gameObject.Hide();
            }
            return selfComponent;
        }
        #endregion

        #region CEGO006 Layer
        public static GameObject Layer(this GameObject selfObj, int layer)
        {
            selfObj.layer = layer;
            return selfObj;
        }

        public static T Layer<T>(this T selfComponent, int layer) where T : Component
        {
            selfComponent.gameObject.layer = layer;
            return selfComponent;
        }

        public static GameObject Layer(this GameObject selfObj, string layerName)
        {
            selfObj.layer = LayerMask.NameToLayer(layerName);
            return selfObj;
        }

        public static T Layer<T>(this T selfComponent, string layerName) where T : Component
        {
            selfComponent.gameObject.layer = LayerMask.NameToLayer(layerName);
            return selfComponent;
        }

        #endregion

        #region CEGO007 Component

        public static T GetOrAddComponent<T>(this GameObject selfComponent) where T : Component
        {
            var comp = selfComponent.gameObject.GetComponent<T>();
            return comp ? comp : selfComponent.gameObject.AddComponent<T>();
        }

        public static Component GetOrAddComponent(this GameObject selfComponent, Type type)
        {
            var comp = selfComponent.gameObject.GetComponent(type);
            return comp ? comp : selfComponent.gameObject.AddComponent(type);
        }

        #endregion

        /// <summary>
        /// 查找子节点
        /// </summary>
        /// <param name="Father"></param>
        /// <param name="ChildName"></param>
        /// <returns></returns>
        public static Transform GetChild(this Transform Father, string ChildName)
        {
            Transform searchTrans = Father.Find(ChildName);
            if (searchTrans == null)
            {
                foreach (Transform trans in Father)
                {
                    searchTrans = GetChild(trans, ChildName);
                    if (searchTrans != null)
                    {
                        return searchTrans;
                    }
                }
            }
            return searchTrans;
        }
        /// <summary>
        /// 查找子节点并获取对应组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Father"></param>
        /// <param name="ChildName"></param>
        /// <returns></returns>
        public static T GetChild<T>(this Transform Father,string ChildName)
        {
            Transform searchTrans = Father.Find(ChildName);
            if (searchTrans == null)
            {
                foreach (Transform trans in Father)
                {
                    searchTrans = GetChild(trans, ChildName);
                    if (searchTrans != null)
                    {
                        return searchTrans.GetComponent<T>();
                    }
                }
            }
            return searchTrans.GetComponent<T>();
        }
    }

    /// <summary>
    /// Image扩展方法
    /// </summary>
    public static class ImageExtension
    {
        public static Image FillAmount(this Image selfImage, float fillamount)
        {
            selfImage.fillAmount = fillamount;
            return selfImage;
        }
    }

    /// <summary>
    /// Int扩展方法
    /// </summary>
    public static class IntExtension
    {
        /// <summary>
        /// 如果值为负数则值为0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int LessThanZeroToZero(this int value)
        {
            if (value<0)
            {
                value = 0;
            }
            return value;
        }
    }
    /// <summary>
    /// float扩展方法
    /// </summary>
    public static class FloatExtension
    {
        /// <summary>
        /// 如果值为负数则值为0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float LessThanZeroToZero(float value)
        {
            if (value < 0)
            {
                value = 0;
            }
            return value;
        }
    }
}

