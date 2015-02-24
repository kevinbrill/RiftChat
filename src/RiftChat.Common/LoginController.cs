using System;
using rift.net.Models;
using rift.net;
using log4net;
using System.Linq;

namespace RiftChat.Common
{
	public class LoginController
	{
		private static readonly ILog logger = LogManager.GetLogger (typeof(LoginController));

		private ILoginView _view;

		public event EventHandler LoginSuccess;
		public event EventHandler LoginFailure;
		public event EventHandler<Character> CharacterSelected;

		public LoginController (ILoginView view)
		{
			_view = view;

			_view.Login += OnHandleLogin;
			_view.CharacterSelected += OnCharacterSelected;
		}

		void OnCharacterSelected (object sender, Character e)
		{
			Character = e;

			_view.CloseView ();

			if (CharacterSelected != null)
				CharacterSelected (this, e);
		}

		void OnHandleLogin (object sender, EventArgs e)
		{
			var factory = new SessionFactory ();

			try {
				Session = factory.Login (_view.UserName, _view.Password);

				if( Session == null ) {
					return;
				}

				var client = new RiftClientSecured(Session);

				var characters = client.ListCharacters().OrderBy( x=>x.Shard.Name).ThenBy(x=>x.Name).ToList();

				_view.SetCharacters( characters );

			} catch (Exception ex) {
				logger.Error (ex);
			} finally {
				if( ( Session == null ) && ( LoginFailure != null ) )
					LoginFailure (this, new EventArgs ());
			}
		}

		public Session Session {
			get;
			private set;
		}

		public Character Character {
			get;
			set;
		}
	}
}