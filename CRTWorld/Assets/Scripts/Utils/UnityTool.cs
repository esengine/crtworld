using System;
using System.Collections.Generic;
using UnityEngine;

public static class UnityTool
{
    public static Transform Find(string path)
    {
        return Find(null, path);
    }

    public static Transform Find(Transform root, string path, Transform defaultTrans)
    {
        Transform tr = Find(root, path);
        return tr != null ? tr : defaultTrans;
    }

    public static Transform Find(Transform root, string path, bool absolute = false)
    {
        if (path.IndexOf("/") == 0)
            path = path.Substring(1);
        //根为空则默认把第一个节点当作根
        if (root == null)
        {
            int index = path.IndexOf("/");
            if (index < 0)
            {
                //Util.LogError("不支持非路径全局查找，请指定根节点或全路径");
                GameObject obj = GameObject.Find(path);
                if (obj == null)
                    return null;

                return obj.transform;
            }
            string rootPath = path.Substring(0, index);
            GameObject rootObj = GameObject.Find(rootPath);
            if (rootObj == null)
                return null;
            else
            {
                root = rootObj.transform;
                path = path.Substring(rootPath.Length);
            }

        }
        string[] arr = SplitString(path, '/');
        return _Find(root, arr, 0, absolute);

    }

    internal static Transform _Find(Transform trans, int siblingIndex, string[] path, int index)
    {
        Transform child = trans.Find(path[index]);
        if (child != null)
        {
            index++;
            if (index >= path.Length)
            {
                return child;
            }
            Transform tmp = _Find(child, siblingIndex, path, index);
            if (tmp != null && tmp.GetSiblingIndex() == siblingIndex)
                return tmp;
        }
        else
        {
            child = FindByName(trans, path[index]);
            if (child != null)
            {
                index++;
                if (index >= path.Length && child.GetSiblingIndex() == siblingIndex)
                    return child;
                else
                {
                    return _Find(child, path, index);
                }
            }
        }

        return null;
    }

    internal static Transform _Find(Transform trans, string[] path, int index, bool absolute = false)
    {
        Transform child = trans.Find(path[index]);
        if (child != null)
        {
            index++;
            if (index >= path.Length)
            {
                return child;
            }
            Transform tmp = _Find(child, path, index, absolute);
            if (tmp != null)
                return tmp;
        }
        else
        {
            if (absolute) return null;
            int findCount = 0;
            child = FindByName(trans, path[index], ref findCount);

            if (child != null)
            {
                index++;
                if (index >= path.Length)
                    return child;
                else
                {
                    return _Find(child, path, index, absolute);
                }
            }
        }

        return null;
    }

    public static Transform FindByName(Transform root, string name, ref int findCount)
    {
        Transform child = root.Find(name);
        if (child)
            return child;

        int count = root.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform childRoot = root.GetChild(i);
            findCount++;
            Transform c = FindByName(childRoot, name, ref findCount);
            if (c != null)
                return c;
        }

        return null;
    }

    public static Transform FindByName(Transform root, string name)
    {
        Transform child = root.Find(name);
        if (child)
            return child;

        int count = root.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform childRoot = root.GetChild(i);
            Transform c = FindByName(childRoot, name);
            if (c != null)
                return c;
        }

        return null;
    }

    public static string[] SplitString(string pszString, char chSep)//低GC分割字符，量大时用
    {
        if (pszString == null)
        {
            return null;
        }
        List<int> segIndex = getIndexesFromString(pszString, chSep);
        if (segIndex.Count <= 0) return new string[1] { pszString };
        string[] strSeg = new string[segIndex.Count + 1];
        var span = pszString.AsSpan();
        int start = 0;
        for (int i = 0; i < segIndex.Count; i++)
        {
            strSeg[i] = span.Slice(start, segIndex[i] - start).ToString();
            start = segIndex[i] + 1;
            if (i == segIndex.Count - 1)
                strSeg[segIndex.Count] = span.Slice(start, pszString.Length - start).ToString();
        }

        return strSeg;
    }

    private static List<int> getIndexesFromString(string content, char sep)
    {
        //_vecIndexes.Clear();
        var _vecIndexes = new List<int>();
        int index = -1;
        do
        {
            //分隔符只可能是一个字符,所以是1
            index = content.IndexOf(sep, index + 1);
            if (index != -1)
            {
                _vecIndexes.Add(index);
            }

        } while (index != -1);
        return _vecIndexes;
    }
}