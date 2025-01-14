using Newtonsoft.Json;

namespace CloudPOS.Utils
{
    public static class SessionHelper
    {
        public static void SetDataToSession<T>(ISession session, string key, T value) 
        { 
            session.SetString(key, JsonConvert.SerializeObject(value)); 
        }
        public static T GetDataFromSession<T>(ISession session, string key)
        {
            var value = session.GetString(key); 
            return value == null ? default : JsonConvert.DeserializeObject<T>(value); 
        }
        public static void ClearSession(ISession session)
        {
            session.Clear();
        }
    }
}
