using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferralPOV.Data
{
    public class ValidationError : Exception
    {
        public string Title;
        public int Status;
        public JObject Errors;


        public string FirstError()
        {
            try
            {
                JToken jt = Errors.First;
                IEnumerable<JToken> values = jt.Values();
                JToken content = values.FirstOrDefault();
                return content.ToString();
            }
            catch(Exception e)
            {
                return "An unknown Error has occured";
            }
        }
    }
}
