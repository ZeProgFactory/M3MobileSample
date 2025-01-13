using System.Collections.ObjectModel;
using ScannerM3;


namespace M3SDKMaui;
public partial class M3SDKPage : ContentPage
{
   private M3ScannerViewModel vm;
   private ObservableCollection<string> _scanned { get; set; }

   public M3SDKPage()
   {
      BindingContext = M3ScannerViewModel.Current;
      vm = M3ScannerViewModel.Current;

      InitializeComponent();

      _scanned = new ObservableCollection<string>();
      listView_scanned.ItemsSource = _scanned;
   }

   protected override void OnAppearing()
   {
      base.OnAppearing();

      M3ScannerViewModel.Current.OnBarcode += Current_BarcodeReceived;
   }

   protected override void OnDisappearing()
   {
      M3ScannerViewModel.Current.OnBarcode -= Current_BarcodeReceived;

      //MessagingCenter.Subscribe<App, int>(this, "value", (sender, arg) =>
      //{
      //   edValue.Text = "" + arg;
      //});

      base.OnDisappearing();
   }

   private void Current_BarcodeReceived(object sender, M3Barcode barcode)
   {
      _scanned.Insert(0, "Data: " + barcode.ToString());
   }

   private void Button_Clicked_Start(object sender, EventArgs e)
   {
      vm.DecodeStart();
   }

   private void Button_Clicked_Stop(object sender, EventArgs e)
   {
      vm.DecodeStop();
   }

   private void Button_Clicked_Enable(object sender, EventArgs e)
   {
      vm.SetEnable(true);
   }

   private void Button_Clicked_Disable(object sender, EventArgs e)
   {
      vm.SetEnable(false);
   }

   private void Button_Clicked_GetParam(object sender, EventArgs e)
   {
      int nParam = Int32.Parse(edParam.Text);

      vm.GetScanParam(nParam);
      // return by MessagingCenter
   }

   private void Button_Clicked_SetParam(object sender, EventArgs e)
   {
      int nParam = Int32.Parse(edParam.Text);
      int nValue = Int32.Parse(edValue.Text);

      vm.SetScanParam(nParam, nValue);
   }

   private void chkKeyDisable_CheckedChanged_KeyDisable(object sender, CheckedChangedEventArgs e)
   {
      vm.SetKeyDisable(chkKeyDisable.IsChecked);
   }
}
