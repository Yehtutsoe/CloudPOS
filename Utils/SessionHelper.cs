using Newtonsoft.Json;

namespace CloudPOS.Utils
{
    public static class SessionHelper
    {
        public static void SetDataToSession(this ISession session,string key,object value)
        {
            session.Clear();
        }
    }
}
