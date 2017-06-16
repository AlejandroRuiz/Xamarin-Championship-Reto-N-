using Android.App;
using Android.Widget;
using Android.OS;
using TestLast.Service;
using System;

namespace TestLast
{
    [Activity(Label = "TestLast", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += async delegate {
				try
				{
					ServiceHelper serviceHelper = new ServiceHelper();
					// Retrieve the values the user entered into the UI
					string email = "alejandro@alejandroruizvarela.com";
					string reto = RetoId;
					string AndroidId = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

					if (string.IsNullOrEmpty(reto))
					{
						Toast.MakeText(this, "Por favor introduce un correo electr�nico v�lido", ToastLength.Short).Show();
					}
					else
					{
						Toast.MakeText(this, "Enviando tu registro", ToastLength.Short).Show();
						await serviceHelper.InsertarEntidad(email, reto, AndroidId);
						Toast.MakeText(this, "Gracias por registrarte", ToastLength.Long).Show();
						SetResult(Result.Ok, Intent);
					}

				}
				catch (Exception exc)
				{
					Toast.MakeText(this, exc.Message, ToastLength.Long).Show();
					SetResult(Result.Canceled, Intent);
				}
			};
        }

        string RetoId = "RetoN+86218+https://github.com/AlejandroRuiz/XamarinChampionshipRetoN";
    }
}

