using QuickPay.WechatPay.Apps;
using System.Collections.Generic;

namespace QuickPay.WechatPay.Requests
{
    public class SceneInfoCreator
    {

        public static Dictionary<string, object> CreateScene(string sceneType, WechatPayApp app)
        {
            switch (sceneType)
            {
                case WechatPaySettings.H5SceneInfoType.IOS:
                    return CreateIosScene(app);
                case WechatPaySettings.H5SceneInfoType.Android:
                    return CreateAndroidScene(app);
                case WechatPaySettings.H5SceneInfoType.Wap:
                default:
                    return CreateWapScene(app);
            }
        }


        /// <summary>
        /// 创建IOS场景
        /// </summary>
        /// <param name="appName">应用名</param>
        /// <param name="bundleId">bundle_id</param>
        /// <returns></returns>
        public static Dictionary<string, object> CreateIosScene(WechatPayApp app)
        {
            var dict = new Dictionary<string, object>();
            var v = new
            {
                type = WechatPaySettings.H5SceneInfoType.IOS,
                app_name = app.NativeMobileInfo.IosName,
                bundle_id = app.NativeMobileInfo.BundleId
            };
            dict.Add(WechatPaySettings.H5SceneInfoFieldName, v);
            return dict;
        }

        /// <summary>
        /// 创建安卓场景
        /// </summary>
        /// <param name="appName">应用名</param>
        /// <param name="packageName">包名</param>
        /// <returns></returns>
        public static Dictionary<string, object> CreateAndroidScene(WechatPayApp app)
        {
            var dict = new Dictionary<string, object>();
            var v = new
            {
                type = WechatPaySettings.H5SceneInfoType.Android,
                app_name = app.NativeMobileInfo.AndroidName,
                package_name = app.NativeMobileInfo.PackageName
            };
            dict.Add(WechatPaySettings.H5SceneInfoFieldName, v);
            return dict;
        }

        /// <summary>
        /// 创建Wap场景
        /// </summary>
        /// <param name="wapUrl">WAP网站URL地址</param>
        /// <param name="wapName">WAP 网站名</param>
        /// <returns></returns>
        public static Dictionary<string, object> CreateWapScene(WechatPayApp app)
        {
            var dict = new Dictionary<string, object>();
            var v = new
            {
                type = WechatPaySettings.H5SceneInfoType.Android,
                wap_url = app.NativeMobileInfo.WapUrl,
                wap_name = app.NativeMobileInfo.WapName
            };
            dict.Add(WechatPaySettings.H5SceneInfoFieldName, v);
            return dict;
        }
    }
}
