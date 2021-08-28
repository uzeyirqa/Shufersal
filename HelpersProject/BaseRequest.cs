using RestSharp;
using System.Collections.Generic;
using System.Net;
using AngleSharp.Common;

namespace HelpersProject
{
    //Class to Stub REST API requests
    public  class BaseRequest
    {
        private readonly RestClient _client;
        private readonly RestRequest _request;

        public BaseRequest(string controllerUrl, string methodUrl, Method reqMethod, CookieContainer cookies = null)
        {
            _client = new RestClient(controllerUrl + methodUrl) { CookieContainer = cookies, Timeout = 300000 };
            _request = new RestRequest(reqMethod);
        }
        

        public IRestResponse SendRestRequest(object parameters = null, Dictionary<string, string> headers = null)
        {
            headers ??= new Dictionary<string, string> { { "content-type", "application/json" } };

            foreach (var i in headers)
                _request.AddHeader(i.Key, i.Value);

            //Add parameters to body.
            if (headers.ContainsValue("application/json") && _request.Method != Method.GET)
            {
                _request.AddJsonBody(parameters);
                _request.RequestFormat = DataFormat.Json;
            }
            else if (parameters != null)
                foreach (var param in parameters.ToDictionary())
                    if (param.Value != null)
                        _request.AddParameter(param.Key, param.Value.ToString());

            var response = _client.Execute(_request);
            //response.AnalyzeResponse();

            return response;
        }
    }
}