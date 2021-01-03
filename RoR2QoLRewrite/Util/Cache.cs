﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLRewrite.Util
{
    public static class Cache<T>
    {
        private static readonly ConcurrentDictionary<string, T> _cache = new ConcurrentDictionary<string, T>();
        private static readonly object lockObj = new object();

        public static void Add(string key, T val)
        {
            lock(Cache<T>.lockObj)
            {
                Cache<T>._cache[key] = val;
            }
        }

        public static T Get(string key)
        {
            lock(Cache<T>.lockObj)
            {
                return Cache<T>._cache[key];
            }
        }

        public static T Dispose(string key)
        {
            lock (Cache<T>.lockObj)
            {
                Cache<T>._cache.TryRemove(key, out T val);
                return val;
            }            
        }
    }
}