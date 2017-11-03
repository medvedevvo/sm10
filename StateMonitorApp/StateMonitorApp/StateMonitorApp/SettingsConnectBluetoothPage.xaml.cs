using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;

namespace StateMonitorApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsConnectBluetoothPage : ContentPage
	{
        IBluetoothLE ble;
        IAdapter adapter;
        private ObservableCollection<IDevice> deviceList;
        IDevice device;
        IList<IService> services;
        IService service;
        ListView lv;

        public SettingsConnectBluetoothPage ()
		{
			InitializeComponent ();
            lv = this.FindByName<ListView>("_lv");

            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            adapter.DeviceDiscovered += test;

            deviceList = new ObservableCollection<IDevice>();

            lv.ItemsSource = deviceList;
		}


        private void test(object sender, EventArgs e)
        {
            this.DisplayAlert("Сообщение", "Обнаружено устройство!" , "OK");
        }
       
        private void btnStatus_Clicked(object sender, EventArgs e)
        {
            var state = ble.State;


            this.DisplayAlert("Режим работы адаптера", 
                state.ToString() + " " + ble.IsAvailable.ToString() + " " +
                ble.Adapter.IsScanning.ToString(), "OK");
        }
        private async void btnScanDev_Clicked(object sender, EventArgs e)
        {
            //adapter.ScanTimeout = 10000;
            //deviceList.Clear();

            adapter.DeviceDiscovered += (s, a) => deviceList.Add(a.Device);
            await adapter.StartScanningForDevicesAsync();
        }

        private void lv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(lv.SelectedItem == null)
            {
                return;
            }

            device = lv.SelectedItem as IDevice;
        }

        private async void btnConnect_Clicked(object sender, EventArgs lo)
        {
            try
            {
                await adapter.ConnectToDeviceAsync(device);
            }
            catch(DeviceConnectionException e)
            {

            }
        }

        private async void btnConnectKnow_Clicked(object sender, EventArgs lo)
        {
            try
            {
                await adapter.ConnectToKnownDeviceAsync(new Guid("guid"));
            }
            catch (DeviceConnectionException e)
            {

            }
        }

        private async void btnGetServs_Clicked(object sender, EventArgs e)
        {
            services = await device.GetServicesAsync();

            service = await device.GetServiceAsync(
                Guid.Parse("ffe0ecd2-3d16-4f8d-90de-e89e7fc396a5"));
        }
    }
}