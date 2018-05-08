using DotCommon.Reflecting;
using QuickPay.Infrastructure.RequestData;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Responses;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace QuickPay.Infrastructure.Util
{
    public class RequestReflectUtil
    {
        private static readonly ConcurrentDictionary<Type, Delegate> RequestToDataDict =
            new ConcurrentDictionary<Type, Delegate>();

        private static readonly ConcurrentDictionary<Type, Delegate> DataToResponseDict =
            new ConcurrentDictionary<Type, Delegate>();

        private static readonly IDictionary<Type, object> DefaultValueDict = new ConcurrentDictionary<Type, object>()
        {
            [typeof(short)] = default(short),
            [typeof(int)] = default(int),
            [typeof(decimal)] = default(decimal),
            [typeof(DateTime)] = default(DateTime),
            [typeof(long)] = default(long),
            [typeof(double)] = default(double),
            [typeof(string)] = default(string)
        };

        private static readonly IDictionary<Type, string> ConvertMethodNames = new ConcurrentDictionary<Type, string>()
        {
            [typeof(short)] = "ToInt16",
            [typeof(int)] = "ToInt32",
            [typeof(long)] = "ToInt64",
            [typeof(double)] = "ToDouble",
            [typeof(decimal)] = "ToDecimal",
            [typeof(DateTime)] = "ToDateTime",
            [typeof(string)] = "ToString",
            [typeof(byte)] = "ToByte",
            [typeof(bool)] = "ToBoolean"
        };

        private static string GetConvertMethod(Type type)
        {
            return ConvertMethodNames[type];
        }

        /// <summary>生成将Request转换成PayData的委托
        /// </summary>
        private static Delegate GetDataFunc(Type sourceType)
        {
            //数据源类型,IQuickPayRequest实现类或子类
            // var sourceType = typeof(T);
            var targetType = typeof(PayData);
            // 定义参数为object类型
            var parameterExpr = Expression.Parameter(typeof(object), "o");
            var bodyExprs = new List<Expression>();
            //code:var payData=new PayData();
            var payDataExpr = Expression.Variable(targetType, "payData");
            var newPayDataExpr = Expression.New(targetType);
            var assignWxPayDataExpr = Expression.Assign(payDataExpr, newPayDataExpr);
            bodyExprs.Add(assignWxPayDataExpr);

            //code:var wxPay=(Request)o;
            var payExpr = Expression.Variable(sourceType, "pay");
            var castWxPayExpr = Expression.Convert(parameterExpr, sourceType);
            var assignWxPayExpr = Expression.Assign(payExpr, castWxPayExpr);
            bodyExprs.Add(assignWxPayExpr);

            //获取IWxPay中所有的属性
            var properties = PropertyInfoUtil.GetProperties(sourceType);
            foreach (var property in properties)
            {
                //获取该属性的Attribute
                var attribute = property.GetCustomAttribute<PayElementAttribute>();
                if (attribute != null)
                {
                    //code:var id=(object)request.Id;
                    //某属性的WxPayData中的Name值
                    var nameExpr = Expression.Constant(attribute.Name);
                    //(object)request.Id 默认转换为object
                    var valueExpr = Expression.Property(payExpr, property);
                    var castValueExpr = Expression.Convert(valueExpr, typeof(object));
                    //code:wxPayData.SetValue("xxx",(object)request.Id)
                    var setValueExpr = Expression.Call(payDataExpr,
                        targetType.GetTypeInfo().GetMethod("SetValue", new Type[] { typeof(string), typeof(object) }),
                        nameExpr, castValueExpr);
                    var ifNotNullExpr = Expression.IfThen(
                        //code: if(id!=null)
                        Expression.NotEqual(castValueExpr, Expression.Constant(null)),
                        //code: if(id!=0)
                        Expression.IfThen(
                            Expression.NotEqual(valueExpr,
                            Expression.Constant(DefaultValueDict[property.PropertyType])),
                            setValueExpr));
                    bodyExprs.Add(ifNotNullExpr);
                }
            }
            //此处需要注意,这里添加的相当于返回值
            bodyExprs.Add(payDataExpr);
            var methodBodyExpr = Expression.Block(targetType, new[] { payDataExpr, payExpr }, bodyExprs);
            // code: entity => { ... }
            var lambdaExpr = Expression.Lambda(methodBodyExpr, parameterExpr);
            var func = lambdaExpr.Compile();
            return func;
        }

        /// <summary>将PayData转换成Response的委托
        /// </summary>
        private static Delegate GetResponseFunc(Type targetType)
        {
            var sourceType = typeof(PayData);
            // 定义参数,传递进来的UserInfo
            var parameterExpr = Expression.Parameter(sourceType, "payData");
            var bodyExprs = new List<Expression>();
            //code:var response=new Response();
            var responseExpr = Expression.Variable(targetType, "response");
            var newResponseExpr = Expression.New(targetType);
            var assignWxPayExpr = Expression.Assign(responseExpr, newResponseExpr);
            bodyExprs.Add(assignWxPayExpr);
            //获取Response的全部属性
            var properties = PropertyInfoUtil.GetProperties(targetType);
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<PayElementAttribute>();
                if (attribute != null)
                {
                    var nameExpr = Expression.Constant(attribute.Name);
                    //code : payData.GetValue("name");
                    var getValueExpr = Expression.Call(parameterExpr,
                        sourceType.GetTypeInfo().GetMethod("GetValue", new[] { typeof(string) }), nameExpr);
                    //code: response.Name=wxPayData.GetValue("name");
                    var fieldExpr = Expression.Property(responseExpr, property);
                    var convertValueExpr = Expression.Call(null,
                        typeof(Convert).GetTypeInfo()
                            .GetMethod(GetConvertMethod(property.PropertyType), new[] { typeof(object) }), getValueExpr);
                    var assignFieldExpr = Expression.Assign(fieldExpr, convertValueExpr);
                    //code: if(wxPayData.GetValue("") != null){ ... }
                    var ifNotNullExpr = Expression.IfThen(
                        Expression.NotEqual(getValueExpr, Expression.Constant(null)),
                        assignFieldExpr);
                    bodyExprs.Add(ifNotNullExpr);
                }
            }
            //此处需要注意,这里添加的相当于返回值
            bodyExprs.Add(responseExpr);
            var methodBodyExpr = Expression.Block(targetType, new[] { responseExpr }, bodyExprs);
            // code: entity => { ... }
            var lambdaExpr = Expression.Lambda(methodBodyExpr, parameterExpr);
            var func = lambdaExpr.Compile();
            return func;
        }

        /// <summary>将Request请求转换为PayData
        /// </summary>
        public static PayData ToPayData(IPayRequest request)
        {
            var type = request.GetType();
            var method = RequestToDataDict.GetOrAdd(type, t =>
            {
                return GetDataFunc(type);
            });
            var toPayDataMethod = method as Func<object, PayData>;
            return toPayDataMethod(request);
        }

        /// <summary>将PayData转换成Response
        /// </summary>
        public static object ToResponse(PayData payData, Type responseType)
        {

            var method = DataToResponseDict.GetOrAdd(responseType, t =>
            {
                return GetResponseFunc(responseType);
            });
            var toReponseMethod = method as Func<PayData, object>;
            return toReponseMethod(payData);
        }



    }
}
