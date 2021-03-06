﻿namespace XK.WeiXin.Author {
    public sealed class AppConfig {

        private AppConfig() { }

        private static readonly AppConfig _instance = new AppConfig();

        public static AppConfig  Instance{
            get { return _instance; }
        }

        private string _appid = "wx20841386e3610a3a";//wx264b6112e484829b
        private string _appSecret = "c06314a5a2c228a4489c87ddfa56e098";//7983acbc9189c4f7dc633e49c7480e8b
        private string _token = "kangwl";
        /// <summary>
        /// AppID(应用ID)
        /// </summary>
        public string AppID
        {
            get { return _appid; }
            set { _appid = value; }
        }

        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret
        {
            get { return _appSecret; }
            set { _appSecret = value; }
        }

        /// <summary>
        /// Token(令牌)
        /// </summary>
        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }

    }
}
