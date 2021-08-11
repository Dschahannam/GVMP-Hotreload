using System;
using System.Collections.Generic;
using System.Text;

namespace GVMP_HotReload.Model
{
	public class WhitelistDataModel
	{
		public string auth_token { get; private set; }

		public string name { get; private set; }

		public string proxy { get; private set; }

		public uint forum_id { get; private set; }

		public WhitelistDataModel(string p_Token, string p_Name, string p_Proxy, uint p_ForumID)
		{
			auth_token = p_Token;
			name = p_Name;
			proxy = p_Proxy;
			forum_id = p_ForumID;
		}
	}
}
