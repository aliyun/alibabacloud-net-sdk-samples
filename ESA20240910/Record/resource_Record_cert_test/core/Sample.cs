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


        public static async Task<PurchaseRatePlanResponseBody> RatePlanInstCertAsync(ESA20240910Client client)
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


        public static async Task<CreateSiteResponseBody> SiteCertAsync(PurchaseRatePlanResponseBody ratePlanInstCertResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("Begin Call CreateSite to create resource");
            CreateSiteRequest createSiteRequest = new CreateSiteRequest
            {
                SiteName = "gositecdn.cn",
                InstanceId = ratePlanInstCertResponseBody.InstanceId,
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


        public static async Task<CreateRecordResponseBody> RecordCertAsync(CreateSiteResponseBody siteCertResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("Begin Call CreateRecord to create resource");
            CreateRecordRequest.CreateRecordRequestData data = new CreateRecordRequest.CreateRecordRequestData
            {
                Type = 111,
                KeyTag = 11,
                Algorithm = 11,
                Certificate = "eGVzdGFsbWNkbg==",
            };
            CreateRecordRequest createRecordRequest = new CreateRecordRequest
            {
                RecordName = "www.gositecdn.cn",
                Comment = "This is a remark",
                SiteId = siteCertResponseBody.SiteId,
                Type = "CERT",
                Data = data,
                Ttl = 100,
            };
            CreateRecordResponse createRecordResponse = await CreateRecordWithRetryAsync(client, createRecordRequest);
            Console.WriteLine("Call CreateRecord success, response: ");
            Console.WriteLine(Common.ToJSONString(createRecordResponse));
            return createRecordResponse.Body;
        }


        public static async Task<CreateRecordResponse> CreateRecordWithRetryAsync(ESA20240910Client client, CreateRecordRequest createRecordRequest)
        {
            string errorCode = "";
            int? retry1 = 0;
            int? interval1 = 5000;
            int? retry2 = 0;
            int? interval2 = 5000;

            while ((retry1 < 10) || (retry2 < 20)) {
                try
                {
                    CreateRecordResponse createRecordResponse = await client.CreateRecordAsync(createRecordRequest);
                    Console.WriteLine("Call CreateRecord success, response: ");
                    Console.WriteLine(Common.ToJSONString(createRecordResponse));
                    return createRecordResponse;
                }
                catch (Darabonba.Exceptions.DaraException error)
                {
                    errorCode = error.Code;
                }
                if (errorCode == "Site.ServiceBusy")
                {
                    Console.WriteLine("Call CreateRecord failed, errorCode: Site.ServiceBusy, please retry");
                    await Task.Delay(interval1.Value);
                    retry1++;
                }
                if (errorCode == "TooManyRequests")
                {
                    Console.WriteLine("Call CreateRecord failed, errorCode: TooManyRequests, please retry");
                    await Task.Delay(interval2.Value);
                    retry2++;
                }
            }
            throw new Darabonba.Exceptions.DaraException(new Dictionary<string, string>
            {
                {"message", "Call CreateRecord failed"},
            });
        }


        public static async Task UpdateRecordCertAsync(CreateRecordResponseBody createRecordResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("Begin Call UpdateRecord to update resource");
            UpdateRecordRequest.UpdateRecordRequestData data = new UpdateRecordRequest.UpdateRecordRequestData
            {
                Type = 222,
                KeyTag = 22,
                Algorithm = 22,
                Certificate = "bGVzdGGsbWNkbg==",
            };
            UpdateRecordRequest updateRecordRequest = new UpdateRecordRequest
            {
                Comment = "test_record_comment",
                Data = data,
                Ttl = 86400,
                RecordId = createRecordResponseBody.RecordId,
            };
            UpdateRecordResponse updateRecordResponse = await UpdateRecordWithRetryAsync(client, updateRecordRequest);
            Console.WriteLine("Call UpdateRecord success, response: ");
            Console.WriteLine(Common.ToJSONString(updateRecordResponse));
        }


        public static async Task<UpdateRecordResponse> UpdateRecordWithRetryAsync(ESA20240910Client client, UpdateRecordRequest updateRecordRequest)
        {
            string errorCode = "";
            int? retry1 = 0;
            int? interval1 = 5000;
            int? retry2 = 0;
            int? interval2 = 3000;

            while ((retry1 < 20) || (retry2 < 10)) {
                try
                {
                    UpdateRecordResponse updateRecordResponse = await client.UpdateRecordAsync(updateRecordRequest);
                    Console.WriteLine("Call UpdateRecord success, response: ");
                    Console.WriteLine(Common.ToJSONString(updateRecordResponse));
                    return updateRecordResponse;
                }
                catch (Darabonba.Exceptions.DaraException error)
                {
                    errorCode = error.Code;
                }
                if (errorCode == "TooManyRequests")
                {
                    Console.WriteLine("Call UpdateRecord failed, errorCode: TooManyRequests, please retry");
                    await Task.Delay(interval1.Value);
                    retry1++;
                }
                if (errorCode == "Record.ServiceBusy")
                {
                    Console.WriteLine("Call UpdateRecord failed, errorCode: Record.ServiceBusy, please retry");
                    await Task.Delay(interval2.Value);
                    retry2++;
                }
            }
            throw new Darabonba.Exceptions.DaraException(new Dictionary<string, string>
            {
                {"message", "Call UpdateRecord failed"},
            });
        }


        public static async Task DestroyRecordCertAsync(CreateRecordResponseBody createRecordResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("Begin Call DeleteRecord to destroy resource");
            DeleteRecordRequest deleteRecordRequest = new DeleteRecordRequest
            {
                RecordId = createRecordResponseBody.RecordId,
            };
            DeleteRecordResponse deleteRecordResponse = await DeleteRecordWithRetryAsync(client, deleteRecordRequest);
            Console.WriteLine("Call DeleteRecord success, response: ");
            Console.WriteLine(Common.ToJSONString(deleteRecordResponse));
        }


        public static async Task<DeleteRecordResponse> DeleteRecordWithRetryAsync(ESA20240910Client client, DeleteRecordRequest deleteRecordRequest)
        {
            string errorCode = "";
            int? retry1 = 0;
            int? interval1 = 5000;
            int? retry2 = 0;
            int? interval2 = 1000;

            while ((retry1 < 20) || (retry2 < 10)) {
                try
                {
                    DeleteRecordResponse deleteRecordResponse = await client.DeleteRecordAsync(deleteRecordRequest);
                    Console.WriteLine("Call DeleteRecord success, response: ");
                    Console.WriteLine(Common.ToJSONString(deleteRecordResponse));
                    return deleteRecordResponse;
                }
                catch (Darabonba.Exceptions.DaraException error)
                {
                    errorCode = error.Code;
                }
                if (errorCode == "TooManyRequests")
                {
                    Console.WriteLine("Call DeleteRecord failed, errorCode: TooManyRequests, please retry");
                    await Task.Delay(interval1.Value);
                    retry1++;
                }
                if (errorCode == "Record.ServiceBusy")
                {
                    Console.WriteLine("Call DeleteRecord failed, errorCode: Record.ServiceBusy, please retry");
                    await Task.Delay(interval2.Value);
                    retry2++;
                }
            }
            throw new Darabonba.Exceptions.DaraException(new Dictionary<string, string>
            {
                {"message", "Call DeleteRecord failed"},
            });
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
            PurchaseRatePlanResponseBody ratePlanInstCertRespBody = await RatePlanInstCertAsync(esa20240910Client);
            CreateSiteResponseBody siteCertRespBody = await SiteCertAsync(ratePlanInstCertRespBody, esa20240910Client);
            CreateRecordResponseBody recordCertRespBody = await RecordCertAsync(siteCertRespBody, esa20240910Client);
            // update resource
            await UpdateRecordCertAsync(recordCertRespBody, esa20240910Client);
            // destroy resource
            await DestroyRecordCertAsync(recordCertRespBody, esa20240910Client);
        }

    }
}

