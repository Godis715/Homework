using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace API_VK_TEST
{

    class UploadServer {
        [JsonProperty("upload_url")]
        public String url;
    }

    class UploadServerResponse {
        [JsonProperty("response")]
        public UploadServer uploadServer;
    }

    class Photo {
        [JsonProperty("photo")]
        public String photo;
        [JsonProperty("hash")]
        public String hash;
    }

    class VKAPI
    {
        private const String token =
            "50b72c2333ffe133be4c85195d03c557c86" +
            "1b5379449dfedde6a8cb963907b82bfbb95f4efcec54eac289";
        private const String groupID = "172434448";
        private const String url = "https://api.vk.com/method/";
        private const String v = "5.85";

        public MultipartFormDataContent ImageToMultipartForm(String path)
        {
            var fileBytes = File.ReadAllBytes(path);

            var multipartForm = new MultipartFormDataContent {
                {
                    new ByteArrayContent(fileBytes), "photo", path
                }
            };
            
            Console.WriteLine(multipartForm.ReadAsStringAsync().Result);

            return multipartForm;
        }

        public String GetUploadServer() {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data");

                String request = $"{url}photos.getOwnerCoverPhotoUploadServer?access_token={token}&group_id={groupID}&v={v}";
                String responseString = client.GetStringAsync(request).Result;

                UploadServerResponse response = JsonConvert.DeserializeObject<UploadServerResponse>(responseString);

                return response.uploadServer.url;
            }
        }

        public Photo UploadPhoto(String path) {
            String url = GetUploadServer();
            var multipartForm = ImageToMultipartForm(path);

            using (var client = new HttpClient())
            {
                var header = client.DefaultRequestHeaders;
                var responseMessage = client.PostAsync(url, multipartForm).Result;
                

                String responseString = responseMessage.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"UploadPhotoResponse: {responseString}");
                Photo response = JsonConvert.DeserializeObject<Photo>(responseString);

                return response;
            }
        }

        public String SavePhoto(Photo photo) {
            using (var client = new HttpClient())
            {
                String request = $"{url}photos.saveOwnerCoverPhoto?access_token={token}&photo={photo.photo}&hash={photo.hash}&v={v}";
                String responseString = client.GetStringAsync(request).Result;
                return responseString;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            VKAPI vkapi = new VKAPI();

            Photo photo = vkapi.UploadPhoto("image.jpg");
            String response = vkapi.SavePhoto(photo);
            Console.WriteLine(response);
            Console.ReadKey();
        }
    }
}
