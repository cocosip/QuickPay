﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickPay.Infrastructure.RequestData
{
    /// <summary>PayData
    /// </summary>
    public class PayData
    {
        private readonly SortedDictionary<string, object> _values = new SortedDictionary<string, object>();
        /// <summary>Ctor
        /// </summary>
        public PayData()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public PayData(SortedDictionary<string, object> values)
        {

            foreach (var item in values)
            {
                if (!_values.ContainsKey(item.Key))
                {
                    _values.Add(item.Key, item.Value);
                }
            }
        }

        /// <summary>设置key和value
        /// </summary>
        public void SetValue(string key, object value)
        {
            _values[key] = value;
        }

        /// <summary>根据key获取value
        /// </summary>
        public object GetValue(string key)
        {
            object o;
            _values.TryGetValue(key, out o);
            return o;
        }

        /// <summary>根据Selector获取值
        /// </summary>
        public object GetValue(Func<KeyValuePair<string, object>, bool> selector)
        {
            return _values.Where(selector).FirstOrDefault().Value;
        }


        /// <summary>是否已经设置某个值
        /// </summary>
        public bool IsSet(string key)
        {
            object o;
            _values.TryGetValue(key, out o);
            return null != o;
        }

        /// <summary>是否有值
        /// </summary>
        public bool HasValue()
        {
            return _values.Any();
        }

        /// <summary>获取数据
        /// </summary>
        public SortedDictionary<string, object> GetValues()
        {
            return _values;
        }

        /// <summary>获取AppId,微信和支付宝通用
        /// </summary>
        public string GetAppId()
        {
            return _values.FirstOrDefault(x => string.Equals(x.Key, "appid", StringComparison.OrdinalIgnoreCase)).Value?.ToString() ?? "";
        }
    }
}
