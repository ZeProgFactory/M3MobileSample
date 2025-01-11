namespace M3SDKMaui;
public partial class MainPage : ContentPage
{
   public MainPage()
   {
      InitializeComponent();
   }

   private async void Button_Clicked_M3SDKPage(object sender, EventArgs e)
   {
      await Shell.Current.GoToAsync("///M3SDKPage");
   }

   private async void Button_Clicked_TestPage(object sender, EventArgs e)
   {
      await Shell.Current.GoToAsync("///TestPage");
   }
}
