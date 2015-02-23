using System;
using RiftChat.Common;
using Gtk;

namespace RiftChat
{
	public partial class LoginWindow : Gtk.Window, ILoginView
	{
		public LoginWindow () :
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}

		#region ILoginView implementation

		public event EventHandler Login;

		public event EventHandler<rift.net.Models.Character> CharacterSelected;

		public void ShowView ()
		{
			this.Show ();
		}

		public void SetMessage (string message)
		{
			throw new NotImplementedException ();
		}

		public void SetCharacters (System.Collections.Generic.List<rift.net.Models.Character> characters)
		{
			var model = new TreeModel ();

			this.comboboxCharacters.Clear ();

						throw new NotImplementedException ();
		}

		public string UserName {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		public string Password {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		public rift.net.Models.Character Character {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		#endregion
	}
}

