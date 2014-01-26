﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Test.CommonAPIs
{
    //已通过测试
    //[TestClass]
    public partial class CommonApiTest
    {
        private string AppId = "wxb65d5174ba1f015b";//换成你的信息
        private string AppSecret = "";//换成你的信息
        protected AccessTokenResult tokenResult = new AccessTokenResult()
                                                      {
                                                          /* 由于获取accessToken有次数限制，为了节约请求，
                                                           * 可以到 http://weixin.senparc.com/Menu 获取Token之后填入下方，
                                                           * 使用当前可用Token直接进行测试。
                                                           */
                                                          access_token = ""
                                                      };

        protected AccessTokenResult LoadToken()
        {
            if (tokenResult == null || string.IsNullOrEmpty(tokenResult.access_token))
            {
                //正确数据，请填写微信公众账号后台的AppId及AppSecret
                tokenResult = CommonApi.GetToken(AppId, AppSecret);
            }
            return tokenResult;
        }

        [TestMethod]
        public void GetTokenTest()
        {
            LoadToken();
            Assert.IsNotNull(tokenResult);
            Assert.IsTrue(tokenResult.access_token.Length > 0);
            Assert.IsTrue(tokenResult.expires_in > 0);
        }

        public void GetTokenFailTest()
        {
            try
            {
                var result = CommonApi.GetToken("appid", "secret");
                Assert.Fail();//上一步就应该已经抛出异常
            }
            catch (ErrorJsonResultException ex)
            {
                //实际返回的信息（错误信息）
                Assert.AreEqual(ex.JsonResult.errcode, ReturnCode.不合法的APPID);
            }
        }

        [TestMethod]
        public void GetUserInfoTest()
        {
            try
            {
                GetTokenTest();
                var result = CommonApi.GetUserInfo(tokenResult.access_token, "olPjZjsXuQPJoV0HlruZkNzKc91E");
                Assert.IsNotNull(result);
            }
            catch (Exception ex)
            {
                //如果不参加内测，只是“服务号”，这类接口仍然不能使用，会抛出异常：错误代码：45009：api freq out of limit
            }
        }

        [TestMethod]
        public void UploadMediaFileTest()
        {
            try
            {
                var file = "..\\..\\..\\..\\Senparc.Weixin.MP.Sample\\Senparc.Weixin.MP.Sample\\Images\\qrcode.jpg";

                var result = CommonApi.UploadMediaFile("token", UploadMediaFileType.image, file);
                Assert.Fail();//上一步就应该已经抛出异常
            }
            catch (ErrorJsonResultException ex)
            {
                //实际返回的信息（错误信息）
                Assert.AreEqual(ex.JsonResult.errcode, ReturnCode.验证失败);
            }
        }
    }
}
