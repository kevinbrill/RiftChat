using System;
using Gtk;
using RiftChat.Common;

namespace RiftChat
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();

			var foo = new ProgramFlowController ();
			foo.Authenticate ();
			/*
			MainWindow win = new MainWindow ();
			win.Show ();
			*/
			Application.Run ();
		}
	}
}
