
using Android.Content;
using ScannerM3;

namespace M3SDKMaui;

public class M3ScannerHmm : IM3Scanner
{
   #region - - - define actions & co - - -

   /// <summary>
   /// action scanner Settings
   /// </summary>
   private const String SCANNER_ACTION_SETTING_CHANGE = "com.android.server.scannerservice.settingchange";

   /// <summary>
   /// Action Scanner Setting Parameter and Value
   /// </summary>
   private const String SCANNER_ACTION_PARAMETER = "android.intent.action.SCANNER_PARAMETER";

   /// <summary>
   /// Action Scanner Enable or Disable
   /// </summary>
   private const String SCANNER_ACTION_ENABLE = "com.android.server.scannerservice.m3onoff";

   /// <summary>
   /// Extra about scanner enable or Disable
   /// </summary>
   private const String SCANNER_EXTRA_ENABLE = "scanneronoff";


   /// <summary>
   /// Action start decoding
   /// </summary>
   private const String SCANNER_ACTION_START = "android.intent.action.M3SCANNER_BUTTON_DOWN";

   /// <summary>
   /// Action stop decoding
   /// </summary>
   private const String SCANNER_ACTION_CANCEL = "android.intent.action.M3SCANNER_BUTTON_UP";


   /// <summary>
   /// Action to receive decoding result and setting parameter result
   /// </summary>
   private const String SCANNER_ACTION_BARCODE = "com.android.server.scannerservice.broadcast";

   /// <summary>
   /// Extra about decoding result
   /// </summary>
   public const String SCANNER_EXTRA_BARCODE_DATA = "m3scannerdata";

   /// <summary>
   /// Extra about code type
   /// </summary>
   public const String SCANNER_EXTRA_BARCODE_CODE_TYPE = "m3scanner_code_type";

   #endregion

   ScanReceiver _InternalScanReceiver = null;

   public M3ScannerHmm()
   {
      _InternalScanReceiver = new M3ScannerHmm.ScanReceiver(Application.Current);
   }

   /// <summary>
   /// Action start decoding
   /// </summary>
   public void DecodeStart()
   {
      Intent intent = new Intent(SCANNER_ACTION_START);
      Android.App.Application.Context.SendBroadcast(intent);
   }

   /// <summary>
   /// Action stop decoding
   /// </summary>
   public void DecodeStop()
   {
      Android.App.Application.Context.SendBroadcast(new Intent(SCANNER_ACTION_CANCEL));
   }

   /// <summary>
   /// Action to receive decoding result and setting parameter result
   /// </summary>
   public void RegisterReceiver()
   {
      // for getting decoding result and setParam, getParam result.
      System.Diagnostics.Debug.WriteLine(String.Format("RegisterReceiver"));

      if (_InternalScanReceiver != null)
      {
         IntentFilter filter = new IntentFilter();
         filter.AddAction(SCANNER_ACTION_BARCODE);
         Android.App.Application.Context.RegisterReceiver(_InternalScanReceiver, filter);
      };
   }

   /// <summary>
   /// 
   /// </summary>
   public void UnregisterReceiver()
   {
      System.Diagnostics.Debug.WriteLine(String.Format("UnregisterReceiver"));

      if (_InternalScanReceiver != null)
      {
         Android.App.Application.Context.UnregisterReceiver(_InternalScanReceiver);
      }
   }

   /// <summary>
   /// Action Scanner Enable or Disable
   /// </summary>
   /// <param name="isEnabled"></param>
   public void SetEnable(bool isEnabled)
   {
      // Scanner enable or disable. If you want to prevent only decoding, we recommend 'SetKeyDisable'
      Intent intent = new Intent(SCANNER_ACTION_ENABLE);
      intent.PutExtra(SCANNER_EXTRA_ENABLE, isEnabled ? 1 : 0);
      Android.App.Application.Context.SendBroadcast(intent);
   }

   /// <summary>
   /// Action Scanner getting Parameter and Value
   /// </summary>
   /// <param name="param"></param>
   public void GetScanParam(int param)
   {
      Intent intent = new Intent(SCANNER_ACTION_PARAMETER);
      intent.PutExtra("symbology", param);
      intent.PutExtra("value", -1);
      Android.App.Application.Context.SendBroadcast(intent);
   }

