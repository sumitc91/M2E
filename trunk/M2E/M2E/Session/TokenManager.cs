using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;

namespace M2E.Session
{
    public class TokenManager
    {
        public static void CreateSession(M2ESession session)
        {
            string sessionId = session.SessionId;
            int hours = 1; // TODO: currently hard coded hour value;
            MemoryCache.Default.Set(sessionId, session, new CacheItemPolicy() { SlidingExpiration = new TimeSpan(hours, 0, 0) });
        }

        public static void removeSession(string sessionId)
        {
            MemoryCache.Default.Remove(sessionId);
        }

        public static bool isValidSession(string sessionId)
        {
            M2ESession session = null;
            return isValidSession(sessionId, out session);
        }

        private static bool isValidSession(string sessionId, out  M2ESession session)
        {
            session = null;
            if (MemoryCache.Default.Contains(sessionId))
            {
                session = (M2ESession)MemoryCache.Default.Get(sessionId);
            }
            return verifySessionObject(session);
        }

        private static bool verifySessionObject(M2ESession session)
        {
            return session != null;
        }
    }
}