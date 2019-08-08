using System.Collections.Generic;

namespace QuickPay.Middleware
{
    /// <summary>Http请求构建
    /// </summary>
    public class HttpBuilder
    {
        /// <summary>Resource资源
        /// </summary>
        public string Resource { get; set; }

        /// <summary>方法
        /// </summary>
        public Method Method { get; set; }

        /// <summary>数据格式
        /// </summary>
        public DataFormat DataFormat { get; set; }

        /// <summary>参数
        /// </summary>
        public List<Parameter> Parameters { get; set; } = new List<Parameter>();

        /// <summary>Ctor
        /// </summary>
        public HttpBuilder()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public HttpBuilder(Method method)
        {
            Method = Method;
        }

        /// <summary>Ctor
        /// </summary>
        public HttpBuilder(string resource, Method method, DataFormat dataFormat)
        {
            Resource = resource;
            Method = method;
            DataFormat = dataFormat;
        }

        /// <summary>添加参数
        /// </summary>
        public HttpBuilder AddParameter(Parameter p)
        {
            Parameters.Add(p);
            return this;
        }

        /// <summary>添加参数
        /// </summary>
        public HttpBuilder AddParameter(string name, object value) => AddParameter(new Parameter(name, value, ParameterType.GetOrPost));

        /// <summary>添加参数
        /// </summary>
        public HttpBuilder AddParameter(string name, object value, ParameterType type)
         => AddParameter(new Parameter(name, value, type));
    }
}
