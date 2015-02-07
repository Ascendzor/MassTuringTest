using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Collections.Concurrent;

namespace Chat
{
	public interface IManageBros
	{
		void NewBroToManage(string connectionId, Dictionary<string, Action<string>> callMeMaybe);
		void SendThisMessageToMyBro(string connectionId, string leMessage);
    }

	public class BroManager : IManageBros
	{
		private Dictionary<String, Dictionary<string, Action<string>>> connectionIds;
		private Dictionary<string, ChatRoom> chatRooms;

		public BroManager()
		{
			connectionIds = new Dictionary<string, Dictionary<string, Action<string>>>();
			chatRooms = new Dictionary<string, ChatRoom>();
			new Thread(MatchBros).Start();
		}

		public void MatchBros()
		{
			Random leRandom = new Random(0);
			while (true)
			{
				if (connectionIds.Count > 1)
				{
					var firstBro = leRandom.Next(connectionIds.Count);
					var secondBro = leRandom.Next(connectionIds.Count);

					if (firstBro != secondBro)
					{
						var firstBroKey = connectionIds.Keys.ToArray()[firstBro];
						var secondBroKey = connectionIds.Keys.ToArray()[secondBro];

						var leChatRoom = new ChatRoom(firstBroKey, connectionIds[firstBroKey], secondBroKey, connectionIds[secondBroKey]);

						chatRooms.Add(firstBroKey, leChatRoom);
						chatRooms.Add(secondBroKey, leChatRoom);

						connectionIds.Remove(firstBroKey);
						connectionIds.Remove(secondBroKey);

					}
				}

				Thread.Sleep(1000);
			}
		}

		public void NewBroToManage(string connectionId, Dictionary<string, Action<string>> callMeMaybe)
		{
			connectionIds.Add(connectionId, callMeMaybe);
		}

		public void SendThisMessageToMyBro(string connectionId, string leMessage)
		{
			chatRooms[connectionId].SendThisMessageYo(leMessage);
		}
	}
}