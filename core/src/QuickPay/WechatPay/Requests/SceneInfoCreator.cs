using QuickPay.WeChatPay.Apps;
using System.Collections.Generic;

namespace QuickPay.WeChatPay.Requests
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
        public static Dictionary<string, object> CreateScene(string sceneType, WeChatPayApp app)
        {
            switch (sceneType)
            {
                case WeChatPaySettings.H5SceneInfoType.IOS:
                    return CreateIosScene(app);
                case WeChatPaySettings.H5SceneInfoType.Android:
                    return CreateAndroidScene(app);
                case WeChatPaySettings.H5SceneInfoType.Wap:
                default:
                    return CreateWapScene(app);
            }
        }


        /// <summary>创建IOS场景
        /// </summary>
        public static Dictionary<string, object> CreateIosScene(WeChatPayApp app)
        {
            var dict = new Dictionary<string, object>();
            var v = new
            {
                type = WeChatPaySettings.H5SceneInfoType.IOS,
                app_name = app.NativeMobileInfo.IosName,
                bundle_id = app.NativeMobileInfo.BundleId
            };
            dict.Add(WeChatPaySettings.H5SceneInfoFieldName, v);
            return dict;
        }

        /// <summary>
        /// 创建安卓场景
        /// </summary>
        public static Dictionary<string, object> CreateAndroidScene(WeChatPayApp app)
        {
            var dict = new Dictionary<string, object>();
            var v = new
            {
                type = WeChatPaySettings.H5SceneInfoType.Android,
                app_name = app.NativeMobileInfo.AndroidName,
                package_name = app.NativeMobileInfo.PackageName
            };
            dict.Add(WeChatPaySettings.H5SceneInfoFieldName, v);
            return dict;
        }

        /// <summary>创建Wap场景
        /// </summary>
        public static Dictionary<string, object> CreateWapScene(WeChatPayApp app)
        {
            var dict = new Dictionary<string, object>();
            var v = new
            {
                type = WeChatPaySettings.H5SceneInfoType.Android,
                wap_url = app.NativeMobileInfo.WapUrl,
                wap_name = app.NativeMobileInfo.WapName
            };
            dict.Add(WeChatPaySettings.H5SceneInfoFieldName, v);
            return dict;
        }
    }
}
