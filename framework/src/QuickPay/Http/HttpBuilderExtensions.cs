﻿//using DotCommon.Extensions;
//using DotCommon.Http;

//namespace QuickPay.Http
//{
//    /// <summary>Http请求辅助类
//    /// </summary>
//    public static class HttpBuilderExtensions
//    {
//        /// <summary>根据HttpBuilder构建RequestRequest请求
//        /// </summary>
//        public static RestSharp.IRestRequest BuildRequest(this HttpBuilder builder)
//        {
//            RestSharp.IRestRequest request = new RestSharp.RestRequest
//            {
//                Method = ParseMethod(builder.Method),
//                Resource = builder.Resource.IsNullOrWhiteSpace() ? "" : builder.Resource
//            };

//            foreach (var parameter in builder.Parameters)
//            {
//                var p = new RestSharp.Parameter()
//                {
//                    Name = parameter.Name,
//                    Value = parameter.Value,
//                    DataFormat = parameter.DataFormat.ParseDataFormat(),
//                    ContentType = parameter.ContentType,
//                    Type = parameter.Type.ParseParameterType()
//                };
//                request.AddParameter(p);
//            }

//            foreach (var file in builder.Files)
//            {
//                var f = new RestSharp.FileParameter()
//                {
//                    Name = file.Name,
//                    FileName = file.FileName,
//                    ContentLength = file.ContentLength,
//                    ContentType = file.ContentType,
//                    Writer = file.Writer
//                };
//                request.Files.Add(f);
//            }

//            return request;
//        }


//        private static RestSharp.Method ParseMethod(this Method method)
//        {
//            switch (method)
//            {
//                case Method.GET:
//                default:
//                    return RestSharp.Method.GET;
//                case Method.POST:
//                    return RestSharp.Method.POST;
//                case Method.PUT:
//                    return RestSharp.Method.PUT;
//                case Method.DELETE:
//                    return RestSharp.Method.DELETE;
//                case Method.HEAD:
//                    return RestSharp.Method.HEAD;
//                case Method.OPTIONS:
//                    return RestSharp.Method.OPTIONS;
//                case Method.PATCH:
//                    return RestSharp.Method.PATCH;
//                case Method.MERGE:
//                    return RestSharp.Method.MERGE;
//                case Method.COPY:
//                    return RestSharp.Method.COPY;
//            }
//        }


//        private static RestSharp.DataFormat ParseDataFormat(this DataFormat dataFormat)
//        {
//            switch (dataFormat)
//            {
//                case DataFormat.Json:
//                    return RestSharp.DataFormat.Json;
//                case DataFormat.Xml:
//                    return RestSharp.DataFormat.Xml;
//                case DataFormat.None:
//                default:
//                    return RestSharp.DataFormat.None;
//            }
//        }

//        private static RestSharp.ParameterType ParseParameterType(this ParameterType parameterType)
//        {
//            switch (parameterType)
//            {
//                case ParameterType.GetOrPost:
//                default:
//                    return RestSharp.ParameterType.GetOrPost;
//                case ParameterType.Cookie:
//                    return RestSharp.ParameterType.Cookie;
//                case ParameterType.UrlSegment:
//                    return RestSharp.ParameterType.UrlSegment;
//                case ParameterType.HttpHeader:
//                    return RestSharp.ParameterType.HttpHeader;
//                case ParameterType.RequestBody:
//                    return RestSharp.ParameterType.RequestBody;
//                case ParameterType.QueryString:
//                    return RestSharp.ParameterType.QueryString;
//                case ParameterType.QueryStringWithoutEncode:
//                    return RestSharp.ParameterType.QueryStringWithoutEncode;
//            }
//        }
//    }
//}
