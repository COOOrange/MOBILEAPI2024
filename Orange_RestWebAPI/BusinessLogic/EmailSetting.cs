using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orange_RestWebAPI.BusinessLogic
{
    public class EmailSetting
    {
        public string MailServer { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string MailServer_Port { get; set; }
        public string From_Mail { get; set; }
        public bool Ssl { get; set; }
        public string MESReplyTo { get; set; }
        public string MESURI { get; set; }
    }
   

}
