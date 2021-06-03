using Amazon;
using Amazon.CognitoIdentity;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CognitoSampleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            var awsAccessKeyId = "2ojjadeiu7vhpttt816eijjge5";
            var secrectKey = "2NfwEZwtIrSSFZ274KUXJptn";
            var appPoolId = "us-east-1_q37FULghv";
            var identityPoolId = "us-east-1:645034002997:userpool/us-east-1_q37FULghv";
            var clientId = "735801293122-fjklkok969jehvbebln66r0690orig4a.apps.googleusercontent.com";
            var username = "usertestawscognito";
            var password = "M1cr0T@l3nt2021";




            var region = Amazon.RegionEndpoint.GetBySystemName("USEast1");

            CognitoAWSCredentials credentials = new CognitoAWSCredentials(
                "us-east-1:6d38082e-e521-4d41-87f9-bf8be0805f93", // ID del grupo de identidades
                region // Región
            );

            using (var s3Client = new AmazonS3Client(credentials))
            {
                // Initial use will be unauthenticated
                s3Client.ListBucketsAsync();

                // Authenticate user through google
                string accessToken = "eyJraWQiOiJhMGVaT3NFUXBtVHpKcFRVMmtrV0NmVWs4ZzRJcnc4c0o3R012ZWpLbVI0PSIsImFsZyI6IlJTMjU2In0.eyJhdF9oYXNoIjoiNUNiUlJuaWVTNXlUSGcya3c3R0FsZyIsInN1YiI6IjY4ZWRjZDNmLThiZTgtNDhjYy05OTQ4LTY1YjQ4Y2E5OGVlZCIsImNvZ25pdG86Z3JvdXBzIjpbInVzLWVhc3QtMV80UjVpN0hOdTJfR29vZ2xlIl0sImVtYWlsX3ZlcmlmaWVkIjpmYWxzZSwiaXNzIjoiaHR0cHM6XC9cL2NvZ25pdG8taWRwLnVzLWVhc3QtMS5hbWF6b25hd3MuY29tXC91cy1lYXN0LTFfNFI1aTdITnUyIiwiY29nbml0bzp1c2VybmFtZSI6Imdvb2dsZV8xMDUyODY2NzY3NTEyOTgwMDg0NDYiLCJhdWQiOiJmajdsM2tob2RqczZidnR2OWllcTdhczVrIiwiaWRlbnRpdGllcyI6W3sidXNlcklkIjoiMTA1Mjg2Njc2NzUxMjk4MDA4NDQ2IiwicHJvdmlkZXJOYW1lIjoiR29vZ2xlIiwicHJvdmlkZXJUeXBlIjoiR29vZ2xlIiwiaXNzdWVyIjpudWxsLCJwcmltYXJ5IjoidHJ1ZSIsImRhdGVDcmVhdGVkIjoiMTYyMjc1MDg2MTM0MiJ9XSwidG9rZW5fdXNlIjoiaWQiLCJhdXRoX3RpbWUiOjE2MjI3NTI2NzgsImV4cCI6MTYyMjc1NjI3OCwiaWF0IjoxNjIyNzUyNjc4LCJlbWFpbCI6ImpvYmVtZUBnbWFpbC5jb20ifQ.cdO6kKuDijtdgjJwHApO2JmD_hkTFq8nbsHa_CgIo9CdiEl8Lh2-mBirO4OrS0_RmdCVwJsXG6KaLuVsRGlbp81F9DPapHNREmoo7PTQ6-BHxgx_F-v95MtDsA295s0bwkIV13BKUWXKuyWVwxAHphSae1j6l-kkRDZu2LDIqGfyvWk8giCGT8s7ia9mp26sxqF7-Qfs_Twz8k4yBv9eMXGWVTJ287_s2PMgP1bIjQSiZyK5t5mnEieWqJYno1cIZAAtywU7PlUEKdhP3T1ip6isbqRuvfZSZFdS088t7JVm2EotMWcNFy4N7eF-LspU7LX_10BFTzCaVZdNUZrepg&access_token=eyJraWQiOiJlb3E2cldtd0t6NnRmVFwvYUxtWWlhTUlienlaTmxpV1pQa1pnQUQ3WE9kWT0iLCJhbGciOiJSUzI1NiJ9.eyJzdWIiOiI2OGVkY2QzZi04YmU4LTQ4Y2MtOTk0OC02NWI0OGNhOThlZWQiLCJjb2duaXRvOmdyb3VwcyI6WyJ1cy1lYXN0LTFfNFI1aTdITnUyX0dvb2dsZSJdLCJ0b2tlbl91c2UiOiJhY2Nlc3MiLCJzY29wZSI6ImF3cy5jb2duaXRvLnNpZ25pbi51c2VyLmFkbWluIHBob25lIG9wZW5pZCBwcm9maWxlIGVtYWlsIiwiYXV0aF90aW1lIjoxNjIyNzUyNjc4LCJpc3MiOiJodHRwczpcL1wvY29nbml0by1pZHAudXMtZWFzdC0xLmFtYXpvbmF3cy5jb21cL3VzLWVhc3QtMV80UjVpN0hOdTIiLCJleHAiOjE2MjI3NTYyNzgsImlhdCI6MTYyMjc1MjY3OCwidmVyc2lvbiI6MiwianRpIjoiODQ3MTIxM2ItY2M1Yy00YzA3LWE5ZGEtNWE2MzJmNDY1YzIwIiwiY2xpZW50X2lkIjoiZmo3bDNraG9kanM2YnZ0djlpZXE3YXM1ayIsInVzZXJuYW1lIjoiZ29vZ2xlXzEwNTI4NjY3Njc1MTI5ODAwODQ0NiJ9.gVQpju01nSl6LT8u4DdVTPWldTOsKCWtDs0bngfaD1-6dL0GXipHWT4uN-ALswYEoWYTWXC25dXRo1GilKlWalvRfu0HArgWa6WUsJ3lnLKnHyewdljSBdwbRszjnsku6XmbaXn1Avk4jrBc2xxhAMWYnUP_TrG4ZYJ4V_XgMYUN57By5Ihc_4Y5nM7-jnStSuod9LXVDDc0GW99mfbhER8u1ir_F6a-1iI5_5HaK1Sbn7th2YaEvgWM4QrmuEbmE06bAV4SJ637cZX8vNNMeLyK2jj1Fgu-NzDT7efNBNiK_gqNP2-mvVKWrKhm7Avbhvss6reXePLkZP66gbXbaw";


                // Add Facebook login to credentials. This clears the current AWS credentials
                // and retrieves new AWS credentials using the authenticated role.
                credentials.AddLogin("accounts.google.com", accessToken);

                // This call is performed with the authenticated role and credentials
                s3Client.ListBucketsAsync();

                var fileBytes = File.ReadAllBytes(@"D:\TestS3\test20210525_1.pdf");
                var memoryStreamFile = new MemoryStream(fileBytes);


                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = memoryStreamFile,
                    Key = "test20210525.pdf",
                    BucketName = "examplebuckecttest"
                };
                var fileTransfertUtility = new TransferUtility(s3Client);
                fileTransfertUtility.Upload(uploadRequest);
            }

            ////Inicialize credential to the cognito Identity provider client so we can authenticate a user 
            //AmazonCognitoIdentityProviderClient provider
            //    = new AmazonCognitoIdentityProviderClient(awsAccessKeyId, secrectKey);

            ////Configure the user pool and User objects for cognito (using the app pool id and username.
            //CognitoUserPool userPool = new CognitoUserPool(appPoolId, clientId, provider);
            //CognitoUser user = new CognitoUser(username, clientId, userPool, provider);

            ////User a Server Side Authentication Flow since this is running on a backend (versus a end-user client App
            //InitiateAdminNoSrpAuthRequest authRequest = new InitiateAdminNoSrpAuthRequest() { Password = password };

            //var authResponse = user.StartWithAdminNoSrpAuthAsync(authRequest);

            //Task.WaitAll(authResponse);

            ////Get short - lived temporary credentials from the authenticated Cognito user
            //CognitoAWSCredentials credentialsCognito = user.GetCognitoAWSCredentials(appPoolId, region);

            ////Use these short lived credentials to interact with the various AWS Services
            //using (var client = new AmazonS3Client(credentialsCognito))
            //{
            //    var response = client.ListBucketsAsync(new ListBucketsRequest());
            //    foreach (S3Bucket bucket in response.Result.Buckets)
            //    {
            //        Console.WriteLine(bucket.BucketName);
            //    }
            //}

        }
    }
}
