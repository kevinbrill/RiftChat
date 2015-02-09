using System;
using rift.net;
using log4net;
using rift.net.Models;
using rift.net.Models.Guilds;
using System.Linq;
using System.Collections.Generic;
using System.Net;

namespace RiftChat
{
	class MainClass
	{
		private static readonly ILog logger = LogManager.GetLogger (typeof(MainClass));

		private static RiftClientSecured securedClient;
		private static RiftChatClient chatClient;

		public static void Main (string[] args)
		{
			log4net.Config.XmlConfigurator.Configure ();

			if ((args == null) || (args.Length == 0)) 
			{
				Console.WriteLine ("Usage: RiftScratchGame /u <username> /p <password> /c <character name@shard name>");
				return;
			}

			// Clear the console.
			Console.Clear ();

			var arguments = Args.Configuration.Configure<CommandObject> ().CreateAndBind (args);

			var sessionFactory = new SessionFactory ();
			Session session;

			// Login
			try 
			{
				session = sessionFactory.Login (arguments.Username, arguments.Password);
			} 
			catch( AuthenticationException ex )
			{
				logger.Error (ex.Message);
				return;
			}
			catch (Exception ex) 
			{
				logger.Error ("An unexpected error occurred", ex);
				return;
			}

			// Create a new client
			securedClient = new RiftClientSecured (session);

			var characters = securedClient.ListCharacters ();
			var character = characters.FirstOrDefault (x => x.FullName == arguments.Character);

			if (character == null) {

				var characterList = string.Join(System.Environment.NewLine, characters.OrderBy(x=>x.Shard.Name).ThenBy(x=>x.Name).Select(x=>string.Format("\t{0}", x.FullName)));

				logger.ErrorFormat ("Unable to locate {0} in your list of characters.  Please use one of the following:", arguments.Character);
				logger.ErrorFormat (characterList);

				return;
			}

			chatClient = new RiftChatClient (session, character);
			var waiter = new Waiter ();

			chatClient.GuildChatReceived += delegate(object sender, Message e) {
				WriteMessage(string.Format("{0}: {1}", e.Sender.Name, e.Text), ConsoleColor.Green );
			};

			chatClient.WhisperReceived +=  delegate(object sender, Message e) {
				WriteMessage(string.Format("{0}: {1}", e.Sender.Name, e.Text), ConsoleColor.Magenta );
			};

			chatClient.OfficerChatReceived +=  delegate(object sender, Message e) {
				WriteMessage(string.Format("{0}: {1}", e.Sender.Name, e.Text), ConsoleColor.DarkGreen );
			};

			chatClient.Login +=  delegate(object sender, rift.net.Models.Action e) {
				if( !e.InGame )
					return;

				WriteMessage( string.Format("{0} has come online.", e.Character.Name), ConsoleColor.Gray );
			};

			chatClient.Logout +=  delegate(object sender, rift.net.Models.Action e) {
				if( !e.InGame )
					return;

				WriteMessage( string.Format("{0} has gone offline.", e.Character.Name), ConsoleColor.Gray );
			};

			ReportGuildStatus (character);

			// Connect
			chatClient.Connect ();

			// Start listening
			chatClient.Listen ();

			string message;

			do { 
				message = Console.ReadLine();

				if(string.IsNullOrWhiteSpace(message))
					continue;

				message = message.Trim();

				if(message == "/status")
				{
					ReportGuildStatus(character);
				}
				else if( message == "/online" )
				{
					ReportOnline(character);
				}
				else if( message.StartsWith("/t "))
				{
					HandleTell( character, message );
				}
				else if( message == "/events" )
				{
					ReportZoneEvents();
				}
				else 
				{
					if (message != null) 
						chatClient.SendGuildMessage(message);
				}

			} while (message != null);

			// Wait for an interrupt
			waiter.Wait ();

			// Stop listening
			chatClient.Stop ();
		}

		private static void WriteMessage(string message, ConsoleColor textColor, DateTime? dateTime = null)
		{
			var currentColor = Console.ForegroundColor;

			Console.ForegroundColor = textColor;
			Console.WriteLine (string.Format ("{0} {1}", dateTime == null ? DateTime.Now.ToString("HH:mm:ss") : dateTime.Value.ToString("HH:mm:ss"), WebUtility.UrlDecode(message)));
			Console.ForegroundColor = currentColor;
		}

		private static void ReportOnline( Character character)
		{
			var currentColor = Console.ForegroundColor;

			var friends = securedClient.ListFriends (character.Id);
			var onlineFriends = friends.Where (x => x.Presence.IsOnlineInGame || x.Presence.IsOnlineOnWeb).OrderBy (x => x.Name);

			Console.WriteLine ("There are {0} friends online currently", onlineFriends.Count());
			foreach (var friend in onlineFriends) {
				Console.WriteLine ("\t{0} {1}", friend.Name, friend.Presence.IsOnlineOnWeb ? "(m)" : "");
			}

			if (character.Guild != null) {
				var guildies = securedClient.ListGuildmates (character.Guild.Id);
				var onlineGuildies = guildies.Where (x => x.Presence.IsOnlineInGame).OrderBy (x => x.Name);

				Console.WriteLine ("There are {0} guildies online currently", onlineGuildies.Count());
				foreach (var guildMate in onlineGuildies) {
					Console.WriteLine ("\t{0}", guildMate.Name);
				}
			}
		}

		private static void ReportGuildStatus( Character character)
		{
			var currentColor = Console.ForegroundColor;

			Console.ForegroundColor = ConsoleColor.Green;

			var guildInfo = securedClient.GetGuildInfo (character.Id);

			Console.WriteLine ("Welcome to <{0}>", guildInfo.Name);
			Console.WriteLine (guildInfo.MessageOfTheDay);

			var guildies = securedClient.ListGuildmates (guildInfo.Id);
			var onlineGuildies = guildies.Where (x => x.Presence.IsOnlineInGame).OrderBy (x => x.Name);

			Console.WriteLine ("There are {0} guildies online currently", onlineGuildies.Count());
			foreach (var guildMate in onlineGuildies) {
				Console.WriteLine ("\t{0}", guildMate.Name);
			}

			Console.ForegroundColor = currentColor;
		}

		private static void HandleTell( Character character, string message )
		{
			var messageParts = message.Split (' ');

			// If message parts does not contain two substrings, then we've got a problem
			if (messageParts.Length < 2)
				throw new Exception ("Unable to parse message text");

			if (messageParts [0] != "/t")
				throw new Exception ("This does not look like a tell");

			var recipientName = messageParts [1].ToLower ();

			// Lookup the recipient from the friends and guild list
			var recipient = ListFriendsAndGuildies (character).FirstOrDefault (x => x.Name.ToLower () == recipientName);

			var actualMessage = string.Join (" ", messageParts, 2, messageParts.Length - 2);

			var result = chatClient.SendWhisper (recipient, actualMessage);
		}

		private static List<Contact> ListFriendsAndGuildies( Character character )
		{
			return securedClient.ListFriends (character.Id).Union (securedClient.ListGuildmates (character.Guild.Id)).ToList();
		}

		private static void ReportZoneEvents()
		{
		}
	}
}

