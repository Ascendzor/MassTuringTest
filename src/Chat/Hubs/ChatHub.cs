using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;

namespace Chat.Hubs
{
	[HubName("Chat")]
	public class ChatHub : Hub
	{
		private IManageBros broManager;
		public ChatHub(IManageBros broManager)
		{
			this.broManager = broManager;
		}

		public void CallMeMaybe(string forTheLols)
		{
			Clients.Client(Context.ConnectionId).foundABro(Context.ConnectionId);
		}

		public void ReceivedMessage(string message)
		{
			Clients.Client(Context.ConnectionId).receivedMessage(message);
		}

		public void ANewBroHasArrived(string a, string b)
		{
			var leActions = new Dictionary<string, Action<string>>();
			leActions.Add("CallMeMaybe", CallMeMaybe);
			leActions.Add("ReceivedMessage", ReceivedMessage);
			broManager.NewBroToManage(Context.ConnectionId, leActions);
		}

		public void SendingAMessageToMyBro(string leMessage)
		{
			broManager.SendThisMessageToMyBro(Context.ConnectionId, leMessage);
		}
	}
}