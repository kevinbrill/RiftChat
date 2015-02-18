using System;
using rift.net;
using rift.net.Models;

namespace RiftChat.Common
{
	public interface IChatView
	{
		Character Player { get; set; }

		event EventHandler<string> SendMessage;

		void MessageReceived( Message message );
	}
}

