using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using NNChallenge.Constants;
using System;

namespace NNChallenge.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Spinner spinnerLocations;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_location);

            var buttonForecst = FindViewById<Button>(Resource.Id.button_forecast);
            buttonForecst.Click += OnForecastClick;

            spinnerLocations = FindViewById<Spinner>(Resource.Id.spinner_location);

            var adapter = new ArrayAdapter<string>(
                this,
                Android.Resource.Layout.SimpleSpinnerDropDownItem,
                LocationConstants.LOCATIONS
            );
            

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            spinnerLocations.Adapter = adapter;
        }

        private void OnForecastClick(object sender, EventArgs e)
        {
            var spiner = spinnerLocations.GetItemAtPosition(spinnerLocations.SelectedItemPosition);

            var intent = new Intent(this, typeof(ForecastActivity));
            intent.PutExtra(LocationConstants.LOCATION, spiner.ToString());
            StartActivity(intent);
        }
    }
}
