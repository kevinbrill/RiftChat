using System;
using rift.net;
using log4net;
using rift.net.Models;
using rift.net.Models.Guilds;
using System.Linq;

namespace RiftChat
{
	class MainClass
	{
		private static readonly ILog logger = LogManager.GetLogger (typeof(MainClass));

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
			var riftClient = new RiftClientSecured (session);

			var characters = riftClient.ListCharacters ();
			var character = characters.FirstOrDefault (x => x.FullName == arguments.Character);

			if (character == null) {

				var characterList = string.Join(System.Environment.NewLine, characters.OrderBy(x=>x.Shard.Name).ThenBy(x=>x.Name).Select(x=>string.Format("\t{0}", x.FullName)));

				logger.ErrorFormat ("Unable to locate {0} in your list of characters.  Please use one of the following:", arguments.Character);
				logger.ErrorFormat (characterList);

				return;
			}

			var chatClient = new RiftChatClient (session, character);
			var client = new RiftClientSecured (session);
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

			ReportGuildStatus (character, client);

			// Connect
			chatClient.Connect ();

			// Start listening
			chatClient.Listen ();

			string message;

			do { 
				message = Console.ReadLine();

				if(message.StartsWith("/status"))
				{
					ReportGuildStatus( character, client );
				}
				else if( message.StartsWith( "/online" ) )
				{
					ReportOnline(character, client);
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
			Console.WriteLine (string.Format ("{0} {1}", dateTime == null ? DateTime.Now.ToString("HH:mm:ss") : dateTime.Value.ToString("HH:mm:ss"), message));
			Console.ForegroundColor = currentColor;
		}

		private static void ReportOnline( Character character, RiftClientSecured client )
		{
			var currentColor = Console.ForegroundColor;

			var friends = client.ListFriends (character.Id);
			var onlineFriends = friends.Where (x => x.Presence.IsOnlineInGame || x.Presence.IsOnlineOnWeb).OrderBy (x => x.Name);

			Console.WriteLine ("There are {0} friends online currently", onlineFriends.Count());
			foreach (var friend in onlineFriends) {
				Console.WriteLine ("\t{0} {1}", friend.Name, friend.Presence.IsOnlineOnWeb ? "(m)" : "");
			}

			if (character.Guild != null) {
				var guildies = client.ListGuildmates (character.Guild.Id);
				var onlineGuildies = guildies.Where (x => x.Presence.IsOnlineInGame).OrderBy (x => x.Name);

				Console.WriteLine ("There are {0} guildies online currently", onlineGuildies.Count());
				foreach (var guildMate in onlineGuildies) {
					Console.WriteLine ("\t{0}", guildMate.Name);
				}
			}
		}

		private static void ReportGuildStatus( Character character, RiftClientSecured client )
		{
			var currentColor = Console.ForegroundColor;

			Console.ForegroundColor = ConsoleColor.Green;

			var guildInfo = client.GetGuildInfo (character.Id);

			Console.WriteLine ("Welcome to <{0}>", guildInfo.Name);
			Console.WriteLine (guildInfo.MessageOfTheDay);

			var guildies = client.ListGuildmates (guildInfo.Id);
			var onlineGuildies = guildies.Where (x => x.Presence.IsOnlineInGame).OrderBy (x => x.Name);

			Console.WriteLine ("There are {0} guildies online currently", onlineGuildies.Count());
			foreach (var guildMate in onlineGuildies) {
				Console.WriteLine ("\t{0}", guildMate.Name);
			}

			Console.ForegroundColor = currentColor;
		}
	}
}

