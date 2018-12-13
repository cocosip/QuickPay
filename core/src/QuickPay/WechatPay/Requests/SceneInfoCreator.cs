using QuickPay.WechatPay.Apps;
using System.Collections.Generic;

namespace QuickPay.WechatPay.Requests
{
    /// <summary>场景创建
    /// </summary>
    public class SceneInfoCreator
    {
        /// <summary>
        /// 创建场景
        /// </summary>
        /// <param name="sceneType">场景类型</param>
        /// <param name="app">微信应用App</param>
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


        /// <summary>创建IOS场景
        /// </summary>
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

        /// <summary>创建Wap场景
        /// </summary>
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
