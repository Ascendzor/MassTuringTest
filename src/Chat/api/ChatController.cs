using Chat.Hubs;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using System;

namespace Chat.Controllers
{
	public class ChatController : Controller
	{
		private IHubContext hub;

		public ChatController(IConnectionManager connectionManager)
		{
			this.hub = connectionManager.GetHubContext<ChatHub>();
		}
	}
}