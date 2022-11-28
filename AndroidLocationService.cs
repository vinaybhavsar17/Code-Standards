using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Microsoft.Extensions.Logging;
using OfficerReports.Interfaces;
using OfficerReports.Platforms.Android.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficerReports.Platforms.Android.Services
{
    [Service]
    public class AndroidLocationService : Service
    {
        private LocationManager _locationManager;
        private OfficerLocationListener _locationListener;

        public const int SERVICE_RUNNING_NOTIFICATION_ID = 10001;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Notification notification = new NotificationHelper().GetLocationTrackingNotification();
            StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);

            StartLocationTracking();

            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            StopLocationTracking();
        }

        private void StartLocationTracking()
        {
            try
            {
                if (_locationManager != null)
                    return;

                _locationManager = (LocationManager)MainApplication.Context.GetSystemService(Context.LocationService);
                _locationListener = new OfficerLocationListener(_locationManager);

                _locationManager.RequestLocationUpdates(LocationManager.FusedProvider, 5000, 5, _locationListener);
            }
            catch (Exception ex)
            {
                App.Logger.LogError(ex.Message);
            }
        }

        private void StopLocationTracking()
        {
            try
            {
                _locationManager?.RemoveUpdates(_locationListener);
                _locationManager = null;
            }
            catch (Exception ex)
            {
                App.Logger.LogError(ex.Message);
            }
        }
    }
}