   /// <summary>
   /// Action Scanner setting Parameter and Value
   /// </summary>
   /// <param name="param"></param>
   public void SetScanParam(int param, int value)
   {
      Intent intent = new Intent(SCANNER_ACTION_PARAMETER);
      intent.PutExtra("symbology", param);
      intent.PutExtra("value", value);
      Android.App.Application.Context.SendBroadcast(intent);
   }

   /// <summary>
   /// Trigger button disable
   /// </summary>
   /// <param name="isEnabled"></param>
   public void SetKeyDisable(bool isEnabled)
   {
      Intent intent = new Intent(SCANNER_ACTION_SETTING_CHANGE);
      intent.PutExtra("setting", "key_press");
      intent.PutExtra("key_press_value", isEnabled ? 0 : 1);
      Android.App.Application.Context.SendBroadcast(intent);
   }

   public void SetSound(SoundMode soundMode)
   {
      Intent intent = new Intent(SCANNER_ACTION_SETTING_CHANGE);
      intent.PutExtra("setting", "sound");
      intent.PutExtra("sound_mode", (int)soundMode);
      Android.App.Application.Context.SendBroadcast(intent);
   }

   public void VibrationEnable(bool vibration)
   {
      Intent intent = new Intent(SCANNER_ACTION_SETTING_CHANGE);
      intent.PutExtra("setting", "vibration");
      intent.PutExtra("vibration_value", vibration ? 1 : 0);
      Android.App.Application.Context.SendBroadcast(intent);
   }

   public void SetReadMode(ReadMode readMode)
   {
      Intent intent = new Intent(SCANNER_ACTION_SETTING_CHANGE);
      intent.PutExtra("setting", "read_mode");
      intent.PutExtra("read_mode_value", (int)readMode);
      Android.App.Application.Context.SendBroadcast(intent);
   }

   public void SetEndCharacter(EndCharacter endCharacter)
   {
      Intent intent = new Intent(SCANNER_ACTION_SETTING_CHANGE);
      intent.PutExtra("setting", "end_char");
      intent.PutExtra("end_char_value", (int)endCharacter);
      Android.App.Application.Context.SendBroadcast(intent);
   }

   public void SetOutputMode(OutputMode outputMode)
   {
      Intent intent = new Intent(SCANNER_ACTION_SETTING_CHANGE);
      intent.PutExtra("setting", "output_mode");
      intent.PutExtra("output_mode_value", (int)outputMode);
      Android.App.Application.Context.SendBroadcast(intent);
   }

   // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -

   //ToDo: Scanner: class ScanReceiver : BroadcastReceiver
   public class ScanReceiver : BroadcastReceiver
   {
      private String barcode;
      private String type;
      //private App scanApp = new App();
      private Application scanApp;

      public ScanReceiver(Application app)
      {
         scanApp = app;
      }

      public override void OnReceive(Context context, Intent intent)
      {
         Android.Util.Log.Info("ScanReceiver", "onReceiver: " + intent.Action);

         if (intent.Action.Equals(SCANNER_ACTION_BARCODE))
         {
            barcode = intent.GetStringExtra(SCANNER_EXTRA_BARCODE_DATA);

            if (barcode != null)
            {
               // Send Barcode Data
               type = intent.GetStringExtra(SCANNER_EXTRA_BARCODE_CODE_TYPE);

               MessagingCenter.Send<Application, string>(scanApp, "barcode", barcode);
               //System.Diagnostics.Debug.WriteLine(String.Format("OnReceive barcode: " + barcode + " type: " + type));
            }
            else
            {
               // Send Parameter data
               int nParam = intent.GetIntExtra("symbology", -1);
               int nValue = intent.GetIntExtra("value", -1);

               MessagingCenter.Send<Application, int>(scanApp, "value", nValue);
               //System.Diagnostics.Debug.WriteLine(String.Format("OnReceive param: " + nParam + " value: " + nValue));
            }
         }
      }
   }

   // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -
}

