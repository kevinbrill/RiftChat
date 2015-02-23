using System;
using rift.net.Models;
using System.Collections.Generic;

namespace RiftChat.Common
{
	public interface ILoginView
	{
		event EventHandler Login;
		event EventHandler<Character> CharacterSelected;

		string UserName {
			get;
			set;
		}

		string Password {
			get;
			set;
		}

		Character Character {
			get;
			set;
		}

		void ShowView();
		void SetMessage( string message );
		void SetCharacters( List<Character> characters );
	}
}