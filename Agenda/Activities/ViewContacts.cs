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
    [Activity(Label = "Agenda", MainLauncher = true, Icon = "@drawable/icon")]
    public class ViewContacts : ListActivity
    {
        List<Contact> contacts;

        protected override async void OnResume()
        {
            base.OnResume();

            contacts = await Database.GetContacts();
            ListAdapter = new ContactsAdapter(this, contacts);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Database.CreateDatabase();

            SetContentView(Resource.Layout.view);

            Button btnAdd = FindViewById<Button>(Resource.Id.btnAdd);

            btnAdd.Click += delegate
            {
                var edit = new Intent(this, typeof(EditContact));
                StartActivity(edit);
            };
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);

            if (position != -1)
            {
                var second = new Intent(this, typeof(EditContact));
                second.PutExtra("id", contacts[position].Id);
                StartActivity(second);
            }
        }
    }
}