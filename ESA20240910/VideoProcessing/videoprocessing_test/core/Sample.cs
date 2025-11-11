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


        public static async Task<CreateVideoProcessingResponseBody> VideoProcAsync(CreateSiteResponseBody siteResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("Begin Call CreateVideoProcessing to create resource");
            CreateVideoProcessingRequest createVideoProcessingRequest = new CreateVideoProcessingRequest
            {
                VideoSeekEnable = "on",
                SiteId = siteResponseBody.SiteId,
                RuleEnable = "on",
                FlvVideoSeekMode = "by_byte",
                Mp4SeekEnd = "end",
                FlvSeekStart = "start",
                Rule = "(http.host eq \"video.example.com\")",
                Sequence = 1,
                Mp4SeekStart = "start",
                SiteVersion = 0,
                FlvSeekEnd = "end",
                RuleName = "test",
            };
            CreateVideoProcessingResponse createVideoProcessingResponse = await client.CreateVideoProcessingAsync(createVideoProcessingRequest);
            Console.WriteLine("Call CreateVideoProcessing success, response: ");
            Console.WriteLine(Common.ToJSONString(createVideoProcessingResponse));
            return createVideoProcessingResponse.Body;
        }


        public static async Task UpdateVideoProcAsync(CreateSiteResponseBody siteResponseBody, CreateVideoProcessingResponseBody createVideoProcessingResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("Begin Call UpdateVideoProcessing to update resource");
            UpdateVideoProcessingRequest updateVideoProcessingRequest = new UpdateVideoProcessingRequest
            {
                VideoSeekEnable = "off",
                SiteId = siteResponseBody.SiteId,
                RuleEnable = "off",
                FlvVideoSeekMode = "by_time",
                Mp4SeekEnd = "e",
                FlvSeekStart = "s",
                Rule = "(http.request.uri eq \"/content?page=1234\")",
                Sequence = 1,
                Mp4SeekStart = "s",
                FlvSeekEnd = "e",
                RuleName = "test_modify",
                ConfigId = createVideoProcessingResponseBody.ConfigId,
            };
            UpdateVideoProcessingResponse updateVideoProcessingResponse = await client.UpdateVideoProcessingAsync(updateVideoProcessingRequest);
            Console.WriteLine("Call UpdateVideoProcessing success, response: ");
            Console.WriteLine(Common.ToJSONString(updateVideoProcessingResponse));
        }


        public static async Task DestroyVideoProcAsync(CreateSiteResponseBody siteResponseBody, CreateVideoProcessingResponseBody createVideoProcessingResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("Begin Call DeleteVideoProcessing to destroy resource");
            DeleteVideoProcessingRequest deleteVideoProcessingRequest = new DeleteVideoProcessingRequest
            {
                SiteId = siteResponseBody.SiteId,
                ConfigId = createVideoProcessingResponseBody.ConfigId,
            };
            DeleteVideoProcessingResponse deleteVideoProcessingResponse = await client.DeleteVideoProcessingAsync(deleteVideoProcessingRequest);
            Console.WriteLine("Call DeleteVideoProcessing success, response: ");
            Console.WriteLine(Common.ToJSONString(deleteVideoProcessingResponse));
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
            PurchaseRatePlanResponseBody ratePlanInstRespBody = await RatePlanInstAsync(esa20240910Client);
            CreateSiteResponseBody siteRespBody = await SiteAsync(ratePlanInstRespBody, esa20240910Client);
            CreateVideoProcessingResponseBody videoProcRespBody = await VideoProcAsync(siteRespBody, esa20240910Client);
            // update resource
            await UpdateVideoProcAsync(siteRespBody, videoProcRespBody, esa20240910Client);
            // destroy resource
            await DestroyVideoProcAsync(siteRespBody, videoProcRespBody, esa20240910Client);
        }

    }
}

