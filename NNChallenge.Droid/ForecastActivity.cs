using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using FFImageLoading;
using NNChallenge.Constants;
using NNChallenge.Implementations;
using NNChallenge.Interfaces;

namespace NNChallenge.Droid
{
    [Activity(Label = "")]
    public class ForecastActivity : AppCompatActivity
    {
        private readonly IRestService restService;

        public ForecastActivity()
        {
            restService = new RestService();
        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            var location = Intent.GetStringExtra(LocationConstants.LOCATION);
            Title = location;

            base.OnCreate(savedInstanceState);

            var forecastData = await restService.GetData(location);
            var adapter = new WeatherRecyclerViewAdapter(forecastData.HourForecast);
            var layoutManager = new LinearLayoutManager(this);

            SetContentView(Resource.Layout.activity_forecast);

            var recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerview);
            recyclerView.SetAdapter(adapter);
            recyclerView.SetLayoutManager(layoutManager);
        }
    }

    internal class WeatherViewHolder : RecyclerView.ViewHolder
    {
        public WeatherViewHolder(View view) : base(view)
        {
            Icon = view.FindViewById<ImageView>(Resource.Id.icon);
            TextTemperature = view.FindViewById<TextView>(Resource.Id.text_temperature);
            TextDateTime = view.FindViewById<TextView>(Resource.Id.text_datetime);
        }

        public ImageView Icon { get; }

        public TextView TextTemperature { get; }

        public TextView TextDateTime { get; }
    }

    internal class WeatherRecyclerViewAdapter : RecyclerView.Adapter
    {
        private readonly IHourWeatherForecastVO[] forecastValues;

        public WeatherRecyclerViewAdapter(IHourWeatherForecastVO[] values)
        {
            forecastValues = values;
        }

        public override int ItemCount => forecastValues.Length;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var weatherViewHolder = holder as WeatherViewHolder;

            var imageUrl = forecastValues[position].ForecastPictureURL;
            ImageService.Instance.LoadUrl(imageUrl).Into(weatherViewHolder.Icon);

            var textTemperature = $"{forecastValues[position].TemperatureCelcius:0.00}C / {forecastValues[position].TemperatureFahrenheit:0.00}F";
            weatherViewHolder.TextTemperature.Text = textTemperature;

            var textDateTime = forecastValues[position].Date.ToString("MMMM dd, yyyy HH:mm");
            weatherViewHolder.TextDateTime.Text = textDateTime;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item, parent, false);
            return new WeatherViewHolder(view);
        }
    }
}
