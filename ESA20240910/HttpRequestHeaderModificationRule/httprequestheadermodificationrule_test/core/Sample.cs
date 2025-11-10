// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Darabonba.Utils;
using ESA20240910Client = AlibabaCloud.SDK.ESA20240910.Client;
using AlibabaCloud.OpenApiClient.Models;
using Aliyun.Credentials;
using AlibabaCloud.SDK.ESA20240910.Models;
using AlibabaCloud.TeaUtil;

namespace AlibabaCloud.CodeSample
{
    public class Sample 
    {

        public Sample()
        {
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>Init Client</para>
        /// </description>
        public static ESA20240910Client CreateESA20240910Client()
        {
            Config config = new Config();
            config.Credential = new Client(null);
            // Endpoint please refer to https://api.aliyun.com/product/ESA
            config.Endpoint = "esa.cn-hangzhou.aliyuncs.com";
            return new ESA20240910Client(config);
        }


        public static async Task<PurchaseRatePlanResponseBody> RatePlanInstAsync(ESA20240910Client client)
        {
            Console.WriteLine("Begin Call PurchaseRatePlan to create resource");
            PurchaseRatePlanRequest purchaseRatePlanRequest = new PurchaseRatePlanRequest
            {
                Type = "NS",
                ChargeType = "PREPAY",
                AutoRenew = false,
                Period = 1,
                Coverage = "overseas",
                AutoPay = true,
                PlanName = "high",
            };
            PurchaseRatePlanResponse purchaseRatePlanResponse = await client.PurchaseRatePlanAsync(purchaseRatePlanRequest);
            DescribeRatePlanInstanceStatusRequest describeRatePlanInstanceStatusRequest = new DescribeRatePlanInstanceStatusRequest
            {
                InstanceId = purchaseRatePlanResponse.Body.InstanceId,
            };
            int? currentRetry = 0;
            int? delayedTime = 10000;
            int? interval = 10000;

            while (currentRetry < 10) {
                try
                {
                    int? sleepTime = 0;
                    if (currentRetry == 0)
                    {
                        sleepTime = delayedTime;
                    }
                    else
                    {
                        sleepTime = interval;
                    }
                    Console.WriteLine("Polling for asynchronous results...");
                    await Task.Delay(sleepTime.Value);
                }
                catch (Darabonba.Exceptions.DaraException error)
                {
                    throw new Darabonba.Exceptions.DaraException(new Dictionary<string, string>
                    {
                        {"message", error.Message},
                    });
                }
                DescribeRatePlanInstanceStatusResponse describeRatePlanInstanceStatusResponse = await client.DescribeRatePlanInstanceStatusAsync(describeRatePlanInstanceStatusRequest);
                string instanceStatus = describeRatePlanInstanceStatusResponse.Body.InstanceStatus;
                if (instanceStatus == "running")
                {
                    Console.WriteLine("Call PurchaseRatePlan success, response: ");
                    Console.WriteLine(Common.ToJSONString(purchaseRatePlanResponse));
                    return purchaseRatePlanResponse.Body;
                }
                currentRetry++;
            }
            throw new Darabonba.Exceptions.DaraException(new Dictionary<string, string>
            {
                {"message", "Asynchronous check failed"},
            });
        }


        public static async Task<CreateSiteResponseBody> SiteAsync(PurchaseRatePlanResponseBody ratePlanInstResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("Begin Call CreateSite to create resource");
            CreateSiteRequest createSiteRequest = new CreateSiteRequest
            {
                SiteName = "gositecdn.cn",
                InstanceId = ratePlanInstResponseBody.InstanceId,
                Coverage = "overseas",
                AccessType = "NS",
            };
            CreateSiteResponse createSiteResponse = await client.CreateSiteAsync(createSiteRequest);
            GetSiteRequest getSiteRequest = new GetSiteRequest
            {
                SiteId = createSiteResponse.Body.SiteId,
            };
            int? currentRetry = 0;
            int? delayedTime = 60000;
            int? interval = 10000;

            while (currentRetry < 5) {
                try
                {
                    int? sleepTime = 0;
                    if (currentRetry == 0)
                    {
                        sleepTime = delayedTime;
                    }
                    else
                    {
                        sleepTime = interval;
                    }
                    Console.WriteLine("Polling for asynchronous results...");
                    await Task.Delay(sleepTime.Value);
                }
                catch (Darabonba.Exceptions.DaraException error)
                {
                    throw new Darabonba.Exceptions.DaraException(new Dictionary<string, string>
                    {
                        {"message", error.Message},
                    });
                }
                GetSiteResponse getSiteResponse = await client.GetSiteAsync(getSiteRequest);
                string status = getSiteResponse.Body.SiteModel.Status;
                if (status == "pending")
                {
                    Console.WriteLine("Call CreateSite success, response: ");
                    Console.WriteLine(Common.ToJSONString(createSiteResponse));
                    return createSiteResponse.Body;
                }
                currentRetry++;
            }
            throw new Darabonba.Exceptions.DaraException(new Dictionary<string, string>
            {
                {"message", "Asynchronous check failed"},
            });
        }


        public static async Task<CreateHttpRequestHeaderModificationRuleResponseBody> ReqHdrModRuleAsync(CreateSiteResponseBody siteResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("Begin Call CreateHttpRequestHeaderModificationRule to create resource");
            CreateHttpRequestHeaderModificationRuleRequest.CreateHttpRequestHeaderModificationRuleRequestRequestHeaderModification requestHeaderModification = new CreateHttpRequestHeaderModificationRuleRequest.CreateHttpRequestHeaderModificationRuleRequestRequestHeaderModification
            {
                Type = "static",
                Value = "add",
                Operation = "add",
                Name = "testadd",
            };
            CreateHttpRequestHeaderModificationRuleRequest.CreateHttpRequestHeaderModificationRuleRequestRequestHeaderModification requestHeaderModification1 = new CreateHttpRequestHeaderModificationRuleRequest.CreateHttpRequestHeaderModificationRuleRequestRequestHeaderModification
            {
                Operation = "del",
                Name = "testdel",
            };
            CreateHttpRequestHeaderModificationRuleRequest.CreateHttpRequestHeaderModificationRuleRequestRequestHeaderModification requestHeaderModification2 = new CreateHttpRequestHeaderModificationRuleRequest.CreateHttpRequestHeaderModificationRuleRequestRequestHeaderModification
            {
                Type = "dynamic",
                Value = "ip.geoip.country",
                Operation = "modify",
                Name = "testmodify",
            };
            CreateHttpRequestHeaderModificationRuleRequest createHttpRequestHeaderModificationRuleRequest = new CreateHttpRequestHeaderModificationRuleRequest
            {
                SiteId = siteResponseBody.SiteId,
                RuleEnable = "on",
                Rule = "(http.host eq \"video.example.com\")",
                Sequence = 1,
                SiteVersion = 0,
                RuleName = "test",
                RequestHeaderModification = new List<CreateHttpRequestHeaderModificationRuleRequest.CreateHttpRequestHeaderModificationRuleRequestRequestHeaderModification>
                {
                    requestHeaderModification,
                    requestHeaderModification1,
                    requestHeaderModification2
                },
            };
            CreateHttpRequestHeaderModificationRuleResponse createHttpRequestHeaderModificationRuleResponse = await client.CreateHttpRequestHeaderModificationRuleAsync(createHttpRequestHeaderModificationRuleRequest);
            Console.WriteLine("Call CreateHttpRequestHeaderModificationRule success, response: ");
            Console.WriteLine(Common.ToJSONString(createHttpRequestHeaderModificationRuleResponse));
            return createHttpRequestHeaderModificationRuleResponse.Body;
        }


        public static async Task UpdateReqHdrModRuleAsync(CreateSiteResponseBody siteResponseBody, CreateHttpRequestHeaderModificationRuleResponseBody createHttpRequestHeaderModificationRuleResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("Begin Call UpdateHttpRequestHeaderModificationRule to update resource");
            UpdateHttpRequestHeaderModificationRuleRequest.UpdateHttpRequestHeaderModificationRuleRequestRequestHeaderModification requestHeaderModification = new UpdateHttpRequestHeaderModificationRuleRequest.UpdateHttpRequestHeaderModificationRuleRequestRequestHeaderModification
            {
                Type = "static",
                Value = "modify1",
                Operation = "modify",
                Name = "testmodify1",
            };
            UpdateHttpRequestHeaderModificationRuleRequest updateHttpRequestHeaderModificationRuleRequest = new UpdateHttpRequestHeaderModificationRuleRequest
            {
                SiteId = siteResponseBody.SiteId,
                RuleEnable = "off",
                Rule = "(http.request.uri eq \"/content?page=1234\")",
                Sequence = 1,
                RuleName = "test_modify",
                RequestHeaderModification = new List<UpdateHttpRequestHeaderModificationRuleRequest.UpdateHttpRequestHeaderModificationRuleRequestRequestHeaderModification>
                {
                    requestHeaderModification
                },
                ConfigId = createHttpRequestHeaderModificationRuleResponseBody.ConfigId,
            };
            UpdateHttpRequestHeaderModificationRuleResponse updateHttpRequestHeaderModificationRuleResponse = await client.UpdateHttpRequestHeaderModificationRuleAsync(updateHttpRequestHeaderModificationRuleRequest);
            Console.WriteLine("Call UpdateHttpRequestHeaderModificationRule success, response: ");
            Console.WriteLine(Common.ToJSONString(updateHttpRequestHeaderModificationRuleResponse));
        }


        public static async Task DestroyReqHdrModRuleAsync(CreateSiteResponseBody siteResponseBody, CreateHttpRequestHeaderModificationRuleResponseBody createHttpRequestHeaderModificationRuleResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("Begin Call DeleteHttpRequestHeaderModificationRule to destroy resource");
            DeleteHttpRequestHeaderModificationRuleRequest deleteHttpRequestHeaderModificationRuleRequest = new DeleteHttpRequestHeaderModificationRuleRequest
            {
                SiteId = siteResponseBody.SiteId,
                ConfigId = createHttpRequestHeaderModificationRuleResponseBody.ConfigId,
            };
            DeleteHttpRequestHeaderModificationRuleResponse deleteHttpRequestHeaderModificationRuleResponse = await client.DeleteHttpRequestHeaderModificationRuleAsync(deleteHttpRequestHeaderModificationRuleRequest);
            Console.WriteLine("Call DeleteHttpRequestHeaderModificationRule success, response: ");
            Console.WriteLine(Common.ToJSONString(deleteHttpRequestHeaderModificationRuleResponse));
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>Running code may affect the online resources of the current account, please proceed with caution!</para>
        /// </description>
        public static async Task Main(string[] args)
        {
            // The code may contain api calls involving fees. Please ensure that you fully understand the charging methods and prices before running.
            // Set the environment variable COST_ACK to true or delete the following judgment to run the sample code.
            string costAcknowledged = Environment.GetEnvironmentVariable("COST_ACK");
            if (costAcknowledged.IsNull() || !(costAcknowledged == "true"))
            {
                Console.WriteLine("Running code may affect the online resources of the current account, please proceed with caution!");
                return ;
            }
            // Init client
            ESA20240910Client esa20240910Client = CreateESA20240910Client();
            // Init resource
            //resource_HttpRequestHeaderModificationRule_test
            PurchaseRatePlanResponseBody ratePlanInstRespBody = await RatePlanInstAsync(esa20240910Client);
            //resource_Site_HttpRequestHeaderModificationRule_test
            CreateSiteResponseBody siteRespBody = await SiteAsync(ratePlanInstRespBody, esa20240910Client);
            CreateHttpRequestHeaderModificationRuleResponseBody reqHdrModRuleRespBody = await ReqHdrModRuleAsync(siteRespBody, esa20240910Client);
            // update resource
            await UpdateReqHdrModRuleAsync(siteRespBody, reqHdrModRuleRespBody, esa20240910Client);
            // destroy resource
            await DestroyReqHdrModRuleAsync(siteRespBody, reqHdrModRuleRespBody, esa20240910Client);
        }

    }
}

