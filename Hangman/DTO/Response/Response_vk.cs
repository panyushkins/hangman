using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.DTO.Response
{
    public class SuccessVK
    {
        [JsonProperty("post_id")]
        public int Post_id { get; set; }
    }

    public class Error
    {
        [JsonProperty("error_code")]
        public int Code { get; set; }

        [JsonProperty("error_msg")]
        public string Message { get; set; }

    }

    public class Response4
    {
        [JsonProperty("response")]
        public List<SuccessVK> response { get; set; }

        [JsonProperty("post_id")]
        public int Post_id { get; set; }

        [JsonProperty("error")]
        public List<Error> error { get; set;}
    }
}