using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSharp7._0CoreGuide.App
{
    public class HttpHelper
    {

        readonly static HttpClient _httpClient;
        #region 初始化构造函数
        static HttpHelper()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5000/");
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMzMiLCJleHAiOjE1OTUwNTU2MDEsImlzcyI6ImlnYm9tX3dlYiIsImF1ZCI6ImlnYm9tX3dlYiJ9.E9IfW4IUt1UvBhT0hObxiMlg3N_FeHDi-DChPBZPd8g");

        }
        #endregion
        
        public static string HttpGet(string api,Dictionary<string,string> paramter)
        {
            //发送get请求
            HttpResponseMessage response = _httpClient.GetAsync(api + "?" + GetDictionaryString(paramter)).Result;
            return  response.Content.ReadAsStringAsync().Result;
        }
        /// <summary>
        /// HttpPost application/json
        /// </summary>
        /// <param name="api">例子：接口地址 api/test/PostJson</param>
        /// <param name="json">参数json字符串</param>
        /// <returns></returns>
        public static string HttpPostJson(string api, string json)
        {
            var requestContent = new StringContent(json,Encoding.UTF8,"application/json");
            HttpResponseMessage response = _httpClient.PostAsync(api,requestContent).Result;
            return response.Content.ReadAsStringAsync().Result;
        }
        /// <summary>
        /// HttpPost application/x-www-form-urlencoded
        /// </summary>
        /// <param name="api">例子：接口地址 api/test/PostForm</param>
        /// <param name="paramter">参数</param>
        /// <returns></returns>
        public static string HttpPostForm(string api, Dictionary<string ,string> paramter)
        {
            var requestContent = new FormUrlEncodedContent(paramter);
            HttpResponseMessage response = _httpClient.PostAsync(api, requestContent).Result;
            return response.Content.ReadAsStringAsync().Result;
        }
        /// <summary>
        /// 单文件上传
        /// </summary>
        /// <param name="api"></param>
        /// <param name="upLoadFile"></param>
        /// <returns></returns>
        public static string HttpPostFile(string api, UpLoadFile upLoadFile)
        {
            var requestContent = new MultipartFormDataContent();
            StreamContent streamContent = new StreamContent(upLoadFile.Stream);
            requestContent.Add(streamContent, upLoadFile.Name);
            HttpResponseMessage response = _httpClient.PostAsync(api, requestContent).Result;
            return response.Content.ReadAsStringAsync().Result;
        }
        /// <summary>
        /// 多文件上传
        /// </summary>
        /// <param name="api"></param>
        /// <param name="upLoadFiles"></param>
        /// <returns></returns>
        public static string HttpPostFiles(string api, List<UpLoadFile> upLoadFiles)
        {
            var requestContent = new MultipartFormDataContent();
            for (int i = 0; i < upLoadFiles.Count; i++)
            {
                var upload = upLoadFiles[i];
                StreamContent streamContent = new StreamContent(upload.Stream);
                requestContent.Add(streamContent, upload.Name );//, upload.FileName
            }
            HttpResponseMessage response = _httpClient.PostAsync(api, requestContent).Result;
            return response.Content.ReadAsStringAsync().Result;
        }
       /// <summary>
       /// 表单+文件同时上传，成功了一般
       /// </summary>
       /// <param name="api"></param>
       /// <param name="paramter"></param>
       /// <param name="upLoadFile"></param>
       /// <returns></returns>
        public static string HttpPostMultipartFormData(string api, Dictionary<string, string> paramter,UpLoadFile upLoadFile)
        {
            var requestContent = new MultipartFormDataContent();
            foreach (var item in paramter)
            {
                requestContent.Add(new StringContent(item.Value), item.Key);
            }
            //追加文件
            StreamContent streamContent = new StreamContent(upLoadFile.Stream);
            requestContent.Add(streamContent, upLoadFile.Name);
            HttpResponseMessage response = _httpClient.PostAsync(api, requestContent).Result;
            return response.Content.ReadAsStringAsync().Result;
        }
        static string GetDictionaryString(Dictionary<string, string> paramter)
        {
            List<string> queryString = new List<string>();
            foreach (var item in paramter)
            {
                queryString.Add($"{item.Key}={item.Value}");
            }
            return string.Join("&", queryString);
        }
        public class Mulitp_Form_Data
        { 
        
        }
        public class UpLoadFile
        {
            /// <summary>
            /// 文件流
            /// </summary>
            public Stream Stream { get; set; }
            /// <summary>
            /// 字段名称 多个文件需要加   [文件索引]
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 替换文件名
            /// </summary>
            //public string FileName { get; set; }
        }
    }

}
