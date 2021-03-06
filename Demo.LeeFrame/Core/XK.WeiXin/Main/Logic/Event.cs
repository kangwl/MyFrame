﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Demo.Common;

namespace XK.WeiXin.Main.Logic {
    public class Event : IMessageLogic {

        public Event(XmlDocument recieveXml) {
            XmlDoc = recieveXml;
            AddKeyWordFunc();
        }
        //1.todo: 用户未关注时，进行关注后的事件推送
        private string guanzhuXml = @"<xml><ToUserName><![CDATA[toUser]]></ToUserName>
                                        <FromUserName><![CDATA[FromUser]]></FromUserName>
                                        <CreateTime>123456789</CreateTime>
                                        <MsgType><![CDATA[event]]></MsgType>
                                        <Event><![CDATA[subscribe]]></Event>
                                        <EventKey><![CDATA[qrscene_123123]]></EventKey>
                                        <Ticket><![CDATA[TICKET]]></Ticket>
                                        </xml>";

        //ToUserName	开发者微信号
        //FromUserName	发送方帐号（一个OpenID）
        //CreateTime	消息创建时间 （整型）
        //MsgType	消息类型，event
        //Event	事件类型，subscribe
        //EventKey	事件KEY值，qrscene_为前缀，后面为二维码的参数值
        //Ticket	二维码的ticket，可用来换取二维码图片







        //todo:2. 用户已关注时的事件推送

        //推送XML数据包示例：

        //<xml>
        //<ToUserName><![CDATA[toUser]]></ToUserName>
        //<FromUserName><![CDATA[FromUser]]></FromUserName>
        //<CreateTime>123456789</CreateTime>
        //<MsgType><![CDATA[event]]></MsgType>
        //<Event><![CDATA[SCAN]]></Event>
        //<EventKey><![CDATA[SCENE_VALUE]]></EventKey>
        //<Ticket><![CDATA[TICKET]]></Ticket>
        //</xml>
        //参数说明：

        //参数	描述
        //ToUserName	开发者微信号
        //FromUserName	发送方帐号（一个OpenID）
        //CreateTime	消息创建时间 （整型）
        //MsgType	消息类型，event
        //Event	事件类型，SCAN
        //EventKey	事件KEY值，是一个32位无符号整数，即创建二维码时的二维码scene_id
        //Ticket	二维码的ticket，可用来换取二维码图片

        private string Content { get { return XmlHelper.GetXmlNodeTextByXpath(XmlDoc, "//Event"); } }



        public System.Xml.XmlDocument XmlDoc { get; set; }

        public string ResponseMessage() {
            ReturnText = Content;
            Func<string> keywordFunc = null;
            keywordFunc = keywordFuncs.FirstOrDefault(d => d.Key == Content.ToLower()).Value;
            if (keywordFunc == null) {
                Log log=new Log();
                log.WriteLog("Event:"+XmlDoc.InnerText); 
                return "";
            }
            return keywordFunc();

        }

        private string ReturnText { get; set; }

        private readonly Dictionary<string, Func<string>> keywordFuncs = new Dictionary<string, Func<string>>();

        private void AddKeyWordFunc() {
            keywordFuncs.Add("subscribe", CreateSendMsg);
            keywordFuncs.Add("scan", CreateSendMsg);
            clickFuncs.Add("bdw", Create_bdw_msg);
            clickFuncs.Add("dw", Create_dw_msg);
            keywordFuncs.Add("click",CreateClickMsg);
            //keywordFuncs.Add("bdw",Create_bdw_msg);
            //keywordFuncs.Add("dw", Create_dw_msg);
        }

        private readonly Dictionary<string, Func<string>> clickFuncs = new Dictionary<string, Func<string>>();
        private string CreateClickMsg()
        {
            string eventKey = XmlHelper.GetXmlNodeTextByXpath(XmlDoc, "//EventKey");
            var clickFunc = clickFuncs.FirstOrDefault(d => d.Key == eventKey.ToLower()).Value;
            return clickFunc(); 
        }

        private string Create_bdw_msg()
        {
            string bdwMsg = "你说你傻不傻";
            string msg = string.Format(Text.sendXml, FromUserName, ToUserName,
                TimeConvert.GetDateTimeStamp(DateTime.Now), bdwMsg);

            return msg;
        }
        private string Create_dw_msg()
        {
            string bdwMsg = "你还真点我啊";
            string msg = string.Format(Text.sendXml, FromUserName, ToUserName,
                TimeConvert.GetDateTimeStamp(DateTime.Now), bdwMsg);

            return msg;
        }
        private string ToUserName { get { return XmlHelper.GetXmlNodeTextByXpath(XmlDoc, "//ToUserName"); } }
        private string FromUserName { get { return XmlHelper.GetXmlNodeTextByXpath(XmlDoc, "//FromUserName"); } }

        public string CreateSendMsg() {
            StringBuilder sb = new StringBuilder();
            sb.Append("欢迎关注大侠我\n");
            sb.Append("love ❤");


           // string ToUserName = XmlHelper.GetXmlNodeTextByXpath(XmlDoc, "//ToUserName");
           // string FromUserName = XmlHelper.GetXmlNodeTextByXpath(XmlDoc, "//FromUserName");

            string msg = string.Format(Text.sendXml, FromUserName, ToUserName,
                TimeConvert.GetDateTimeStamp(DateTime.Now), sb.ToString());

            return msg;
        }
    }
}
