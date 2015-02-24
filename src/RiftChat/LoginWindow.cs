using System;
using RiftChat.Common;
using Gtk;
using rift.net.Models;

namespace RiftChat
{
	public partial class LoginWindow : Gtk.Window, ILoginView
	{
		private ListStore charactersModel = new ListStore (typeof(string), typeof(Character));

		public LoginWindow () :
		base (Gtk.WindowType.Toplevel)
		{
			this.Build ();

			var renderer = new CellRendererText ();
			this.comboboxCharacters.PackStart (renderer, false);
			this.comboboxCharacters.AddAttribute (renderer, "text", 0);

			this.buttonLogin.Clicked += (sender, e) => {
				if(Login != null )
					Login( this, new EventArgs());
			};

			this.buttonCharacterSelect.Clicked += (sender, e) =>  {
				if(CharacterSelected != null)
					CharacterSelected( this, this.Character);
			};
		}

		#region ILoginView implementation

		public event EventHandler Login;

		public event EventHandler<rift.net.Models.Character> CharacterSelected;

		public void ShowView ()
		{
			this.Show ();
		}

		public void CloseView()
		{
			this.Hide ();
		}

		public void SetMessage (string message)
		{
			throw new NotImplementedException ();
		}

		public void SetCharacters (System.Collections.Generic.List<rift.net.Models.Character> characters)
		{
			charactersModel.Clear ();

			foreach (var character in characters) {
				charactersModel.AppendValues (character.FullName, character);
			}

			this.comboboxCharacters.Model = charactersModel;
		}

		public string UserName {
			get {
				return this.entryUsername.Text;
			}
			set {
				this.entryUsername.Text = value;
			}
		}

		public string Password {
			get {
				return this.entryPassword.Text;
			}
			set {
				this.entryPassword.Text = value;
			}
		}

		public rift.net.Models.Character Character {
			get {
				TreeIter iter;
				this.comboboxCharacters.GetActiveIter (out iter);
				return (Character)this.comboboxCharacters.Model.GetValue (iter, 1);			
			}
			set {
			}
		}

		#endregion
	}
}

