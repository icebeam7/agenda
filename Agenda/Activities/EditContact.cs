using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Agenda.Classes;

namespace Agenda.Activities
{
    [Activity(Label = "EditContact")]
    public class EditContact : Activity
    {
        int id = 0;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.edit);

            Button btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
            EditText txtName = FindViewById<EditText>(Resource.Id.txtName);
            EditText txtEmailAddress = FindViewById<EditText>(Resource.Id.txtEmailAddress);
            EditText txtTelephoneNumber = FindViewById<EditText>(Resource.Id.txtTelephoneNumber);

            btnRegister.Click += async delegate {
                Contact contact = new Contact() {
                    Id = id,
                    Name = txtName.Text,
                    TelephoneNumber = txtTelephoneNumber.Text,
                    EmailAddress = txtEmailAddress.Text
                };

                bool result = await Database.RegisterContact(contact, (id == 0));
                new AlertDialog.Builder(this).SetMessage(result ? "Contact registered successfully!" : "An error happened").SetTitle("Message").Show();
            };

            id = Intent.GetIntExtra("id", 0);

            if (id != 0)
            {
                var contact = await Database.GetContact(id);
                txtName.Text = contact.Name;
                txtTelephoneNumber.Text = contact.TelephoneNumber;
                txtEmailAddress.Text = contact.EmailAddress;
            }
        }
    }
}