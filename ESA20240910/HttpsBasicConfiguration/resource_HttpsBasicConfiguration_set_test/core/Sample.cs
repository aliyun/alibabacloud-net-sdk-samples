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


        public static async Task<CreateHttpsBasicConfigurationResponseBody> HttpsCfgAsync(CreateSiteResponseBody siteResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("Begin Call CreateHttpsBasicConfiguration to create resource");
            CreateHttpsBasicConfigurationRequest createHttpsBasicConfigurationRequest = new CreateHttpsBasicConfigurationRequest
            {
                SiteId = siteResponseBody.SiteId,
                RuleEnable = "on",
                Https = "on",
                Rule = "true",
                RuleName = "test2",
            };
            CreateHttpsBasicConfigurationResponse createHttpsBasicConfigurationResponse = await client.CreateHttpsBasicConfigurationAsync(createHttpsBasicConfigurationRequest);
            Console.WriteLine("Call CreateHttpsBasicConfiguration success, response: ");
            Console.WriteLine(Common.ToJSONString(createHttpsBasicConfigurationResponse));
            return createHttpsBasicConfigurationResponse.Body;
        }


        public static async Task UpdateHttpsCfgAsync(CreateSiteResponseBody siteResponseBody, CreateHttpsBasicConfigurationResponseBody createHttpsBasicConfigurationResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("Begin Call UpdateHttpsBasicConfiguration to update resource");
            UpdateHttpsBasicConfigurationRequest updateHttpsBasicConfigurationRequest = new UpdateHttpsBasicConfigurationRequest
            {
                SiteId = siteResponseBody.SiteId,
                RuleEnable = "on",
                Https = "off",
                Rule = "true",
                RuleName = "test2",
                ConfigId = createHttpsBasicConfigurationResponseBody.ConfigId,
            };
            UpdateHttpsBasicConfigurationResponse updateHttpsBasicConfigurationResponse = await client.UpdateHttpsBasicConfigurationAsync(updateHttpsBasicConfigurationRequest);
            Console.WriteLine("Call UpdateHttpsBasicConfiguration success, response: ");
            Console.WriteLine(Common.ToJSONString(updateHttpsBasicConfigurationResponse));
        }


        public static async Task DestroyHttpsCfgAsync(CreateSiteResponseBody siteResponseBody, CreateHttpsBasicConfigurationResponseBody createHttpsBasicConfigurationResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("Begin Call DeleteHttpsBasicConfiguration to destroy resource");
            DeleteHttpsBasicConfigurationRequest deleteHttpsBasicConfigurationRequest = new DeleteHttpsBasicConfigurationRequest
            {
                SiteId = siteResponseBody.SiteId,
                ConfigId = createHttpsBasicConfigurationResponseBody.ConfigId,
            };
            DeleteHttpsBasicConfigurationResponse deleteHttpsBasicConfigurationResponse = await client.DeleteHttpsBasicConfigurationAsync(deleteHttpsBasicConfigurationRequest);
            Console.WriteLine("Call DeleteHttpsBasicConfiguration success, response: ");
            Console.WriteLine(Common.ToJSONString(deleteHttpsBasicConfigurationResponse));
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
            CreateHttpsBasicConfigurationResponseBody httpsCfgRespBody = await HttpsCfgAsync(siteRespBody, esa20240910Client);
            // update resource
            await UpdateHttpsCfgAsync(siteRespBody, httpsCfgRespBody, esa20240910Client);
            // destroy resource
            await DestroyHttpsCfgAsync(siteRespBody, httpsCfgRespBody, esa20240910Client);
        }

    }
}

