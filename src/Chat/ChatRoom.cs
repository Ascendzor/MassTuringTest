using System;
using System.Collections.Generic;

namespace Chat
{
    public class ChatRoom
    {
		private string firstBrodentification;
		private string secondBrodentification;
		private Dictionary<string, Action<string>> firstBroCallMeMaybe;
		private Dictionary<string, Action<string>> secondBroCallMeMaybe;

		public ChatRoom(string firstBrodentification, Dictionary<string, Action<string>> callMeMaybeFirstBro, string secondBrodentification, Dictionary<string, Action<string>> callMeMaybeSecondBro)
		{
			this.firstBrodentification = firstBrodentification;
			this.secondBrodentification = secondBrodentification;

			this.firstBroCallMeMaybe = callMeMaybeFirstBro;
			this.secondBroCallMeMaybe = callMeMaybeSecondBro;

			callMeMaybeFirstBro["CallMeMaybe"]("because yolo");
			callMeMaybeSecondBro["CallMeMaybe"]("because yolo");
		}

		public void SendThisMessageYo(string leMessage)
		{
			firstBroCallMeMaybe["ReceivedMessage"](leMessage);
			secondBroCallMeMaybe["ReceivedMessage"](leMessage);
		}
    }
}