using System;
using UnityEngine;

namespace ZJD
{
    public static class TransformExtension
    {
        /// <summary>
        /// 获取T类型实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="ts">根节点</param>
        /// <param name="path">路径</param>
        /// <param name="t">T类型实例</param>
        public static void GetInstance<T>(this Transform ts, string path, out T t)
        {
            try
            {
                t = ts.Find(path).GetComponent<T>();
            }
            catch (Exception)
            {
                t = default(T);
            }
        }

        /// <summary>
        /// 获取T类型实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="ts">根节点</param>
        /// <param name="path">路径</param>
        public static T GetInstance<T>(this Transform ts , string path)
        {
            try
            {
                T t = ts.Find(path).GetComponent<T>();
                return t == null ? default(T) : t;
            }
            catch (Exception)
            {
                return  default(T);
            }
        }

        public static void BackAt(this Transform ts, Vector3 target)
        {
            Vector3 p1 = target;
            Vector3 p2 = ts.position;
            Vector3 p3 = new Vector3(2 * p2.x - p1.x, 2 * p2.y - p1.y, 2 * p2.z - p1.z);

            ts.LookAt(p3);
        }
    }
}
